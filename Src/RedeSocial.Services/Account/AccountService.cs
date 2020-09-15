using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        private IRoleStore<RedeSocial.Domain.Account.Role> RoleRepository { get; set; }

        public AccountService(IAccountRepository accountRepository, IRoleStore<RedeSocial.Domain.Account.Role> roleRepository)
        {
            this.AccountRepository = accountRepository;
            this.RoleRepository = roleRepository;
        }

        async Task<IdentityResult> IAccountService.CreateAsync(Domain.Account.Account user, CancellationToken cancellationToken)
        {
            var userName = await this.AccountRepository.FindByUserNameAsync(user.UserName, default);
            var userEmail = await this.AccountRepository.FindByEmailAsync(user.Email, default);

            var role = this.RoleRepository.FindByNameAsync("USUARIO", default);

            if (userName == null && userEmail == null && role != null)
            {
                user.ID_Role = role.Result;               

                await this.AccountRepository.CreateAsync(user, cancellationToken);

                role.Result.IDs_Accounts.Add(user);
                return IdentityResult.Success;
            }
            else
            {
                return IdentityResult.Failed();
            }
        }

        public async Task<IdentityResult> DeleteAsync(Domain.Account.Account user, CancellationToken cancellationToken)
        {
            return await AccountRepository.DeleteAsync(user, cancellationToken);
        }

        public async Task<Domain.Account.Account> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return await AccountRepository.FindByIdAsync(userId, cancellationToken);
        }

        public async Task<Domain.Account.Account> FindByUserNameAsync(string UserName, CancellationToken cancellationToken)
        {
            return await AccountRepository.FindByUserNameAsync(UserName, cancellationToken);
        }

        public async Task<Domain.Account.Account> FindByEmailAsync(string Email, CancellationToken cancellationToken)
        {
            return await AccountRepository.FindByEmailAsync(Email, cancellationToken);
        }

        public async Task<string> GetUserIdAsync(Domain.Account.Account user, CancellationToken cancellationToken)
        {
            return await AccountRepository.GetUserIdAsync(user, cancellationToken);
        }

        public async Task<string> GetUserNameAsync(Domain.Account.Account user, CancellationToken cancellationToken)
        {
            return await AccountRepository.GetUserNameAsync(user, cancellationToken);
        }

        public async Task<string> GetNormalizedUserNameAsync(Domain.Account.Account user, CancellationToken cancellationToken)
        {
            return await AccountRepository.GetNormalizedUserNameAsync(user, cancellationToken);
        }

        public async Task<Domain.Account.Account> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return await AccountRepository.FindByNameAsync(normalizedUserName, cancellationToken);
        }

        public Task SetNormalizedUserNameAsync(Domain.Account.Account user, string normalizedName, CancellationToken cancellationToken)
        {
            return AccountRepository.SetNormalizedUserNameAsync(user, normalizedName, cancellationToken);
        }

        public Task SetUserNameAsync(Domain.Account.Account user, string userName, CancellationToken cancellationToken)
        {
            return AccountRepository.SetUserNameAsync(user, userName, cancellationToken);
        }

        public async Task<IdentityResult> UpdateAsync(Domain.Account.Account user, CancellationToken cancellationToken)
        {
            return await AccountRepository.UpdateAsync(user, cancellationToken);
        }

        public async Task<Domain.Account.Account> GetAccountByEmailPassword(string email, string password)
        {
            return await AccountRepository.GetAccountByEmailPassword(email, password);
        }

        public async Task<Domain.Account.Account> GetAccountByUserNamePassword(string userName, string password)
        {
            return await AccountRepository.GetAccountByUserNamePassword(userName, password);
        }

        public async Task<ActionResult<IEnumerable<Domain.Account.Account>>> GetAccountsAsync()
        {
            return await AccountRepository.GetAccountsAsync();
        }
    }
}
