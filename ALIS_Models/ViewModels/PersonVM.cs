using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ALIS_Models.ViewModels
{
    public class PersonVM
    {
        public int PersonId { get; set; }
        public string PersonName { get; set; }

        [Required(ErrorMessage = "Введите Email")]
        public string Email { get; set; }

        public IEnumerable<SelectListItem> RoleList { get; set; }
        [Required(ErrorMessage = "Выбор роли обязателен")]
        public IdentityRole Role { get; set; }

    }
}
