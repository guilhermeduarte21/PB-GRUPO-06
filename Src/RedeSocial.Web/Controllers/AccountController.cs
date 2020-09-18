using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RedeSocial.Web.Models.Account;
using RedeSocial.Web.ApiServices.Account;
using RedeSocial.Services.Account;

namespace RedeSocial.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountApi _accountApi;
        private readonly IAccountIdentityManager _accountIdentityManager;

        public AccountController(IAccountApi accountApi, IAccountIdentityManager accountIdentityManager)
        {
            _accountApi = accountApi;
            _accountIdentityManager = accountIdentityManager;
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
                //Services
                var aut = await _accountIdentityManager.Login(model.UserName, model.Password);
                //Api
                var token  = await _accountApi.LoginAsync(model);

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
                    var response = await _accountApi.CreateAccountAsync(model);

                    if (response.Succeeded)
                    {
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
    }
}
