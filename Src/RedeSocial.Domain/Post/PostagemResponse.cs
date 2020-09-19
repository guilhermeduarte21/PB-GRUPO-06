using RedeSocial.Domain.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedeSocial.Domain.Post
{
    public class PostagemResponse
    {
        public Guid ID { get; set; }
        public string Descricao { get; set; }
        public DateTime DataPostagem { get; set; }
        public string FotoPostUrl { get; set; }

        public virtual AccountSimplesResponse ID_Account { get; set; }
        public virtual IList<Comentario> IDs_Comentarios { get; set; } = new List<Comentario>();
    }
}
