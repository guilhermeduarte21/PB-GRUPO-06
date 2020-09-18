using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RedeSocial.Services.Account;
using RedeSocial.Web.ApiServices.Account;
using RedeSocial.Web.Models;

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

        [HttpGet]
        public IActionResult Logout()
        {
            _accountIdentityManager.Logout();

            return Redirect("/");
        }
    }
}
