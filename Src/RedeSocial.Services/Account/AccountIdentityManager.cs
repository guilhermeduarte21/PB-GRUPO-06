using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RedeSocial.CrossCutting.RegexExemple;
using RedeSocial.Domain.Account.Repository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RedeSocial.Services.Account
{
    public class AccountIdentityManager : IAccountIdentityManager
    {
        private IAccountRepository Repository { get; set; }
        private SignInManager<Domain.Account.Account> SignInManager { get; set; }
        private IConfiguration _configurantion { get; set; }

        Domain.Account.Account account;

        public AccountIdentityManager(IAccountRepository accountRepository, SignInManager<Domain.Account.Account> signInManager,
                                                                                IConfiguration configuration)
        {
            this.Repository = accountRepository;
            this.SignInManager = signInManager;
            _configurantion = configuration;
        }

        public async Task<SignInResult> Login(string userName, string password)
        {
            if (RegexUtilities.IsValidEmail(userName))
            {
                account = await this.Repository.GetAccountByEmailPassword(userName, password);
            }
            else
            {
                account = await this.Repository.GetAccountByUserNamePassword(userName, password);
            }

            if(account == null)
            {
                return SignInResult.Failed;
            }

            await SignInManager.SignInAsync(account, false);

            return SignInResult.Success;
        }

        public async Task Logout()
        {
            await this.SignInManager.SignOutAsync();
        }
    }
}
