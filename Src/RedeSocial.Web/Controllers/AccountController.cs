using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RedeSocial.Domain.Account;
using RedeSocial.Web.ViewModel.Account;
using RedeSocial.Web.ApiServices.Account;
using RedeSocial.Domain.ViewModel;
using RedeSocial.Services.Account;
using Microsoft.AspNetCore.Http;

namespace RedeSocial.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountApi _accountApi;
        private readonly IAccountIdentityManager _accountIdentityManager;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public AccountController(ILogger<AccountController> logger, IAccountApi accountApi, IAccountIdentityManager accountIdentityManager,
                                    IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _accountApi = accountApi;
            _accountIdentityManager = accountIdentityManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(string returnUrl = "")
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            try
            {
                var user = new LoginRequest
                {
                    UserName = model.UserName,
                    Password = model.Password,
                };

                //Services
                var aut = await _accountIdentityManager.Login(user.UserName, user.Password);
                //Api
                var token  = await _accountApi.LoginAsync(user);

                //Session["token"] = token;
                _session.SetString("token", token);

                if (aut == null)
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
        public IActionResult Register()
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
                        Nome = model.Name,
                        SobreNome = model.SobreNome,
                        DataNascimento = model.DtBirthday,
                        FotoPerfilUrl = "https://redesocialinfnet.blob.core.windows.net/fotos-perfil/perfil.png"
                    };

                    var response = await _accountApi.CreateAsync(user);

                    if (response.Succeeded)
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
            await _accountApi.Logout();

            return View();
        }

        public class TokenResult
        {
            public String Token { get; set; }
        }
    }
}
