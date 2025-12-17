using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models
{
    public class RoleFormViewModel
    {
        [Required, StringLength(256)]
        public string Name { get; set; }
    }
}
