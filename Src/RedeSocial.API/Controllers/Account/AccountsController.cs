using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedeSocial.Services.Account;

namespace RedeSocial.API.Controllers.Account
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private IAccountService AccountService { get; set; }

        public AccountsController(IAccountService accountService)
        {
            this.AccountService = accountService;
        }

        // GET: api/Accounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Domain.Account.Account>>> GetAccounts()
        {
            return await AccountService.GetAccountsAsync();
        }

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Domain.Account.Account>> GetAccount(Guid id)
        {
            var account = await AccountService.FindByIdAsync(id.ToString(), default);

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }

        // PUT: api/Accounts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(Guid id, Domain.Account.Account account)
        {
            if (id != account.ID)
            {
                return BadRequest();
            }

            await AccountService.UpdateAsync(account, default);


            return NoContent();
        }


        // POST: api/Accounts
        [AllowAnonymous]
        [Route("Register")]
        [HttpPost]
        public async Task<ActionResult<Domain.Account.Account>> PostAccount(Domain.Account.Account account)
        {
            await AccountService.CreateAsync(account, default);

            return CreatedAtAction("GetAccount", new { id = account.ID }, account);
        }

        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Domain.Account.Account>> DeleteAccount(Guid id)
        {
            var account = await AccountService.FindByIdAsync(id.ToString(), default);
            if (account == null)
            {
                return NotFound();
            }

            await AccountService.DeleteAsync(account, default);

            return account;
        }
    }
}
