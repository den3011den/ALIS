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
    public class PersonController : Controller
    {
        private readonly IPersonRepository _personRepo;

        public PersonController(IPersonRepository personRepo)
        {
            _personRepo = personRepo;
        }
        public IActionResult Index()
        {

            return View();
        }


        #region API CALLS
        [HttpGet]
        public IActionResult GetPersonList()
        {
            return Json(new { data = _personRepo.GetAll() });
        }
        #endregion
    }
}
