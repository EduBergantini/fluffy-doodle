using System.Net;
using System.Net.Http;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Xunit;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

using Blog.Domain.Users.Models;
using Blog.IntegrationTests.Common;
using Blog.IntegrationTests.Users.Factories;
using Blog.Domain.Users.UseCases;
using Blog.IntegrationTests.Users.Stubs;
using Blog.Domain.Users.Errors;
using System.Collections.Generic;


namespace Blog.IntegrationTests.Users
{
    public class UsersControllerTests : IntegrationTest<UsersIntegrationTest>
    {
        public readonly string signInUrl = "/api/users/sign-in";
        private readonly StringContent validHttpContent;

        public UsersControllerTests(UsersIntegrationTest apiWebApplicationFactory) 
            : base(apiWebApplicationFactory)
        {
            this.validHttpContent = new StringContent("{\"email\": \"any_email\", \"plainTextPassword\": \"any_password\"}", Encoding.UTF8, "application/json");
        }

        [Fact]
        public async Task POST_ShouldReturnBadRequestWhenSignReceiveInvalidModel()
        {
            var response = await base.httpClient.PostAsync(this.signInUrl, new StringContent("{}", Encoding.UTF8, "application/json"));
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var body = await response.Content.ReadAsStringAsync();
            Assert.False(string.IsNullOrWhiteSpace(body));
        }

        [Fact]
        public async Task POST_ShouldReturnBadRequestWhenSignInDoNotFindUser()
        {
            var client = base.factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddScoped<IAuthenticateUseCase, AuthenticateUseCaseWithExceptionStub>((service) =>
                    {
                        return new AuthenticateUseCaseWithExceptionStub(new UserNotFoundException());
                    });
                });
            }).CreateClient();

            var response = await client.PostAsync(this.signInUrl, this.validHttpContent);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var body = await response.Content.ReadAsStringAsync();
            Assert.False(string.IsNullOrWhiteSpace(body));

            var error = JsonSerializer.Deserialize<IDictionary<string, string[]>>(body);
            Assert.Single(error.First().Value);
            Assert.Equal("Senha inválida ou o usuário não encontrado", error.First().Value[0]);
        }

        [Fact]
        public async Task POST_ShouldReturnBadRequestWhenSignInReceiveInvalidPassword()
        {
            var client = base.factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddScoped<IAuthenticateUseCase, AuthenticateUseCaseWithExceptionStub>((service) =>
                    {
                        return new AuthenticateUseCaseWithExceptionStub(new InvalidPasswordException());
                    });
                });
            }).CreateClient();

            var response = await client.PostAsync(this.signInUrl, this.validHttpContent);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var body = await response.Content.ReadAsStringAsync();
            Assert.False(string.IsNullOrWhiteSpace(body));

            var error = JsonSerializer.Deserialize<IDictionary<string, string[]>>(body);
            Assert.Single(error.First().Value);
            Assert.Equal("Senha inválida ou o usuário não encontrado", error.First().Value[0]);
        }


        [Fact]
        public async Task POST_ShouldReturnAuthenticationTokenWhenSignInSucceeds()
        {
            var response = await base.httpClient.PostAsync(this.signInUrl, validHttpContent);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var body = await response.Content.ReadAsStringAsync();
            var authToken = JsonSerializer.Deserialize<AuthenticationTokenModel>(body, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            Assert.Equal("any_token", authToken.Token);
            Assert.Equal(1000, authToken.ExpireInMs);
        }
    }
}
