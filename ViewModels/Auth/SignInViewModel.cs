﻿using System.ComponentModel.DataAnnotations;

namespace MVC_3.ViewModels.Auth
{
    public class SignInViewModel
    {
        [Required(ErrorMessage = "Email Is Required ! ! ")]
        [EmailAddress(ErrorMessage = "InValid Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password Is Required ! ! ")]
        [DataType(DataType.Password)]
        [MaxLength (16)]
        [MinLength(4)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
