using Blog.Domain.Users.Models;
using Blog.IntegrationTests.Common;
using Blog.IntegrationTests.Users.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

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
        public async Task POST_ShouldReturnAuthenticationTokenWhenSignInSucceeds()
        {
            var response = await base.httpClient.PostAsync(this.signInUrl, validHttpContent);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var body = await response.Content.ReadAsStringAsync();
            var authToken = JsonSerializer.Deserialize<AuthenticationTokenModel>(body, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            Assert.Equal("any_token", authToken.Token);
            Assert.Equal(1000, authToken.ExpireInMs);
        }

        [Fact]
        public async Task POST_ShouldReturnBadRequestWhenSignReceiveInvalidModel()
        {
            var response = await base.httpClient.PostAsync(this.signInUrl, new StringContent("{}", Encoding.UTF8, "application/json"));
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var body = await response.Content.ReadAsStringAsync();
            Assert.False(string.IsNullOrWhiteSpace(body));
        }
    }
}
