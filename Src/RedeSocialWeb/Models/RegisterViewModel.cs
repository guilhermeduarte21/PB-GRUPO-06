using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RedeSocialWeb.Models
{
    public class RegisterViewModel
    {
        public String Name { get; set; }
        public string UserName { get; set; }
        public String Email { get; set; }
        public DateTime DtBirthday { get; set; }
        public String Password { get; set; }
        public String ConfirmPassword { get; set; }
    }
}
