using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RedeSocial.Domain.Account.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateAsync(Account user, CancellationToken cancellationToken);
        Task<IdentityResult> DeleteAsync(Account user, CancellationToken cancellationToken);
        Task<Account> FindByIdAsync(string userId, CancellationToken cancellationToken);
        Task<Account> FindByUserNameAsync(string UserName, CancellationToken cancellationToken);
        Task<Account> FindByEmailAsync(string Email, CancellationToken cancellationToken);
        Task<string> GetUserIdAsync(Account user, CancellationToken cancellationToken);
        Task<string> GetUserNameAsync(Account user, CancellationToken cancellationToken);
        Task<string> GetNormalizedUserNameAsync(Account user, CancellationToken cancellationToken);
        Task<Account> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken);
        Task SetNormalizedUserNameAsync(Account user, string normalizedName, CancellationToken cancellationToken);
        Task SetUserNameAsync(Account user, string userName, CancellationToken cancellationToken);
        Task<IdentityResult> UpdateAsync(Account user, CancellationToken cancellationToken);
        Task<Account> GetAccountByEmailPassword(string email, string password);
        Task<Account> GetAccountByUserNamePassword(string userName, string password);
    }
}
