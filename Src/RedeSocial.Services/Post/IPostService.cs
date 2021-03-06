﻿using Microsoft.AspNetCore.Identity;
using RedeSocial.Domain.Post;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RedeSocial.Services.Post
{
    public interface IPostService
    {
        Task<IdentityResult> CreateAsync(Postagem user, CancellationToken cancellationToken);
        Task<IdentityResult> DeleteAsync(Postagem user, CancellationToken cancellationToken);
        Task<Postagem> FindByIdAsync(string userId, CancellationToken cancellationToken);
        Task<Postagem> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken);
        Task<string> GetNormalizedUserNameAsync(Postagem user, CancellationToken cancellationToken);
        Task<string> GetUserIdAsync(Postagem user, CancellationToken cancellationToken);
        Task<string> GetUserNameAsync(Postagem user, CancellationToken cancellationToken);
        Task SetNormalizedUserNameAsync(Postagem user, string normalizedName, CancellationToken cancellationToken);
        Task SetUserNameAsync(Postagem user, string userName, CancellationToken cancellationToken);
        Task<IdentityResult> UpdateAsync(Postagem user, CancellationToken cancellationToken);
        Task<string> GetDescricaoAsync(Postagem user, CancellationToken cancellationToken);
        Task<string> GetFotoPostUrlAsync(Postagem user, CancellationToken cancellationToken);
        Task<Domain.Account.Account> GetID_AccountAsync(Postagem user, CancellationToken cancellationToken);
        Task<IList<Comentario>> GetIDs_ComentariosAsync(Postagem user, CancellationToken cancellationToken);
        Task<IEnumerable<Postagem>> GetPostsAsync();
        Task<IList<Postagem>> GetPostByAccountAsync(Guid id, CancellationToken cancellationToken);
    }
}
