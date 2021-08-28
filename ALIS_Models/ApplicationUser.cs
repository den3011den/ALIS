using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ALIS_Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
       public string FullName { get; set; }
    }
}
