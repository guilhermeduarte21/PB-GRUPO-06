using RedeSocial.Domain.Account;
using RedeSocial.Domain.Post;
using System;
using System.Collections.Generic;

namespace RedeSocial.Web.Models.Account
{
    public class AccountViewModel
    {
        public Guid ID { get; set; }
        public string UserName { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }

        public string FotoPerfilUrl { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public DateTime DataNascimento { get; set; }

        public virtual Role ID_Role { get; set; } //UMA CONTA TEM UMA ROLE | UMA ROLE TEM MUITAS CONTAS (M : 1)


        public virtual IList<Domain.Account.Account> IDs_Seguidores { get; set; } //UM PERFIL TEM MUITOS SEGUIDORES
        public virtual IList<Domain.Account.Account> IDs_Seguindo { get; set; } //UM PERFIL PODE SEGUIR MUITOS PERFIS
        public virtual IList<Postagem> IDs_Postagens { get; set; } = new List<Postagem>(); //UM PERFIL TEM MUITAS POSTAGENS
    }
}
