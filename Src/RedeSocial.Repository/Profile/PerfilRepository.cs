using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RedeSocial.Domain.Profile;
using RedeSocial.Domain.Profile.Repository;
using RedeSocial.Repository.Context;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RedeSocial.Repository.Profile
{
    public class PerfilRepository : IUserStore<Perfil>, IPerfilRepository
    {
        private RedeSocialContext Context { get; set; }

        public PerfilRepository(RedeSocialContext redeSocialContext)
        {
            this.Context = redeSocialContext;
        }

        public async Task<IdentityResult> CreateAsync(Perfil user, CancellationToken cancellationToken)
        {
            this.Context.Perfis.Add(user);
            await this.Context.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(Perfil user, CancellationToken cancellationToken)
        {
            this.Context.Perfis.Remove(user);
            await this.Context.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public void Dispose()
        {

        }

        public async Task<Perfil> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return await this.Context.Perfis.FirstOrDefaultAsync(x => x.ID == new Guid(userId));
        }

        public async Task<Perfil> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return await this.Context.Perfis.FirstOrDefaultAsync(x => x.Nome == normalizedUserName);
        }

        public async Task<string> GetNormalizedUserNameAsync(Perfil user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.Nome);
        }

        public async Task<string> GetUserIdAsync(Perfil user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.ID.ToString());
        }

        public async Task<string> GetUserNameAsync(Perfil user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.Nome);
        }

        public Task SetNormalizedUserNameAsync(Perfil user, string normalizedName, CancellationToken cancellationToken)
        {
            user.Nome = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(Perfil user, string userName, CancellationToken cancellationToken)
        {
            user.Nome = userName;
            return Task.CompletedTask;
        }

        public async Task<IdentityResult> UpdateAsync(Perfil user, CancellationToken cancellationToken)
        {
            var perfilToUpdate = await this.Context.Perfis.AsNoTracking().FirstOrDefaultAsync(x => x.ID == user.ID);
            perfilToUpdate = user;
            this.Context.Entry(perfilToUpdate).State = EntityState.Modified;
            this.Context.Perfis.Add(perfilToUpdate);
            await this.Context.SaveChangesAsync();

            return IdentityResult.Success;
        }

        public async Task<string> GetUserSobreNomeAsync(Perfil user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.SobreNome);
        }

        public Task SetSobreNomeAsync(Perfil user, string sobreNome, CancellationToken cancellationToken)
        {
            user.SobreNome = sobreNome;
            return Task.CompletedTask;
        }
    }
}
