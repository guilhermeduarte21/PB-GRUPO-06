using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RedeSocial.Services.Account;
using RedeSocialWeb.Models;

namespace RedeSocialWeb.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IAccountIdentityManager AccountIdentityManager { get; set; }
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IAccountIdentityManager accountIdentityManager)
        {
            _logger = logger;
            this.AccountIdentityManager = accountIdentityManager;
        }

        public IActionResult Index()
        {
            return View();
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

        [HttpGet]
        public IActionResult Logout()
        {
            if (this.User.Identity.IsAuthenticated)
                AccountIdentityManager.Logout();

            return Redirect("/");
        }
    }
}
