﻿using System;
using System.Net;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Blog.Domain.Users.UseCases;
using Blog.Server.Api.Models;
using Blog.Domain.Users.Errors;
 
namespace Blog.Server.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAuthenticateUseCase authenticateUseCase;

        public UsersController(IAuthenticateUseCase authenticateUseCase)
        {
            this.authenticateUseCase = authenticateUseCase;
        }

        [Route("sign-in")]
        [HttpPost]
        public async Task<IActionResult> Post(AuthenticationModel authModel)
        {
            var authenticationToken = await this.authenticateUseCase.Authenticate(authModel.Email, authModel.PlainTextPassword);
            return Created("api/contents", authenticationToken);
        }
    }
}