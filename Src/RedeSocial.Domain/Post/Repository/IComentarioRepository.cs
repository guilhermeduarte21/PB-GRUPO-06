using Microsoft.AspNetCore.Identity;
using RedeSocial.Domain.Account;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RedeSocial.Domain.Post
{
    public interface IComentarioRepository
    {
        Task<IdentityResult> CreateAsync(Comentario user, CancellationToken cancellationToken);
        Task<IdentityResult> DeleteAsync(Comentario user, CancellationToken cancellationToken);
        Task<Comentario> FindByIdAsync(string userId, CancellationToken cancellationToken);
        Task<Comentario> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken);
        Task<string> GetNormalizedUserNameAsync(Comentario user, CancellationToken cancellationToken);
        Task<string> GetUserIdAsync(Comentario user, CancellationToken cancellationToken);
        Task<string> GetUserNameAsync(Comentario user, CancellationToken cancellationToken);
        Task SetNormalizedUserNameAsync(Comentario user, string normalizedName, CancellationToken cancellationToken);
        Task SetUserNameAsync(Comentario user, string userName, CancellationToken cancellationToken);
        Task<IdentityResult> UpdateAsync(Comentario user, CancellationToken cancellationToken);
        Task<string> GetDescricaoAsync(Comentario user, CancellationToken cancellationToken);
        Task<Account.Account> GetID_AccountAsync(Comentario user, CancellationToken cancellationToken);
        Task<Postagem> GetID_PostagemAsync(Comentario user, CancellationToken cancellationToken);
    }
}
