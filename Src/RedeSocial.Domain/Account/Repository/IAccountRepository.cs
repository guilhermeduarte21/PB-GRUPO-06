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
        Task<Account> GetAccountByEmailPassword(string email, string password);
        Task<Account> GetAccountByUserNamePassword(string userName, string password);
        Task<IdentityResult> CreateAsync(Account user, CancellationToken cancellationToken);
        Task<Account> FindByUserNameAsync(string UserName, CancellationToken cancellationToken);

        Task<Account> FindByEmailAsync(string Email, CancellationToken cancellationToken);
    }
}
