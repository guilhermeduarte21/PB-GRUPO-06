using Microsoft.AspNetCore.Identity;
using RedeSocial.Web.Models.Account;
using RedeSocial.Web.Models.Perfil;
using RedeSocial.Web.Models.Post;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedeSocial.Web.ApiServices.Account
{
    public interface IAccountApi
    {
        Task<string> LoginAsync(LoginViewModel loginRequest);
        Task<IdentityResult> CreateAccountAsync(RegisterViewModel user);
        Task<AccountViewModel> FindByUserNameAsync(string userName);
        Task<AccountViewModel> FindByIDAsync(string id);

        Task<PerfilEditViewModel> GetPerfilToUpdate(string userName);
        Task<IdentityResult> UpdatePerfilAsync(PerfilEditViewModel user);
        Task<IdentityResult> UpdateAccountAsync(AccountViewModel user);

        void AuthenticationHeader(string token);

        Task<List<PostViewModel>> GetPostByAccountAsync(Guid id);
        Task<List<AccountSimplesViewModel>> GetFindAccountsAsync(string nome);

        Task<IdentityResult> CreatePostAsync(Guid id, PostCreateViewModel post);
        //Task<IdentityResult> EditPostAsync(PostCreateViewModel post);
        //Task<IdentityResult> DeletePostAsync(PostCreateViewModel post);

        Task Logout();
    }
}
