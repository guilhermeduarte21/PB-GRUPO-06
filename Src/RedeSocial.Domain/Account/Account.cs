
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using RedeSocial.Domain.Profile;

namespace RedeSocial.Domain.Account
{
    public class Account
    {
        public Guid ID { get; set; }
        public string UserName { get; set; }
        public String Email { get; set; }       
        public String Password { get; set; }

        public virtual Perfil ID_Perfil { get; set; } //UMA CONTA TEM UM PERFIL | UM PERFIL TEM UMA CONTA (1 : 1)
        public virtual Role ID_Role { get; set; } //UMA CONTA TEM UMA ROLE | UMA ROLE TEM MUITAS CONTAS (M : 1)
    }
}
