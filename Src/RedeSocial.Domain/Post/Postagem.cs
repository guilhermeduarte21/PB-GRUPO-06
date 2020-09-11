using RedeSocial.Domain.Account;
using RedeSocial.Domain.Profile;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedeSocial.Domain.Post
{
    public class Postagem
    {
        public Guid ID { get; set; }
        public string Descricao { get; set; }
        public string FotoPostUrl { get; set; }

        public Perfil ID_Perfil { get; set; } //UMA POSTAGEM TEM UM PERFIL (QUEM POSTOU)
        public virtual IList<Comentario> IDs_Comentarios { get; set; } //UMA POSTAGEM TEM MUITOS COMENTARIOS
    }
}
