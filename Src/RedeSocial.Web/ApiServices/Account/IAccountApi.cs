using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RedeSocial.Domain.ViewModel;
using RedeSocial.Web.ViewModel.Account;
using RedeSocial.Web.ViewModel.Perfil;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RedeSocial.Web.ApiServices.Account
{
    public interface IAccountApi
    {
        Task<string> LoginAsync(LoginRequest loginRequest);
        Task<IdentityResult> CreateAsync(Domain.Account.Account user);
        Task<AccountViewModel> FindByUserNameAsync(string userId);
        Task<PerfilEditViewModel> GetPerfilToUpdate(string userName);
        Task<IdentityResult> UpdateAsync(PerfilEditViewModel user);
        void AuthenticationHeader(string token);
        Task Logout();
    }
}
