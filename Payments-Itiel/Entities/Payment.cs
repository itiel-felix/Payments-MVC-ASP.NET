using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Payments_Itiel.Entities
{
    public class Payment
    {

        public int ID { get; set; }
        [StringLength(250)]
        [Required]
        public string Title { get; set; }
        public string? Description { get; set; }
        public float Total {  get; set; }

        public string? UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }

    }
}
