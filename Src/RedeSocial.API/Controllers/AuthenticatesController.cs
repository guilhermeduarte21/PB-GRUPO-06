using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedeSocial.Domain.ViewModel;
using RedeSocial.Services.Account;
using RedeSocial.Services.Authenticate;

namespace RedeSocial.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticatesController : ControllerBase
    {
        private readonly IAuthenticateService _authenticateService;

        public AuthenticatesController(IAuthenticateService authenticateService)
        {
            this._authenticateService = authenticateService;
        }

        [Route("Token")]
        [HttpPost]
        [RequireHttps]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
                return await Task.FromResult(BadRequest(ModelState));

            var token = _authenticateService.Login(loginRequest.UserName, loginRequest.Password);

            if (String.IsNullOrWhiteSpace(token.Result))
            {
                return await Task.FromResult(BadRequest("Login ou senha Inválidos"));
            }

            return Ok(new
            {
                Token = token.Result
            });
        }

    }
}
