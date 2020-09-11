using RedeSocial.Domain.Account;
using RedeSocial.Domain.Post;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedeSocial.Domain.Profile
{
    public class Perfil
    {
        public Guid ID { get; set; }
        public string FotoPerfilUrl { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public DateTime DataNascimento { get; set; }


        public virtual IList<Perfil> IDs_Seguidores { get; set; } //UM PERFIL TEM MUITOS SEGUIDORES
        public virtual IList<Perfil> IDs_Seguindo { get; set; } //UM PERFIL PODE SEGUIR MUITOS PERFIS
        public virtual IList<Postagem> IDs_Postagens { get; set; } //UM PERFIL TEM MUITAS POSTAGENS
    }
}
