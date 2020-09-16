using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedeSocial.Domain.Account.Repository;
using RedeSocial.Domain.Profile;
using RedeSocial.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RedeSocial.Repository.Account
{
    public class AccountRepository : IUserStore<Domain.Account.Account>, IAccountRepository
    {
        private bool disposedValue;

        private RedeSocialContext Context { get; set; }

        public AccountRepository(RedeSocialContext redeSocialContext)
        {
            this.Context = redeSocialContext;
        }

        public async Task<IdentityResult> CreateAsync(Domain.Account.Account user, CancellationToken cancellationToken)
        {
            this.Context.Accounts.Add(user);
            await this.Context.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(Domain.Account.Account user, CancellationToken cancellationToken)
        {
            this.Context.Accounts.Remove(user);
            await this.Context.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public Task<Domain.Account.Account> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return this.Context.Accounts.FirstOrDefaultAsync(x => x.ID == new Guid(userId));
        }

        public Task<Domain.Account.Account> FindByUserNameAsync(string UserName, CancellationToken cancellationToken)
        {
            return this.Context.Accounts.FirstOrDefaultAsync(x => x.UserName == UserName);
        }

        public Task<Domain.Account.Account> FindByEmailAsync(string Email, CancellationToken cancellationToken)
        {
            return this.Context.Accounts.FirstOrDefaultAsync(x => x.Email == Email);
        }

        public Task<string> GetUserIdAsync(Domain.Account.Account user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.ID.ToString());
        }

        public Task<string> GetUserNameAsync(Domain.Account.Account user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task<string> GetNormalizedUserNameAsync(Domain.Account.Account user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task<Domain.Account.Account> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return this.Context.Accounts.FirstOrDefaultAsync(x => x.UserName == normalizedUserName);
        }

        public Task SetNormalizedUserNameAsync(Domain.Account.Account user, string normalizedName, CancellationToken cancellationToken)
        {
            user.UserName = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(Domain.Account.Account user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.CompletedTask;
        }

        public async Task<IdentityResult> UpdateAsync(Domain.Account.Account user, CancellationToken cancellationToken)
        {
            var accountToUpdate = await this.Context.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.ID == user.ID);
            accountToUpdate = user;
            this.Context.Entry(accountToUpdate).State = EntityState.Modified;
            this.Context.Accounts.Add(accountToUpdate);
            await this.Context.SaveChangesAsync();

            return IdentityResult.Success;
        }

        public Task<Domain.Account.Account> GetAccountByEmailPassword(string email, string password)
        {
            return Task.FromResult(this.Context.Accounts
                                                .Include(x => x.ID_Role)
                                                .FirstOrDefault(x => x.Email == email && x.Password == password));
        }

        public Task<Domain.Account.Account> GetAccountByUserNamePassword(string userName, string password)
        {
            return Task.FromResult(this.Context.Accounts
                                                .Include(x => x.ID_Role)
                                                .FirstOrDefault(x => x.UserName == userName && x.Password == password));
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // Tarefa pendente: descartar o estado gerenciado (objetos gerenciados)
                }

                // Tarefa pendente: liberar recursos não gerenciados (objetos não gerenciados) e substituir o finalizador
                // Tarefa pendente: definir campos grandes como nulos
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Não altere este código. Coloque o código de limpeza no método 'Dispose(bool disposing)'
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public async Task<ActionResult<IEnumerable<Domain.Account.Account>>> GetAccountsAsync()
        {
           return await this.Context.Accounts.ToListAsync();
        }
    }
}
