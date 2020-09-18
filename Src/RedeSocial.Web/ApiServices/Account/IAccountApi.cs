using Microsoft.AspNetCore.Identity;
using RedeSocial.Web.Models.Account;
using RedeSocial.Web.Models.Perfil;
using RedeSocial.Web.Models.Post;
using System;
using System.Threading.Tasks;

namespace RedeSocial.Web.ApiServices.Account
{
    public interface IAccountApi
    {
        Task<string> LoginAsync(LoginViewModel loginRequest);
        Task<IdentityResult> CreateAccountAsync(RegisterViewModel user);
        Task<AccountViewModel> FindByUserNameAsync(string userName);

        Task<PerfilEditViewModel> GetPerfilToUpdate(string userName);
        Task<IdentityResult> UpdateAsync(PerfilEditViewModel user);

        void AuthenticationHeader(string token);

        Task<IdentityResult> CreatePostAsync(PostCreateViewModel post);

        Task Logout();
    }
}
