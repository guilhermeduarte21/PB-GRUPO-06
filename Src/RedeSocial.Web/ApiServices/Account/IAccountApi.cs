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

        Task<PerfilEditViewModel> GetPerfilToUpdate(string userName);
        Task<IdentityResult> UpdateAsync(PerfilEditViewModel user);

        void AuthenticationHeader(string token);

        Task<List<PostViewModel>> GetPostByAccountAsync(Guid id);

        Task<IdentityResult> CreatePostAsync(Guid id, PostCreateViewModel post);
        //Task<IdentityResult> EditPostAsync(PostCreateViewModel post);
        //Task<IdentityResult> DeletePostAsync(PostCreateViewModel post);

        Task Logout();
    }
}
