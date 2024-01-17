using System.ComponentModel.DataAnnotations;

namespace Payments_Itiel.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Field {0} is required.")]
        [EmailAddress(ErrorMessage = "It must be a valid email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Field {0} is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}
