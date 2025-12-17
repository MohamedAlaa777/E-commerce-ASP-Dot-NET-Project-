using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string firstName { get; set; } = null!;
        public string lastName { get; set; } = null!;
    }
}
