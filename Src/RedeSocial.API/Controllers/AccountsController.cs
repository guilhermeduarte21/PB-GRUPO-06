using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedeSocial.Domain.Account;
using RedeSocial.Services.Account;

namespace RedeSocial.API.Controllers
{
    [Route("api/Accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService AccountService;
        private readonly IAccountIdentityManager _accountIdentityManager;

        public AccountsController(IAccountService accountService, IAccountIdentityManager accountIdentityManager)
        {
            this.AccountService = accountService;
            this._accountIdentityManager = accountIdentityManager;
        }

        // GET: api/Accounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
            return await AccountService.GetAccountsAsync();
        }

        // GET: api/Accounts/5
        [HttpGet("{username}")]
        public async Task<ActionResult<Account>> GetAccount(string userName)
        {
            var account = await AccountService.FindByUserNameAsync(userName, default);

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }

        // PUT: api/Accounts/5
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

        // POST: api/Accounts
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<Account>> PostAccount(Account account)
        {
            await AccountService.CreateAsync(account, default);

            return CreatedAtAction("GetAccount", new { id = account.ID });
        }

        // DELETE: api/Accounts/5
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

        [Route("logout")]
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
