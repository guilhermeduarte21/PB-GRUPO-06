using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RedeSocialWeb.ViewModel.Account
{
    public class RegisterViewModel
    {
        [Required]
        public String Name { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public String Email { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DtBirthday { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public String ConfirmPassword { get; set; }
    }
}
