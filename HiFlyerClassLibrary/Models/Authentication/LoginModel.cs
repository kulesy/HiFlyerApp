using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiFlyerClassLibrary.Models.Authentication
{
    public class LoginModel
    {
        [Display(Name = "email")]
        [Required]
        public string Email { get; set; }

        [Display(Name = "password")]
        [Required]
        public string Password { get; set; }
    }
}
