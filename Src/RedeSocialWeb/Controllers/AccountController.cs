using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RedeSocial.Domain.Account;
using RedeSocial.Domain.Profile;
using RedeSocial.API.Controllers;
using RedeSocial.Web.ViewModel.Account;
using RedeSocial.API.ViewModel;
using Newtonsoft.Json;
using RestSharp;

namespace RedeSocial.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Login(string returnUrl = "")
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest model, string returnUrl)
        {
            try
            {
                var client = new RestClient();
                var request = new RestRequest("https://localhost:5001/api/Autenticar/login", DataFormat.Json);
                request.AddJsonBody(model);

                var response = client.Post<Account>(request);

                if (response == null)
                {
                    ModelState.AddModelError(string.Empty, "Login ou senha inválidos");
                    return View(model);
                }

                if (!String.IsNullOrWhiteSpace(returnUrl))
                    return Redirect(returnUrl);

                return Redirect("/");
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Ocorreu um erro, por favor tente mais tarde.");
                return View(model);
            }
        }

        //GET: Account/Register
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new Account
                    {
                        UserName = model.UserName,
                        Email = model.Email,
                        Password = model.Password,
                        ID_Perfil = new Perfil
                        {
                            Nome = model.Name,
                            SobreNome = model.SobreNome,
                            DataNascimento = model.DtBirthday
                        }
                    };

                    var client = new RestClient();
                    var request = new RestRequest("https://localhost:5001/api/aluno", DataFormat.Json);
                    request.AddJsonBody(user);

                    var response = client.Post<Account>(request);

                    if (response != null)
                    {
                        _logger.LogInformation("Cadastrado com sucesso!");
                        return View(nameof(Success));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Email ou Username já existe!");
                        return View(model);
                    }
                }

                return View(model);
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Ocorreu um erro, por favor tente mais tarde.");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Success()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            var client = new RestClient();
            var request = new RestRequest("https://localhost:5001/api/conta/logout", Method.GET);

            request.RequestFormat = DataFormat.Json;

            return View();
        }
    }
}
