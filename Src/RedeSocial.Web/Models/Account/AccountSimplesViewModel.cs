using System;

namespace RedeSocial.Web.Models.Account
{
    public class AccountSimplesViewModel
    {
        public Guid ID { get; set; }
        public string UserName { get; set; }
        public string FotoPerfilUrl { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
