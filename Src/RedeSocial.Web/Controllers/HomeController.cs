using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Microsoft.Extensions.Logging;
using RedeSocial.CrossCutting.UploadImg;
using RedeSocial.Services.Account;
using RedeSocial.Web.ApiServices.Account;
using RedeSocial.Web.Models;
using RedeSocial.Web.Models.Post;

namespace RedeSocial.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IAccountApi _accountApi;
        private readonly ILogger<HomeController> _logger;
        private readonly IAccountIdentityManager _accountIdentityManager;
        public string UserName => this.User.Identity.Name;

        public HomeController(ILogger<HomeController> logger, IAccountApi accountApi, IAccountIdentityManager accountIdentityManager)
        {
            _logger = logger;
            _accountApi = accountApi;
            _accountIdentityManager = accountIdentityManager;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _accountApi.FindByUserNameAsync(UserName);

            return View(response);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Perfil()
        {
            return Redirect("/Perfil");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile imagem, string message)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string urlImg = null;

                    if (imagem != null)
                    {
                        urlImg = Upload.UploadFoto(imagem.OpenReadStream(), new GuidValueGenerator().ToString(), "fotos-post");
                    }

                    var post = new PostCreateViewModel
                    {
                        Descricao = message,
                        DataPostagem = DateTime.Now,
                        FotoPostUrl = urlImg
                    };

                    var response = await _accountApi.CreatePostAsync(post);

                    if (response.Succeeded)
                    {
                        _logger.LogInformation("Postado com sucesso!");
                        return Redirect("/");
                    }
                    else
                    {
                        return Redirect("/");
                    }
                }

                return Redirect("/");
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Ocorreu um erro, por favor tente mais tarde.");
                return View();
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            _accountIdentityManager.Logout();

            return Redirect("/");
        }
    }
}
