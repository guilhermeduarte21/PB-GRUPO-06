using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedeSocial.Web.ViewModel.Perfil
{
    public class PerfilEditViewModel
    {
        public Guid ID { get; set; }
        public string FotoPerfilUrl { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
