using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALIS_Models.ViewModels
{
    public class ProductVM
    {
        public Person Person { get; set; }
        public IEnumerable<SelectListItem> RoleSelectList { get; set; }        
    }
}
