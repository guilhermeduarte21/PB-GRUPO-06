using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RedeSocial.Services.Authenticate
{
    public interface IAuthenticateService
    {
        Task<string> Login(string userName, string password);
    }
}
