using Microsoft.AspNetCore.Identity;
using RedeSocial.Domain.ViewModel;
using System.Threading.Tasks;

namespace RedeSocial.Web.ApiServices.Account
{
    public interface IAccountApi
    {
        Task<SignInResult> LoginAsync(LoginRequest loginRequest);
        Task<IdentityResult> CreateAsync(Domain.Account.Account user);
        Task Logout();
    }
}
