using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using RedeSocial.Domain.Account;
using RedeSocial.Domain.Post;

namespace RedeSocial.Web.Models.Post
{
    public class PostCreateViewModel
    {
        [Required]
        public string Descricao { get; set; }
        public DateTime DataPostagem { get; set; }
        public string FotoPostUrl { get; set; }
    }
}
