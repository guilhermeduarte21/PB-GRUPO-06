using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RedeSocial.CrossCutting.RegexExemple;
using RedeSocial.Domain.Account.Repository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RedeSocial.Services.Account
{
    public class AccountIdentityManager : IAccountIdentityManager
    {
        private IAccountRepository Repository { get; set; }
        private SignInManager<Domain.Account.Account> SignInManager { get; set; }
        private IConfiguration _configurantion { get; set; }

        Domain.Account.Account account;

        public AccountIdentityManager(IAccountRepository accountRepository, SignInManager<Domain.Account.Account> signInManager,
                                                                                IConfiguration configuration)
        {
            this.Repository = accountRepository;
            this.SignInManager = signInManager;
            _configurantion = configuration;
        }

        public async Task<String> Login(string userName, string password)
        {
            if (RegexUtilities.IsValidEmail(userName))
            {
                account = await this.Repository.GetAccountByEmailPassword(userName, password);
            }
            else
            {
                account = await this.Repository.GetAccountByUserNamePassword(userName, password);
            }

            if(account == null)
            {
                return null;
            }

            await SignInManager.SignInAsync(account, false);

            return CreateToken(account);
        }

        private string CreateToken(Domain.Account.Account account)
        {
            var key = Encoding.UTF8.GetBytes("2CDJT95DNHCGENGF5432418VFNJ37FN598FMD83425XMNXGVATWPTLV94348DH");

            var claims = new List<Claim>();

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, account.ID.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, account.UserName));
            claims.Add(new Claim(ClaimTypes.Email, account.Email));

            claims.Add(new Claim(ClaimTypes.Role, "USUARIO"));

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
                Audience = "ACCOUNTS-API",
                Issuer = "ACCOUNTS-API"
            };

            var securityToken = tokenHandler.CreateToken(tokenDescription);

            var token = tokenHandler.WriteToken(securityToken);

            return token;
        }

        public async Task Logout()
        {
            await this.SignInManager.SignOutAsync();
        }
    }
}
