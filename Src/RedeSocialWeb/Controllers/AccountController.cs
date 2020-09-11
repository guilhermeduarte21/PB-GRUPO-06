using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RedeSocial.Domain.Account;
using RedeSocial.Repository.Account;
using RedeSocial.Services.Account;
using RedeSocialWeb.ViewModel.Account;

namespace RedeSocialWeb.Controllers
{
    public class AccountController : Controller
    {
        private IAccountService AccountService { get; set; }
        private IAccountIdentityManager AccountIdentityManager { get; set; }

        private readonly ILogger<AccountController> _logger;

        public AccountController(IAccountService accountService, IAccountIdentityManager accountIdentityManager, ILogger<AccountController> logger)
        {
            this.AccountService = accountService;
            this.AccountIdentityManager = accountIdentityManager;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(string returnUrl = "")
        {
            ViewBag.ReturnUrl = returnUrl;
            if (this.User.Identity.IsAuthenticated)
                AccountIdentityManager.Logout();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            try
            {
                var result = await this.AccountIdentityManager.Login(model.UserName, model.Password);

                if (!result.Succeeded)
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
            if (this.User.Identity.IsAuthenticated)
                AccountIdentityManager.Logout();

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
                        Name = model.Name,
                        UserName = model.UserName,
                        Email = model.Email,
                        DtBirthday = model.DtBirthday,
                        Password = model.Password
                    };
                    var result = await AccountService.CreateAsync(user, default);

                    if (result.Succeeded)
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
        public IActionResult Logout()
        {
            if (this.User.Identity.IsAuthenticated)
                AccountIdentityManager.Logout();

            return View();
        }
    }
}
