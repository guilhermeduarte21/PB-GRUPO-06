using System;

namespace RedeSocial.Domain.Account
{
    public class AccountSimplesResponse
    {
        public Guid ID { get; set; }
        public string UserName { get; set; }
        public string FotoPerfilUrl { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
