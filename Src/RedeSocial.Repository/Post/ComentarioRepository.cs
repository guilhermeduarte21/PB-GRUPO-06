using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RedeSocial.Domain.Post;
using RedeSocial.Domain.Account;
using RedeSocial.Repository.Context;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RedeSocial.Repository.Post
{
    public class ComentarioRepository : IUserStore<Comentario>, IComentarioRepository
    {
        private RedeSocialContext Context { get; set; }

        public ComentarioRepository(RedeSocialContext redeSocialContext)
        {
            this.Context = redeSocialContext;
        }

        public async Task<IdentityResult> CreateAsync(Comentario user, CancellationToken cancellationToken)
        {
            this.Context.Comentarios.Add(user);
            await this.Context.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(Comentario user, CancellationToken cancellationToken)
        {
            this.Context.Comentarios.Remove(user);
            await this.Context.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public void Dispose()
        {

        }

        public async Task<Comentario> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return await this.Context.Comentarios.FirstOrDefaultAsync(x => x.ID == new Guid(userId));
        }

        public async Task<Comentario> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return await this.Context.Comentarios.FirstOrDefaultAsync(x => x.Descricao == normalizedUserName);
        }

        public async Task<string> GetNormalizedUserNameAsync(Comentario user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.Descricao);
        }

        public async Task<string> GetUserIdAsync(Comentario user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.ID.ToString());
        }

        public async Task<string> GetUserNameAsync(Comentario user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.Descricao);
        }

        public Task SetNormalizedUserNameAsync(Comentario user, string normalizedName, CancellationToken cancellationToken)
        {
            user.Descricao = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(Comentario user, string userName, CancellationToken cancellationToken)
        {
            user.Descricao = userName;
            return Task.CompletedTask;
        }

        public async Task<IdentityResult> UpdateAsync(Comentario user, CancellationToken cancellationToken)
        {
            var comentarioToUpdate = await this.Context.Comentarios.AsNoTracking().FirstOrDefaultAsync(x => x.ID == user.ID);
            comentarioToUpdate = user;
            this.Context.Entry(comentarioToUpdate).State = EntityState.Modified;
            this.Context.Comentarios.Add(comentarioToUpdate);
            await this.Context.SaveChangesAsync();

            return IdentityResult.Success;
        }

        public async Task<string> GetDescricaoAsync(Comentario user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.Descricao);
        }

        public async Task<Domain.Account.Account> GetID_AccountAsync(Comentario user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.ID_Account);
        }

        public async Task<Postagem> GetID_PostagemAsync(Comentario user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.ID_Postagem);
        }
    }
}
