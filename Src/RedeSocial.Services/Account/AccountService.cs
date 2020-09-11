using Microsoft.AspNetCore.Identity;
using RedeSocial.Domain.Account.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RedeSocial.Services.Account
{
    public class AccountService : IAccountService
    {
        private IAccountRepository AccountRepository { get; set; }

        public AccountService(IAccountRepository accountRepository)
        {
            this.AccountRepository = accountRepository;
        }

        async Task<IdentityResult> IAccountService.CreateAsync(Domain.Account.Account user, CancellationToken cancellationToken)
        {
            var userName = await this.AccountRepository.FindByUserNameAsync(user.UserName, default);
            var userEmail = await this.AccountRepository.FindByEmailAsync(user.Email, default);

            if (userName == null && userEmail == null)
            {
                await this.AccountRepository.CreateAsync(user, cancellationToken);
                return IdentityResult.Success;
            }
            else
            {
                return IdentityResult.Failed();
            }
        }
    }
}
