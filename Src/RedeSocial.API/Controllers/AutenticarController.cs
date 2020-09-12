using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedeSocial.API.ViewModel;
using RedeSocial.Services.Account;

namespace RedeSocial.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticarController : ControllerBase
    {
        private IAccountIdentityManager AccountIdentityManager { get; set; }

        public AutenticarController(IAccountIdentityManager accountIdentityManager)
        {
            this.AccountIdentityManager = accountIdentityManager;
        }

        [Route("Login")]
        [HttpPost]
        [RequireHttps]
        public async Task<IActionResult> AuthenticateAsync(LoginRequest model)
        {
            if (!ModelState.IsValid)
                return await Task.FromResult(BadRequest(ModelState));

            var response = AccountIdentityManager.Login(model.Username, model.Password);

            if (response == null)
                return await Task.FromResult(BadRequest("Login ou senha Inválidos"));

            return Ok(response);
        }
    }
}
