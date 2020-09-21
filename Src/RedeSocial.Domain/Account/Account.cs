
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Identity;
using RedeSocial.Domain.Post;

namespace RedeSocial.Domain.Account
{
    public class Account
    {
        [Key]
        public Guid ID { get; set; }
        public string UserName { get; set; }
        public String Email { get; set; }       
        public String Password { get; set; }

        public string FotoPerfilUrl { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public DateTime DataNascimento { get; set; }

        public virtual Role ID_Role { get; set; } //UMA CONTA TEM UMA ROLE | UMA ROLE TEM MUITAS CONTAS (M : 1)


        public virtual IList<Account> IDs_Seguidores { get; set; } = new List<Account>(); //UM PERFIL TEM MUITOS SEGUIDORES
        public virtual IList<Account> IDs_Seguindo { get; set; } = new List<Account>(); //UM PERFIL PODE SEGUIR MUITOS PERFIS
        public virtual IList<Postagem> IDs_Postagens { get; set; } = new List<Postagem>(); //UM PERFIL TEM MUITAS POSTAGENS
    }
}
