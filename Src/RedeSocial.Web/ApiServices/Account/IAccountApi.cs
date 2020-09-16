using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RedeSocial.Domain.ViewModel;
using RedeSocial.Web.ViewModel.Account;
using System;
using System.Threading.Tasks;

namespace RedeSocial.Web.ApiServices.Account
{
    public interface IAccountApi
    {
        Task<string> LoginAsync(LoginRequest loginRequest);
        Task<IdentityResult> CreateAsync(Domain.Account.Account user);
        Task<AccountViewModel> FindByUserNameAsync(string userId);
        Task Logout();
    }
}
