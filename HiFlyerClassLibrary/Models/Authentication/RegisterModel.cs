﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiFlyerClassLibrary.Models.Authentication
{
    public class RegisterModel
    {
        [Display(Name = "first name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "last name")]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "email")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "password")]
        [Required]
        public string Password { get; set; }

        [Display(Name = "confirm password")]
        [Required]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        public bool AcceptsMarketing { get; set; }

        public bool isTrue
        { get { return true; } }

        [Required]
        [Compare("isTrue", ErrorMessage = "Please agree to Terms and Conditions")]
        public bool AcceptsTerms { get; set; }
    }
}
