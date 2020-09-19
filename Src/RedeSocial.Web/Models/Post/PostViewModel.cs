using RedeSocial.Domain.Post;
using RedeSocial.Web.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedeSocial.Web.Models.Post
{
    public class PostViewModel
    {
        public Guid ID { get; set; }
        public string Descricao { get; set; }
        public DateTime DataPostagem { get; set; }
        public string FotoPostUrl { get; set; }

        public AccountViewModel ID_Account { get; set; } //UMA POSTAGEM TEM UM PERFIL (QUEM POSTOU)
        public virtual IList<Comentario> IDs_Comentarios { get; set; } = new List<Comentario>(); //UMA POSTAGEM TEM MUITOS COMENTARIOS
    }
}
