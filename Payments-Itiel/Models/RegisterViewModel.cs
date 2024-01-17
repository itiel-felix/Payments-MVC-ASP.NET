using System.ComponentModel.DataAnnotations;

namespace Payments_Itiel.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Field {0} is required.")]
        [EmailAddress(ErrorMessage = "It must be a valid email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Field {0} is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
