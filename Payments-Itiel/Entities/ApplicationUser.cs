using Microsoft.AspNetCore.Identity;

namespace Payments_Itiel.Entities
{
    public class ApplicationUser :IdentityUser
    {
        public virtual ICollection<Payment>? Payments { get; set; }
    }
}
