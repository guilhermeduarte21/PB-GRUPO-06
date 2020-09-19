using Microsoft.AspNetCore.Identity;
using RedeSocial.Domain.Post;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RedeSocial.Services.Post
{
    public class PostService : IPostService
    {
        private IPostagemRepository PostagemRepository { get; set; }

        public PostService(IPostagemRepository postagemRepository)
        {
            this.PostagemRepository = postagemRepository;
        }

        public async Task<IdentityResult> CreateAsync(Postagem post, CancellationToken cancellationToken)
        {
            return await PostagemRepository.CreateAsync(post, cancellationToken);
        }

        public async Task<IdentityResult> DeleteAsync(Postagem user, CancellationToken cancellationToken)
        {
            return await PostagemRepository.DeleteAsync(user, cancellationToken);
        }

        public async Task<Postagem> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return await PostagemRepository.FindByIdAsync(userId, cancellationToken);
        }

        public async Task<Postagem> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return await PostagemRepository.FindByNameAsync(normalizedUserName, cancellationToken);
        }

        public async Task<string> GetDescricaoAsync(Postagem user, CancellationToken cancellationToken)
        {
            return await PostagemRepository.GetDescricaoAsync(user, cancellationToken);
        }

        public async Task<string> GetFotoPostUrlAsync(Postagem user, CancellationToken cancellationToken)
        {
            return await PostagemRepository.GetFotoPostUrlAsync(user, cancellationToken);
        }

        public async Task<IList<Comentario>> GetIDs_ComentariosAsync(Postagem user, CancellationToken cancellationToken)
        {
            return await PostagemRepository.GetIDs_ComentariosAsync(user, cancellationToken);
        }

        public async Task<Domain.Account.Account> GetID_AccountAsync(Postagem user, CancellationToken cancellationToken)
        {
            return await PostagemRepository.GetID_AccountAsync(user, cancellationToken);
        }

        public async Task<string> GetNormalizedUserNameAsync(Postagem user, CancellationToken cancellationToken)
        {
            return await PostagemRepository.GetNormalizedUserNameAsync(user, cancellationToken);
        }

        public async Task<string> GetUserIdAsync(Postagem user, CancellationToken cancellationToken)
        {
            return await PostagemRepository.GetUserIdAsync(user, cancellationToken);
        }

        public async Task<string> GetUserNameAsync(Postagem user, CancellationToken cancellationToken)
        {
            return await PostagemRepository.GetUserNameAsync(user, cancellationToken);
        }

        public async Task SetNormalizedUserNameAsync(Postagem user, string normalizedName, CancellationToken cancellationToken)
        {
            await PostagemRepository.SetNormalizedUserNameAsync(user, normalizedName, cancellationToken);
        }

        public async Task SetUserNameAsync(Postagem user, string userName, CancellationToken cancellationToken)
        {
            await PostagemRepository.SetUserNameAsync(user, userName, cancellationToken);
        }

        public async Task<IdentityResult> UpdateAsync(Postagem user, CancellationToken cancellationToken)
        {
            return await PostagemRepository.UpdateAsync(user, cancellationToken);
        }

        public async Task<IEnumerable<Postagem>> GetPostsAsync()
        {
            return await PostagemRepository.GetPostsAsync();
        }

        public async Task<IList<Postagem>> GetPostByAccountAsync(Guid id, CancellationToken cancellationToken)
        {
            return await PostagemRepository.GetPostByAccountAsync(id, cancellationToken);
        }
    }
}
