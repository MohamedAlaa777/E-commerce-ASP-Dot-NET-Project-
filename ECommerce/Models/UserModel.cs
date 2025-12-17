using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models
{
    public class UserModel
    {
        [Required(ErrorMessage ="Required")]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
