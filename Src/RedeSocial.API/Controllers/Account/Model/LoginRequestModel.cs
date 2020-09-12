using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RedeSocial.API.Controllers.Account.Model
{
    public class LoginRequestModel
    {
        [Required(ErrorMessage = "Email é um campo obrigátorio")]
        [EmailAddress(ErrorMessage = "Email não está em um formato correto")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password é um campo obrigátorio")]
        public string Password { get; set; }
    }
}
