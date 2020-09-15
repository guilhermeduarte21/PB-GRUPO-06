using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RedeSocial.Domain.ViewModel
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Username/Email é um campo obrigátorio")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Senha é um campo obrigátorio")]
        public string Password { get; set; }
    }
}
