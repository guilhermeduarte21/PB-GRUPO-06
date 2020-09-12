using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedeSocial.Domain.Account;
using RedeSocial.Repository.Context;
using RedeSocial.Services.Account;

namespace RedeSocial.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        private IAccountService AccountService { get; set; }
        private IAccountIdentityManager AccountIdentityManager { get; set; }

        public ContaController(IAccountService accountService, IAccountIdentityManager accountIdentityManager)
        {
            this.AccountService = accountService;
            this.AccountIdentityManager = accountIdentityManager;
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
            await this.AccountIdentityManager.Logout();
        }

        private bool AccountExists(Guid id)
        {
            return AccountService.FindByIdAsync(id.ToString(), default) != null;
        }
    }
}
