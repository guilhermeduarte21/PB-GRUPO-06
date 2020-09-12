using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedeSocial.API.Controllers.Account.Model;
using RedeSocial.Services.Account;

namespace RedeSocial.API.Controllers.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private IAccountIdentityManager AccountIdentityManager { get; set; }

        public AuthenticateController(IAccountIdentityManager accountIdentityManager)
        {
            this.AccountIdentityManager = accountIdentityManager;
        }

        [Route("Login")]
        [HttpPost]
        [RequireHttps]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequestModel loginRequest)
        {
            if (!ModelState.IsValid)
                return await Task.FromResult(BadRequest(ModelState));

            var result = await AccountIdentityManager.Login(loginRequest.Username, loginRequest.Password);

            if (!result.Succeeded)
            {
                return await Task.FromResult(BadRequest("Login ou senha inválidos"));
            }

            return Ok();
        }

        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            await AccountIdentityManager.Logout();

            return Ok();
        }
    }
}
