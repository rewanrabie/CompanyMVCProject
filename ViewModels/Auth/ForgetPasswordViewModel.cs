using System.ComponentModel.DataAnnotations;

namespace MVC_3.ViewModels.Auth
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "Email Is Required ! ! ")]
        [EmailAddress(ErrorMessage = "InValid Email")]
        public string Email { get; set; }
    }
}
