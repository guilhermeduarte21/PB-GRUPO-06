using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using RedeSocial.Web.Models.Account;
using RedeSocial.Web.Models.Perfil;
using RedeSocial.Web.Models.Post;
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

        public async Task<IdentityResult> CreateAccountAsync(RegisterViewModel user)
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

        public async Task<AccountViewModel> FindByIDAsync(string id)
        {
            var response = await _httpClient.GetAsync("accounts/getid/" + id);

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

        public async Task<String> LoginAsync(LoginViewModel loginViewModel)
        {
            var loginRequestJson = JsonConvert.SerializeObject(loginViewModel);

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

        public async Task<IdentityResult> UpdatePerfilAsync(PerfilEditViewModel user)
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
        public async Task<IdentityResult> UpdateAccountAsync(AccountViewModel user)
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

        public async Task<IdentityResult> CreatePostAsync(Guid id, PostCreateViewModel post)
        {
            var requestJson = JsonConvert.SerializeObject(post);

            var conteudo = new StringContent(requestJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("Accounts/" + id + "/posts", conteudo);

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

        public async Task Logout()
        {
            await _httpClient.GetAsync("accounts/logout");
        }

        public async Task<List<PostViewModel>> GetPostByAccountAsync(Guid id)
        {
            var response = await _httpClient.GetAsync("accounts/" + id + "/posts");

            List<PostViewModel> listPostViewModel = null;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();

                listPostViewModel = JsonConvert.DeserializeObject<List<PostViewModel>>(content);
            }

            response.EnsureSuccessStatusCode();

            return listPostViewModel;
        }

        public async Task<List<AccountSimplesViewModel>> GetFindAccountsAsync(string nome)
        {
            var response = await _httpClient.GetAsync("accounts/pesquisa/" + nome);

            List<AccountSimplesViewModel> listaccountsSimplesViewModel = null;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();

                listaccountsSimplesViewModel = JsonConvert.DeserializeObject<List<AccountSimplesViewModel>>(content);
            }

            response.EnsureSuccessStatusCode();

            return listaccountsSimplesViewModel;
        }
    }
}
