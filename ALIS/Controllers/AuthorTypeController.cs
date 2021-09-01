using ALIS_DataAccess.Repository;
using ALIS_Models;
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

        // GET - Create
        public IActionResult Create()
        {
            return View();
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AuthorType obj)
        {
            if (ModelState.IsValid)
            {
                _authorTypeRepo.Add(obj);
                _authorTypeRepo.Save();
                TempData[WC.Success] = "Тип авторства '" + obj.Name + "' успешно создан";
                return RedirectToAction("Index");
            }
            TempData[WC.Error] = "Не удалось создать тип авторства '" + obj.Name + "'";
            return View(obj);
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
