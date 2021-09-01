using ALIS_DataAccess.Repository;
using ALIS_Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALIS.Controllers
{
    [Authorize(Roles = WC.AdminRole)]
    public class AuthorTypeController : Controller
    {

        private readonly IAuthorTypeRepository _authorTypeRepo;

        public AuthorTypeController(IAuthorTypeRepository authorTypeRepo)
        {
            _authorTypeRepo = authorTypeRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAuthorTypeList()
        {
            return Json(new { data = _authorTypeRepo.GetAll() });
        }
        #endregion
    }
}
