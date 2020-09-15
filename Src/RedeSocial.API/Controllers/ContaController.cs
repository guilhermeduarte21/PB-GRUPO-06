using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedeSocial.Domain.Account;
using RedeSocial.Domain.ViewModel;
using RedeSocial.Services.Account;

namespace RedeSocial.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        private readonly IAccountService AccountService;
        private readonly IAccountIdentityManager _accountIdentityManager;

        public ContaController(IAccountService accountService, IAccountIdentityManager accountIdentityManager)
        {
            this.AccountService = accountService;
            this._accountIdentityManager = accountIdentityManager;
        }

        [AllowAnonymous]
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> AuthenticateAsync(LoginRequest model)
        {
            if (!ModelState.IsValid)
                return await Task.FromResult(BadRequest(ModelState));

            var response = await _accountIdentityManager.Login(model.UserName, model.Password);

            if (response == null)
                return BadRequest(new { message = "Login ou senha Inválidos" });

            return Ok();
        }

        // GET: api/Conta
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
            return await AccountService.GetAccountsAsync();
        }

        // GET: api/Conta/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount(Guid id)
        {
            var account = await AccountService.FindByIdAsync(id.ToString(), default);

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }

        // PUT: api/Conta/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(Guid id, Account account)
        {
            if (id != account.ID)
            {
                return BadRequest();
            }


            try
            {
                await AccountService.UpdateAsync(account, default);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Conta
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<Account>> PostAccount(Account account)
        {
            await AccountService.CreateAsync(account, default);

            return CreatedAtAction("GetAccount", new { id = account.ID }, account);
        }

        // DELETE: api/Conta/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Account>> DeleteAccount(Guid id)
        {
            var account = await AccountService.FindByIdAsync(id.ToString(), default);
            if (account == null)
            {
                return NotFound();
            }

            await AccountService.DeleteAsync(account, default);

            return account;
        }

        [Route("api/logout")]
        [HttpGet]
        public async Task Logout()
        {
            await this._accountIdentityManager.Logout();
        }

        private bool AccountExists(Guid id)
        {
            return AccountService.FindByIdAsync(id.ToString(), default) != null;
        }
    }
}
