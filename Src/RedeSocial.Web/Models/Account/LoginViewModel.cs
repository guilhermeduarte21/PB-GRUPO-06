using System.ComponentModel.DataAnnotations;

namespace RedeSocial.Web.Models.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "UserName/Email é um campo obrigátorio")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password é um campo obrigátorio")]
        public string Password { get; set; }
    }
}
