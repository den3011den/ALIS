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
    public class GenreController : Controller
    {

        private readonly IGenreRepository _genreRepo;

        public GenreController(IGenreRepository genreRepo)
        {
            _genreRepo = genreRepo;
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
        public IActionResult Create(Genre obj)
        {
            if (ModelState.IsValid)
            {
                Genre isExistsThisName = _genreRepo.FirstOrDefault(u => u.Name.ToLower() == obj.Name.ToLower());
                if (isExistsThisName != null)
                {
                    TempData[WC.Error] = "Жанр с наименованием '" + obj.Name + "' уже существует";
                    return View(obj);
                }

                _genreRepo.Add(obj);
                _genreRepo.Save();
                TempData[WC.Success] = "Жанр '" + obj.Name + "' успешно создан";
                return RedirectToAction("Index");
            }
            TempData[WC.Error] = "Не удалось создать жанр '" + obj.Name + "'";

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

            var obj = _genreRepo.Find(id.GetValueOrDefault());

            if (id == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Genre obj)
        {
            if (ModelState.IsValid)
            {
                _genreRepo.Update(obj, UpdateMode.Update);
                _genreRepo.Save();

                TempData[WC.Success] = "Жанр '" + obj.Name + "' успешно обновлен";
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

            var obj = _genreRepo.Find(id.GetValueOrDefault());

            if (id == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //POST - DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Genre obj)
        {

            if (ModelState.IsValid)
            {
                _genreRepo.Update(obj, UpdateMode.MoveToArchive);
                _genreRepo.Save();

                TempData[WC.Success] = "Жанр '" + obj.Name + "' успешно удалён в архив";
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

            var obj = _genreRepo.Find(id.GetValueOrDefault());

            if (id == null)
            {
                return NotFound();
            }


            return View(obj);
        }


        //POST - Restore
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Restore(Genre obj)
        {

            if (ModelState.IsValid)
            {
                _genreRepo.Update(obj, UpdateMode.RestoreFromArchive);
                _genreRepo.Save();

                TempData[WC.Success] = "Жанр '" + obj.Name + "' успешно восстановлен из архива";
                return RedirectToAction("Index");
            }

            // TODO: После восстановления из архива записи восстанавливать строку поиска в списке Index
            // TODO: После восстановления из архива записи восстанавливать checkbox "Показывать архивные записи"
            // TODO: После восстановления из архива записи устанавливать выбор в списке Index на удалённую в архив запись


            return View(obj);

        }



        #region API CALLS
        [HttpGet]
        public IActionResult GetGenreList()
        {
            return Json(new { data = _genreRepo.GetAll() });
        }
        #endregion
    }
}
