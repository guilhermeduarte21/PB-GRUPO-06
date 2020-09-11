using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RedeSocial.Domain.Post;
using RedeSocial.Domain.Profile;
using RedeSocial.Repository.Context;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RedeSocial.Repository.Post
{
    public class PostagemRepository : IUserStore<Postagem>, IPostagemRepository
    {
        private RedeSocialContext Context { get; set; }

        public PostagemRepository(RedeSocialContext redeSocialContext)
        {
            this.Context = redeSocialContext;
        }

        public async Task<IdentityResult> CreateAsync(Postagem user, CancellationToken cancellationToken)
        {
            this.Context.Postagens.Add(user);
            await this.Context.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(Postagem user, CancellationToken cancellationToken)
        {
            this.Context.Postagens.Remove(user);
            await this.Context.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public void Dispose()
        {

        }

        public async Task<Postagem> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return await this.Context.Postagens.FirstOrDefaultAsync(x => x.ID == new Guid(userId));
        }

        public async Task<Postagem> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return await this.Context.Postagens.FirstOrDefaultAsync(x => x.Descricao == normalizedUserName);
        }

        public async Task<string> GetNormalizedUserNameAsync(Postagem user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.Descricao);
        }

        public async Task<string> GetUserIdAsync(Postagem user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.ID.ToString());
        }

        public async Task<string> GetUserNameAsync(Postagem user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.Descricao);
        }

        public Task SetNormalizedUserNameAsync(Postagem user, string normalizedName, CancellationToken cancellationToken)
        {
            user.Descricao = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(Postagem user, string userName, CancellationToken cancellationToken)
        {
            user.Descricao = userName;
            return Task.CompletedTask;
        }

        public async Task<IdentityResult> UpdateAsync(Postagem user, CancellationToken cancellationToken)
        {
            var perfilToUpdate = await this.Context.Postagens.AsNoTracking().FirstOrDefaultAsync(x => x.ID == user.ID);
            perfilToUpdate = user;
            this.Context.Entry(perfilToUpdate).State = EntityState.Modified;
            this.Context.Postagens.Add(perfilToUpdate);
            await this.Context.SaveChangesAsync();

            return IdentityResult.Success;
        }

        public async Task<string> GetDescricaoAsync(Postagem user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.Descricao);
        }

        public async Task<string> GetFotoPostUrlAsync(Postagem user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.FotoPostUrl);
        }

        public async Task<Perfil> GetID_PerfilAsync(Postagem user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.ID_Perfil);
        }

        public async Task<IList<Comentario>> GetIDs_ComentariosAsync(Postagem user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.IDs_Comentarios);
        }
    }
}
