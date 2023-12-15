using System.ComponentModel.DataAnnotations;

namespace PL.Models.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MinLength(4, ErrorMessage = "MinLength is 4")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

    }
}
