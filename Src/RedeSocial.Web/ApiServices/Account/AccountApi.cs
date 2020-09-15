using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using RedeSocial.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RedeSocial.Web.ApiServices.Account
{
    public class AccountApi : IAccountApi
    {
        private readonly HttpClient _httpClient;

        public AccountApi()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.BaseAddress = new Uri("https://localhost:5001/");
        }

        public async Task<IdentityResult> CreateAsync(Domain.Account.Account user)
        {
            var userJson = JsonConvert.SerializeObject(user);

            var conteudo = new StringContent(userJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/accounts", conteudo);

            if (response.IsSuccessStatusCode)
            {
                return IdentityResult.Success;
            }

            return IdentityResult.Failed();
        }

        public async Task<SignInResult> LoginAsync(LoginRequest loginRequest)
        {
            var loginRequestJson = JsonConvert.SerializeObject(loginRequest);

            var conteudo = new StringContent(loginRequestJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/accounts/login", conteudo);

            if (response.IsSuccessStatusCode)
            {
                return SignInResult.Success;
            }

            return SignInResult.Failed;
        }

        public async Task Logout()
        {
            await _httpClient.GetAsync("api/logout");
        }
    }
}
