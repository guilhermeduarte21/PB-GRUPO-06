using RedeSocial.Domain.Account;
using RedeSocial.Web.Models.Post;
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

        public virtual Role ID_Role { get; set; }


        public virtual IList<AccountSimplesResponse> IDs_Seguidores { get; set; } = new List<AccountSimplesResponse>();
        public virtual IList<AccountSimplesResponse> IDs_Seguindo { get; set; } = new List<AccountSimplesResponse>();
        public virtual List<PostViewModel> IDs_Postagens { get; set; } = new List<PostViewModel>();
        public virtual List<AccountSimplesViewModel> Accounts_Busca { get; set; } = new List<AccountSimplesViewModel>();
    }
}
