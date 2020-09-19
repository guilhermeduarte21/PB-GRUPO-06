using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RedeSocial.Services.Account
{
    public interface IAccountService
    {
        Task<IdentityResult> CreateAsync(Domain.Account.Account user, CancellationToken cancellationToken);
        Task<IdentityResult> DeleteAsync(Domain.Account.Account user, CancellationToken cancellationToken);
        Task<Domain.Account.Account> FindByIdAsync(string userId, CancellationToken cancellationToken);
        Task<Domain.Account.Account> FindByUserNameAsync(string UserName, CancellationToken cancellationToken);
        Task<Domain.Account.Account> FindByEmailAsync(string Email, CancellationToken cancellationToken);
        Task<string> GetUserIdAsync(Domain.Account.Account user, CancellationToken cancellationToken);
        Task<string> GetUserNameAsync(Domain.Account.Account user, CancellationToken cancellationToken);
        Task<string> GetNormalizedUserNameAsync(Domain.Account.Account user, CancellationToken cancellationToken);
        Task<Domain.Account.Account> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken);
        Task SetNormalizedUserNameAsync(Domain.Account.Account user, string normalizedName, CancellationToken cancellationToken);
        Task SetUserNameAsync(Domain.Account.Account user, string userName, CancellationToken cancellationToken);
        Task<IdentityResult> UpdateAsync(Domain.Account.Account user, CancellationToken cancellationToken);
        Task<Domain.Account.Account> GetAccountByEmailPassword(string email, string password);
        Task<Domain.Account.Account> GetAccountByUserNamePassword(string userName, string password);
        Task<ActionResult<IEnumerable<Domain.Account.Account>>> GetAccountsAsync();
        Task<ActionResult<IEnumerable<Domain.Account.Account>>> FindAccountAsync(string name, CancellationToken cancellationToken);
    }
}
