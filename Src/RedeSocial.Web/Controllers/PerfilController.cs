using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using RedeSocial.CrossCutting.UploadImg;
using RedeSocial.Services.Account;
using RedeSocial.Web.ApiServices.Account;
using RedeSocial.Web.ViewModel.Perfil;

namespace RedeSocial.Web.Controllers
{
    [Authorize]
    public class PerfilController : Controller
    {
        private readonly IAccountApi _accountApi;
        private readonly IAccountIdentityManager _accountIdentityManager;
        public string UserName => this.User.Identity.Name;

        public PerfilController(IAccountApi accountApi, IAccountIdentityManager accountIdentityManager)
        {
            _accountApi = accountApi;
            _accountIdentityManager = accountIdentityManager;
        }

        // GET: PerfilController
        public async Task<IActionResult> Index()
        {
            var response = await _accountApi.FindByUserNameAsync(UserName);

            if (response == null)
                return RedirectToAction(nameof(Logout));

            return View(response);
        }

        // GET: PerfilController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PerfilController/Edit/5
        public async Task<IActionResult> Edit()
        {
            var response = await _accountApi.GetPerfilToUpdate(UserName);

            if (response == null)
                return RedirectToAction(nameof(Logout));

            return View(response);
        }

        // POST: PerfilController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PerfilEditViewModel perfil, IFormFile imagem, string nome, string sobrenome, DateTime datanascimento)
        {
            try
            {
                if (nome == null || imagem == null || sobrenome == null || datanascimento == null)
                    return View();

                string UrlImgame = Upload.UploadFoto(imagem.OpenReadStream(), UserName + "fotoPerfil", "fotos-perfil");
                var account = await _accountApi.FindByUserNameAsync(UserName);

                perfil.ID = account.ID;
                perfil.Nome = nome;
                perfil.SobreNome = sobrenome;
                perfil.DataNascimento = datanascimento;
                perfil.FotoPerfilUrl = UrlImgame;

                await _accountApi.UpdateAsync(perfil);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
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
