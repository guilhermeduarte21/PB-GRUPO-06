using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RedeSocial.Domain.ViewModel;
using RedeSocial.Web.ViewModel.Account;
using RedeSocial.Web.ViewModel.Perfil;
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
            _httpClient.BaseAddress = new Uri("https://localhost:5001/api/");
        }

        public async Task<IdentityResult> CreateAsync(Domain.Account.Account user)
        {
            var userJson = JsonConvert.SerializeObject(user);

            var conteudo = new StringContent(userJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("accounts", conteudo);

            if (response.IsSuccessStatusCode)
            {
                return IdentityResult.Success;
            }

            return IdentityResult.Failed();
        }

        public async Task<AccountViewModel> FindByUserNameAsync(string userName)
        {
            var response = await _httpClient.GetAsync("accounts/" + userName);

            AccountViewModel accountViewModel = null;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();

                accountViewModel = JsonConvert.DeserializeObject<AccountViewModel>(content);
            }

            response.EnsureSuccessStatusCode();

            return accountViewModel;
        }

        public async Task<PerfilEditViewModel> GetPerfilToUpdate(string userName)
        {
            var response = await _httpClient.GetAsync("accounts/" + userName);

            PerfilEditViewModel perfilEditViewModel = null;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();

                perfilEditViewModel = JsonConvert.DeserializeObject<PerfilEditViewModel>(content);
            }

            response.EnsureSuccessStatusCode();

            return perfilEditViewModel;
        }

        public async Task<String> LoginAsync(LoginRequest loginRequest)
        {
            var loginRequestJson = JsonConvert.SerializeObject(loginRequest);

            var conteudo = new StringContent(loginRequestJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("Authenticates/Token", conteudo);
            var customerJsonString = await response.Content.ReadAsStringAsync();
            var deserialized = JsonConvert.DeserializeObject<TokenResult>(custome‌​rJsonString);

            AuthenticationHeader(deserialized.Token);

            if (response.IsSuccessStatusCode)
            {
                return deserialized.Token;
            }

            return null;
        }

        public async Task Logout()
        {
            await _httpClient.GetAsync("accounts/logout");
        }

        public async Task<IdentityResult> UpdateAsync(PerfilEditViewModel user)
        {
            var userJson = JsonConvert.SerializeObject(user);

            var conteudo = new StringContent(userJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("accounts/" + user.ID, conteudo);

            if (response.IsSuccessStatusCode)
            {
                return IdentityResult.Success;
            }

            return IdentityResult.Failed();
        }

        public class TokenResult
        {
            public String Token { get; set; }
        }

        public void AuthenticationHeader(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
