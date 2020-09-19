using System;
using System.ComponentModel.DataAnnotations;

namespace RedeSocial.Web.Models.Account
{
    public class RegisterViewModel
    {
        [Required]
        public String Nome { get; set; }
        [Required]
        public String SobreNome { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public String Email { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public String ConfirmPassword { get; set; }
    }
}
