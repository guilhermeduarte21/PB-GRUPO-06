using RedeSocial.Domain.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedeSocial.Domain.Post
{
    public class Postagem
    {
        public Guid ID { get; set; }
        public string Descricao { get; set; }
        public DateTime DataPostagem { get; set; }
        public string FotoPostUrl { get; set; }

        public virtual Account.Account ID_Account { get; set; } //UMA POSTAGEM TEM UM PERFIL (QUEM POSTOU)
        public virtual IList<Comentario> IDs_Comentarios { get; set; } = new List<Comentario>(); //UMA POSTAGEM TEM MUITOS COMENTARIOS
    }
}
