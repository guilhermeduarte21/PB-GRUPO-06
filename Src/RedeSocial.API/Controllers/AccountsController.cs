using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedeSocial.Domain.Account;
using RedeSocial.Domain.Post;
using RedeSocial.Services.Account;
using RedeSocial.Services.Post;
using AutoMapper;

namespace RedeSocial.API.Controllers
{
    [Route("api/Accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IAccountIdentityManager _accountIdentityManager;
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        public AccountsController(IAccountService accountService, IAccountIdentityManager accountIdentityManager, IPostService postService,
                                    IMapper mapper)
        {
            this._accountService = accountService;
            this._accountIdentityManager = accountIdentityManager;
            this._postService = postService;
            this._mapper = mapper;
        }

        #region API ACCOUNT
        // GET: api/Accounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
            var listaAccounts = await _accountService.GetAccountsAsync();

            return Ok(listaAccounts);
        }
        // GET: api/Accounts/5
        [HttpGet("{username}")]
        public async Task<ActionResult<Account>> GetAccount(string userName)
        {
            var account = await _accountService.FindByUserNameAsync(userName, default);

            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }
        [HttpGet("pesquisa/{name}")]
        public ActionResult GetAccountByName(string name)
        {
            var accounts = _accountService.FindAccountAsync(name, default);

            if (accounts.Result == null)
                return NotFound();

            var response = _mapper.Map<List<AccountSimplesResponse>>(accounts.Result.Value);

            return Ok(response);
        }
        // PUT: api/Accounts/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAccount(Guid id, Account account)
        {
            if (id != account.ID)
            {
                return BadRequest();
            }


            try
            {
                await _accountService.UpdateAsync(account, default);
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
            await _accountService.CreateAsync(account, default);

            return CreatedAtAction("GetAccount", new { id = account.ID });
        }
        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Account>> DeleteAccount(Guid id)
        {
            var account = await _accountService.FindByIdAsync(id.ToString(), default);
            if (account == null)
            {
                return NotFound();
            }

            await _accountService.DeleteAsync(account, default);

            return account;
        }
        [Route("logout")]
        [HttpGet]
        public async Task<ActionResult> Logout()
        {
            await this._accountIdentityManager.Logout();
            return Ok("Deslogado!");
        }
        private bool AccountExists(Guid id)
        {
            return _accountService.FindByIdAsync(id.ToString(), default) != null;
        }
        #endregion //API ACCOUNT

        #region API POST
        [HttpGet("{id}/posts")]
        public ActionResult GetPostsDaAccount([FromRoute] Guid id)
        {
            var account = _accountService.FindByIdAsync(id.ToString(), default);

            if (account.Result == null)
                return NotFound();

            var postagens = _postService.GetPostByAccountAsync(id, default);

            var response = _mapper.Map<List<PostagemResponse>>(postagens.Result);

            return Ok(response);
        }

        [HttpPost("{id}/posts")]
        public async Task<ActionResult> PostPostsDaAccount([FromRoute] Guid id, [FromBody] Postagem postagem)
        {
            var account = await _accountService.FindByIdAsync(id.ToString(), default);

            if (account == null)
                return NotFound();

            postagem.ID_Account = account;
            await _postService.CreateAsync(postagem, default);

            return CreatedAtAction(nameof(PostPostsDaAccount), new { postagem.ID });
        }
        #endregion //API POST
    }
}
