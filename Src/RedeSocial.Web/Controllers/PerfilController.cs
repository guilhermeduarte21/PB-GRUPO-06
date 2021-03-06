﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedeSocial.CrossCutting.UploadImg;
using RedeSocial.Web.ApiServices.Account;
using RedeSocial.Web.Models.Perfil;

namespace RedeSocial.Web.Controllers
{
    [Authorize]
    public class PerfilController : Controller
    {
        private readonly IAccountApi _accountApi;
        public string UserName => this.User.Identity.Name;

        public PerfilController(IAccountApi accountApi)
        {
            _accountApi = accountApi;
        }

        // GET: PerfilController
        public async Task<IActionResult> Index()
        {
            var response = await _accountApi.FindByUserNameAsync(UserName);

            var list = await _accountApi.GetPostByAccountAsync(response.ID);
            var listSorted = list.OrderByDescending(x => x.DataPostagem).ToList();
            response.IDs_Postagens = listSorted;

            return View(response);
        }

        // GET: PerfilController/Edit/5
        public async Task<IActionResult> Edit()
        {
            var response = await _accountApi.GetPerfilToUpdate(UserName);

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

                await _accountApi.UpdatePerfilAsync(perfil);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
