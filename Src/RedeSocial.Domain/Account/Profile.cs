using System;
using System.Collections.Generic;
using System.Text;

namespace RedeSocial.Domain.Account
{
    public class Profile
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public DateTime DataAniversario { get; set; }
        public string PictureUrl { get; set; }

        public Account Account { get; set; }
    }
}
