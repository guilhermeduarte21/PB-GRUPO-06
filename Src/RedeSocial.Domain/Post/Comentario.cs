using RedeSocial.Domain.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedeSocial.Domain.Post
{
    public class Comentario
    {
        public Guid ID { get; set; }
        public string Descricao { get; set; }

        public Account.Account ID_Account { get; set; } //UM COMENTARIO TEM UM PERFIL (QUEM COMENTOU)
        public Postagem ID_Postagem { get; set; } //NÃO SEI SE PRECISARIA RELACIONAR O COMENTARIO A UMA POSTAGEM
    }
}