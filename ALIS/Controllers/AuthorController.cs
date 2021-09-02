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
    public class AuthorController : Controller
    {

        private readonly IAuthorRepository _authorRepo;

        public AuthorController(IAuthorRepository authorRepo)
        {
            _authorRepo = authorRepo;
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
        public IActionResult Create(Author obj)
        {
            if (ModelState.IsValid)
            {
                Author isExistsThisName = _authorRepo.FirstOrDefault(u => u.Name.ToLower() == obj.Name.ToLower());
                if (isExistsThisName != null)
                {
                    TempData[WC.Error] = "Автор с наименованием '" + obj.Name + "' уже существует";
                    return View(obj);
                }

                _authorRepo.Add(obj);
                _authorRepo.Save();
                TempData[WC.Success] = "Автор '" + obj.Name + "' успешно создан";
                return RedirectToAction("Index");
            }
            TempData[WC.Error] = "Не удалось создать автора '" + obj.Name + "'";

            // TODO: После добавления записи восстанавливать строку поиска в списке Index
            // TODO: После добавления записи восстанавливать checkbox "Показывать архивные записи"
            // TODO: После добавления записи устанавливать выбор в списке Index на созданую запись

            return View(obj);
        }


        //EDIT - GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _authorRepo.Find(id.GetValueOrDefault());

            if (id == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Author obj)
        {
            if (ModelState.IsValid)
            {

                Author isExistsThisName = _authorRepo.FirstOrDefault(u => u.Name.ToLower() == obj.Name.ToLower() && u.Id != obj.Id);
                if (isExistsThisName != null)
                {
                    TempData[WC.Error] = "Автор с наименованием '" + obj.Name + "' уже существует";
                    return View(obj);
                }


                _authorRepo.Update(obj, UpdateMode.Update);
                _authorRepo.Save();

                TempData[WC.Success] = "Автор '" + obj.Name + "' успешно обновлен";
                return RedirectToAction("Index");
            }

            // TODO: После редактирования записи восстанавливать строку поиска в списке Index
            // TODO: После редактирования записи восстанавливать checkbox "Показывать архивные записи"
            // TODO: После редактирования записи устанавливать выбор в списке Index на отредактированную запись

            return View(obj);
        }


        //GET - DELETE
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _authorRepo.Find(id.GetValueOrDefault());

            if (id == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //POST - DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Author obj)
        {

            if (ModelState.IsValid)
            {
                _authorRepo.Update(obj, UpdateMode.MoveToArchive);
                _authorRepo.Save();

                TempData[WC.Success] = "Автор '" + obj.Name + "' успешно удалён в архив";
                return RedirectToAction("Index");
            }

            // TODO: После удаления записи восстанавливать строку поиска в списке Index
            // TODO: После удаления записи восстанавливать checkbox "Показывать архивные записи"
            // TODO: После удаления записи, если checkbox "Показывать архивные записи" включен, устанавливать выбор в списке Index на удалённую в архив запись


            return View(obj);


        }

        //GET - RESTORE
        public IActionResult Restore(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _authorRepo.Find(id.GetValueOrDefault());

            if (id == null)
            {
                return NotFound();
            }


            return View(obj);
        }


        //POST - Restore
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Restore(Author obj)
        {

            if (ModelState.IsValid)
            {
                _authorRepo.Update(obj, UpdateMode.RestoreFromArchive);
                _authorRepo.Save();

                TempData[WC.Success] = "Автор '" + obj.Name + "' успешно восстановлен из архива";
                return RedirectToAction("Index");
            }

            // TODO: После восстановления из архива записи восстанавливать строку поиска в списке Index
            // TODO: После восстановления из архива записи восстанавливать checkbox "Показывать архивные записи"
            // TODO: После восстановления из архива записи устанавливать выбор в списке Index на удалённую в архив запись


            return View(obj);

        }



        #region API CALLS
        [HttpGet]
        public IActionResult GetAuthorList()
        {
            return Json(new { data = _authorRepo.GetAll() });
        }
        #endregion
    }
}
