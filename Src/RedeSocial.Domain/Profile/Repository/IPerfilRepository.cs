using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RedeSocial.Domain.Profile.Repository
{
    public interface IPerfilRepository
    {
        Task<IdentityResult> CreateAsync(Perfil user, CancellationToken cancellationToken);
        Task<IdentityResult> DeleteAsync(Perfil user, CancellationToken cancellationToken);
        Task<Perfil> FindByIdAsync(string userId, CancellationToken cancellationToken);
        Task<Perfil> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken);
        Task<string> GetNormalizedUserNameAsync(Perfil user, CancellationToken cancellationToken);
        Task<string> GetUserIdAsync(Perfil user, CancellationToken cancellationToken);
        Task<string> GetUserNameAsync(Perfil user, CancellationToken cancellationToken);
        Task<string> GetUserSobreNomeAsync(Perfil user, CancellationToken cancellationToken);
        Task SetNormalizedUserNameAsync(Perfil user, string normalizedName, CancellationToken cancellationToken);
        Task SetUserNameAsync(Perfil user, string userName, CancellationToken cancellationToken);
        Task SetSobreNomeAsync(Perfil user, string sobreNome, CancellationToken cancellationToken);
        Task<IdentityResult> UpdateAsync(Perfil user, CancellationToken cancellationToken);
    }
}
