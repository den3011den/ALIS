using ALIS_DataAccess.Repository;
using ALIS_Models;
using ALIS_Models.ViewModels;
using ALIS_Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace ALIS.Controllers
{
    [Authorize(Roles = WC.AdminRole)]
    public class PersonController : Controller
    {
        private readonly IPersonRepository _personRepo;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        [BindProperty]
        public PersonVM PersonVM { get; set; }

        public PersonController(IPersonRepository personRepo, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager,
            IEmailSender emailSender)
        {
            _personRepo = personRepo;
            _roleManager = roleManager;
            _userManager = userManager;
            _emailSender = emailSender;
            PersonVM = new PersonVM();

        }
        public IActionResult Index()
        {

            return View();
        }

        public void GetRoles(string userId)
        {
            //PersonVM.RoleList = (IEnumerable<SelectListItem>)_roleManager.Roles.OrderBy(x => x.Name).ToList();


            PersonVM.RoleList = _personRepo.GetAllDropdownRoles();

            ApplicationUser user = (ApplicationUser)_userManager.FindByIdAsync(userId).GetAwaiter().GetResult();

            if (user != null)
            {
                if (_userManager.IsInRoleAsync(user, WC.AdminRole).GetAwaiter().GetResult())
                    PersonVM.Role = _roleManager.FindByNameAsync(WC.AdminRole).GetAwaiter().GetResult();
                if (_userManager.IsInRoleAsync(user, WC.ClientRole).GetAwaiter().GetResult())
                    PersonVM.Role = _roleManager.FindByNameAsync(WC.ClientRole).GetAwaiter().GetResult();
            }
            else
            {
                PersonVM.Role = _roleManager.FindByNameAsync(WC.ClientRole).GetAwaiter().GetResult();
            }
        }

        //GET - ActivateUserForPerson
        public IActionResult ActivateUserForPerson(int? id)
        {

            if (id == null || id == 0)
            {
                TempData[WC.Error] = "Нет ID персоны";
                return RedirectToAction("Index");

            }

            var person = _personRepo.Find(id.GetValueOrDefault());

            if (person == null)
            {
                TempData[WC.Error] = "Не удалось найти персону с ID: " + id.ToString();
                return RedirectToAction("Index");
            }

            if (person.IsArchive.GetValueOrDefault())
            {
                TempData[WC.Error] = "Персона в архиве. Для предоставления доступа в систему нужно восстановить персону из архива. " + id.ToString();
                return RedirectToAction("Index");
            }

            PersonVM.PersonId = person.Id;
            PersonVM.PersonName = person.Name + " " + person.Surname + " " + person.Patronymic;

            if (String.IsNullOrEmpty(person.UserId))
                PersonVM.Email = person.Email;
            else
                PersonVM.Email = _userManager.FindByIdAsync(person.UserId).GetAwaiter().GetResult().Email;

            GetRoles(person.UserId);

            return View(PersonVM);
        }

        //POST - ActivateUserForPerson
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ActivateUserForPerson(PersonVM PersonVM)
        {

            Person person = _personRepo.FirstOrDefault(u => u.Id == PersonVM.PersonId);
            if (ModelState.IsValid)
            {

                if (person == null)
                {
                    TempData[WC.Error] = "Персона не найдена!";
                    GetRoles("");
                    return View(PersonVM);
                }

                bool needMailConfirmation = false;
                bool needAddUser = false;
                ApplicationUser user = new ApplicationUser();

                if (!String.IsNullOrEmpty(person.UserId))
                {
                    user = (ApplicationUser)_userManager.FindByIdAsync(person.UserId).GetAwaiter().GetResult();

                    if (user == null)
                    {
                        TempData[WC.Error] = "Пользователь не найден!";
                        GetRoles(person.UserId);
                        return View(PersonVM);
                    }

                    user.FullName = person.Name + " " + person.Surname + " " + person.Patronymic;
                    user.LockoutEnd = null;

                    if (!String.Equals(PersonVM.Email.ToLower().Trim(), user.Email.ToLower().Trim()))
                    {
                        user.EmailConfirmed = false;
                        user.UserName = PersonVM.Email.ToLower().Trim();
                        needMailConfirmation = true;
                    }
                }
                else
                {
                    needAddUser = true;
                    needMailConfirmation = false;
                    user.EmailConfirmed = true;

                    user.FullName = person.Name + ' ' + person.Surname + ' ' + person.Patronymic;
                    user.Email = PersonVM.Email.Trim();
                    user.UserName = PersonVM.Email.Trim();
                    user.LockoutEnd = null;

                }

                if (needAddUser)
                {

                    try
                    {
                        _userManager.CreateAsync(user).GetAwaiter().GetResult();
                    }
                    catch (Exception expection)
                    {
                        TempData[WC.Error] = "Не удалось создать пользователя. Изменения не сохранены!";
                        GetRoles("");
                        return View(PersonVM);
                    }
                    user = (ApplicationUser)_userManager.FindByEmailAsync(user.Email).GetAwaiter().GetResult();
                }
                else
                {
                    _userManager.UpdateAsync(user).GetAwaiter().GetResult();
                }



                person.IsNotActive = false;

                string roleName = _roleManager.FindByIdAsync(PersonVM.Role.Id).GetAwaiter().GetResult().Name;
                var userRoles = _userManager.GetRolesAsync(user).GetAwaiter().GetResult();
                try
                {
                    if (userRoles.Count > 0)
                        _userManager.RemoveFromRolesAsync(user, userRoles).GetAwaiter().GetResult();


                    _userManager.AddToRoleAsync(user, roleName).GetAwaiter().GetResult(); ;

                }
                catch (Exception expection)
                {
                    TempData[WC.Error] = "При обработке ролей пользователя произошла ошибка. Изменения не сохранены!";
                    GetRoles(person.UserId);
                    return View(PersonVM);
                }


                person.UserId = user.Id;
                person.User = user;
                person.RoleName = roleName;
                try
                {
                    _personRepo.Update(person, UpdateMode.Update);
                    _personRepo.Save();
                }
                catch (Exception expection)
                {
                    TempData[WC.Error] = "При обновлении персоны произошла ошибка!";
                    GetRoles(person.UserId);
                    return View(PersonVM);
                }

                if (needAddUser)
                {

                    var code = _userManager.GeneratePasswordResetTokenAsync(user).GetAwaiter().GetResult();
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ResetPassword",
                        pageHandler: null,
                        values: new { area = "Identity", code },
                        protocol: Request.Scheme);

                    _emailSender.SendEmailAsync(
                        user.Email,
                        "ALIS: Вам предоставлен доступ в систему. Требуется установка пароля",
                        $"Администратор предоставил Вам доступ в систему. Для начала работы необходимо установить пароль для входа в систему, перейдя по <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>этой ссылке</a>. В качестве email и логина используйте '" + user.Email + "'");
                }

                //if (needMailConfirmation)
                //{

                //    var userId = user.Id;
                //    var code = _userManager.GenerateChangeEmailTokenAsync(user, user.Id).GetAwaiter().GetResult();
                //    var newEmail = user.Email;
                //    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                //    var callbackUrl = Url.Page(
                //        "/Account/ConfirmEmailChange",
                //        pageHandler: null,
                //        values: new { userId = userId, email = newEmail, code = code },
                //        protocol: Request.Scheme);
                //    _emailSender.SendEmailAsync(
                //    user.Email,
                //    "ALIS: Подтвердите Ваш email",
                //    $"При предоставлении доступа к системе Администратор установил или изменил email (он же логин) для Вашего аккаунта. Для подтверждения email <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>кликните здесь</a>.");
                //}

                if (!(needMailConfirmation || needAddUser))
                {
                    var newEmail = user.Email;
                    var callbackUrl = Url.Page(
                        "/Account/Login",
                        pageHandler: null,
                        values: new { area = "Identity" },
                        protocol: Request.Scheme);
                    _emailSender.SendEmailAsync(
                    user.Email,
                    "ALIS: Предоставление доступа в систему",
                    $"Администратор предоставил Вам доступ в систему. Для входа используйте email {newEmail} в качестве логина на <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>этой странице</a>.");

                }

                TempData[WC.Success] = "Успешно предоставлен доступ в систему для '" + user.FullName + "'";
                return RedirectToAction("Index");

            }

            TempData[WC.Error] = "Не все или неверно заполнены данные";
            GetRoles(person.UserId);
            return View(PersonVM);
        }


        //GET - DeactivateUserForPerson
        public IActionResult DeactivateUserForPerson(int? id)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }

            var person = _personRepo.Find(id.GetValueOrDefault());

            if (person == null)
            {
                return NotFound();
            }

            PersonVM.PersonId = person.Id;
            PersonVM.PersonName = person.Name + " " + person.Surname + " " + person.Patronymic;

            PersonVM.Email = _userManager.FindByIdAsync(person.UserId).GetAwaiter().GetResult().Email;

            GetRoles(person.UserId);

            return View(PersonVM);
        }

        //POST - DeactivateUserForPerson
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeactivateUserForPerson(PersonVM PersonVM)
        {

            Person person = _personRepo.FirstOrDefault(u => u.Id == PersonVM.PersonId);
            if (ModelState.IsValid)
            {

                if (person == null)
                {
                    TempData[WC.Error] = "Персона не найдена!";
                    GetRoles("");
                    return View(PersonVM);
                }

                ApplicationUser user = new ApplicationUser();

                if (!String.IsNullOrEmpty(person.UserId))
                {
                    user = (ApplicationUser)_userManager.FindByIdAsync(person.UserId).GetAwaiter().GetResult();

                    if (user == null)
                    {
                        TempData[WC.Error] = "Пользователь не найден!";
                        GetRoles(person.UserId);
                        return View(PersonVM);
                    }

                    user.FullName = person.Name + " " + person.Surname + " " + person.Patronymic;
                    user.LockoutEnabled = true;
                    user.LockoutEnd = DateTime.Now.AddYears(200);
                    _userManager.UpdateAsync(user).GetAwaiter().GetResult();

                    var newEmail = user.Email;
                    var callbackUrl = Url.Page(
                        "/Account/Login",
                        pageHandler: null,
                        values: new { area = "Identity" },
                        protocol: Request.Scheme);
                    _emailSender.SendEmailAsync(
                    user.Email,
                    "ALIS: Блокировка доступа в систему",
                    $"Администратор заблокировал Вам доступ в систему (email/login: " + newEmail + "). Если считаете, что это произошло по ошибке, пожалуйста, обратилесь к администратору системы.");

                    TempData[WC.Success] = "Доступ в систему для '" + user.FullName + "' успешно отозван";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData[WC.Error] = "Не обнаружена связка персоны с пользователем системы!";
                    GetRoles("");
                    return View(PersonVM);
                }
            }

            else
            {
                TempData[WC.Error] = "Не все или неверно заполнены данные";
                GetRoles(person.UserId);
                return View(PersonVM);
            }

        }

        //GET - EditUserRoleForPerson
        public IActionResult EditUserRoleForPerson(int? id)
        {

            if (id == null || id == 0)
            {
                TempData[WC.Error] = "Нет ID персоны" + id.ToString();
                return RedirectToAction("Index");

            }

            var person = _personRepo.Find(id.GetValueOrDefault());

            if (person == null)
            {
                TempData[WC.Error] = "Не найдена персона с ID: " + id.ToString();
                return RedirectToAction("Index");
            }

            if (person.IsArchive.GetValueOrDefault())
            {
                TempData[WC.Error] = "Персона в архиве. Для предоставления доступа в систему нужно восстановить персону из архива. " + id.ToString();
                return RedirectToAction("Index");
            }


            if (String.IsNullOrEmpty(person.UserId))
            {
                TempData[WC.Error] = "Для того, чтобы менять роль пользователя необходимо предоставить ему доступ в систему";
                return RedirectToAction("Index");

            }


            PersonVM.PersonId = person.Id;
            PersonVM.PersonName = person.Name + " " + person.Surname + " " + person.Patronymic;

            ApplicationUser user = (ApplicationUser)_userManager.FindByIdAsync(person.UserId).GetAwaiter().GetResult();

            if (user == null)
            {
                TempData[WC.Error] = "Пользователь не найден";
                return RedirectToAction("Index");
            }

            if ((user.LockoutEnabled == true) && (user.LockoutEnd >= DateTime.Now))
            {
                TempData[WC.Error] = "Для того, чтобы менять роль пользователя необходимо предоставить ему доступ в систему";
                return RedirectToAction("Index");
            }

            PersonVM.Email = _userManager.FindByIdAsync(person.UserId).GetAwaiter().GetResult().Email;

            GetRoles(person.UserId);

            return View(PersonVM);
        }

        //POST - EditUserRoleForPerson
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditUserRoleForPerson(PersonVM PersonVM)
        {

            Person person = _personRepo.FirstOrDefault(u => u.Id == PersonVM.PersonId);
            if (ModelState.IsValid)
            {

                if (person == null)
                {
                    TempData[WC.Error] = "Персона не найдена!";
                    GetRoles("");
                    return View(PersonVM);
                }

                ApplicationUser user = new ApplicationUser();

                if (!String.IsNullOrEmpty(person.UserId))
                {
                    user = (ApplicationUser)_userManager.FindByIdAsync(person.UserId).GetAwaiter().GetResult();

                    if (user == null)
                    {
                        TempData[WC.Error] = "Пользователь не найден!";
                        GetRoles(person.UserId);
                        return View(PersonVM);
                    }

                    user.FullName = person.Name + " " + person.Surname + " " + person.Patronymic;
                    user.LockoutEnd = null;

                    string roleName = _roleManager.FindByIdAsync(PersonVM.Role.Id).GetAwaiter().GetResult().Name;
                    var userRoles = _userManager.GetRolesAsync(user).GetAwaiter().GetResult();
                    try
                    {
                        if (userRoles.Count > 0)
                            _userManager.RemoveFromRolesAsync(user, userRoles).GetAwaiter().GetResult();

                        _userManager.AddToRoleAsync(user, roleName).GetAwaiter().GetResult(); ;

                    }
                    catch (Exception expection)
                    {
                        TempData[WC.Error] = "При обработке ролей пользователя произошла ошибка. Изменения не сохранены!";
                        GetRoles(person.UserId);
                        return View(PersonVM);
                    }

                    var newEmail = user.Email;
                    var callbackUrl = Url.Page(
                        "/Account/Login",
                        pageHandler: null,
                        values: new { area = "Identity" },
                        protocol: Request.Scheme);
                    _emailSender.SendEmailAsync(
                    user.Email,
                    "ALIS: Изменение роли доступа в систему",
                    $"Администратор изменил Вашу роль в системе на {roleName} для логина {newEmail}. Вход на сайт <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>по этой ссылке.</a>.");

                    TempData[WC.Success] = "Роль " + roleName + " успешно предоставлена персоне '" + user.FullName + "'";

                    return RedirectToAction("Index");


                }
                else
                {
                    TempData[WC.Error] = "Пустой код пользователя!";
                    GetRoles(person.UserId);
                    return View(PersonVM);

                }

            }

            TempData[WC.Error] = "Не все или неверно заполнены данные";
            GetRoles(person.UserId);
            return View(PersonVM);
        }




        //GET - MovePersonToArchive
        public IActionResult MovePersonToArchive(int? id)
        {

            if (id == null || id == 0)
            {
                TempData[WC.Error] = "Пустой ИД персоны";
                return RedirectToAction("Index");
            }

            var person = _personRepo.Find(id.GetValueOrDefault());

            if (person == null)
            {
                TempData[WC.Error] = "Не найдена персона с ID: " + id.ToString();
                return RedirectToAction("Index");

            }

            if (person.IsArchive.GetValueOrDefault())
            {
                TempData[WC.Error] = "Персона '" + person.Name + " " + person.Surname + " " + person.Patronymic + "' уже у архиве";
                return RedirectToAction("Index");

            }


            PersonVM.PersonId = person.Id;
            PersonVM.PersonName = person.Name + " " + person.Surname + " " + person.Patronymic;

            if (String.IsNullOrEmpty(person.UserId))
                PersonVM.Email = "nouser@nouser.com";
            else
                PersonVM.Email = _userManager.FindByIdAsync(person.UserId).GetAwaiter().GetResult().Email;

            GetRoles(person.UserId);

            return View(PersonVM);
        }

        //POST - MovePersonToArchive
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MovePersonToArchive(PersonVM PersonVM)
        {

            Person person = _personRepo.FirstOrDefault(u => u.Id == PersonVM.PersonId);

            if (ModelState.IsValid)
            {

                if (person == null)
                {
                    TempData[WC.Error] = "Персона не найдена!";
                    GetRoles("");
                    return View(PersonVM);
                }

                ApplicationUser user = new ApplicationUser();

                if (!String.IsNullOrEmpty(person.UserId))
                {
                    user = (ApplicationUser)_userManager.FindByIdAsync(person.UserId).GetAwaiter().GetResult();

                    if (user == null)
                    {
                        TempData[WC.Error] = "Пользователь не найден!";
                        GetRoles(person.UserId);
                        return View(PersonVM);
                    }

                    user.LockoutEnabled = true;
                    user.LockoutEnd = DateTime.Now.AddYears(200);

                    try
                    {
                        _userManager.UpdateAsync(user).GetAwaiter().GetResult();
                    }
                    catch (Exception ex)
                    {
                        TempData[WC.Error] = "Не удалось обновить информацию о пользователе!";
                        GetRoles(person.UserId);
                        return View(PersonVM);
                    }

                    try
                    {
                        var newEmail = user.Email;
                        var callbackUrl = Url.Page(
                            "/Account/Login",
                            pageHandler: null,
                            values: new { area = "Identity" },
                            protocol: Request.Scheme);
                        _emailSender.SendEmailAsync(
                        user.Email,
                        "ALIS: Отзыв доступа в систему",
                        $"Администратор заблокировал Вам доступ в систему (email/логин: {newEmail} ). Если считаете, что это произошло по ошибке, пожалуйста, обратитесь к администратору сайта.");
                    }
                    catch (Exception ex)
                    {
                    }
                }
                try
                {
                    _personRepo.Update(person, UpdateMode.MoveToArchive);
                    _personRepo.Save();
                }
                catch (Exception expection)
                {
                    TempData[WC.Error] = "При обновлении персоны произошла ошибка!";
                    GetRoles(person.UserId);
                    return View(PersonVM);
                }

                TempData[WC.Success] = "Персона '" + person.Name + " " + person.Surname + " " + person.Patronymic + "' успешно помещена в архив.";
                return RedirectToAction("Index");

            }

            TempData[WC.Error] = "Не все или неверно заполнены данные";
            GetRoles(person.UserId);
            return View(PersonVM);

        }


        //GET - RestorePersonFromArchive
        public IActionResult RestorePersonFromArchive(int? id)
        {

            if (id == null || id == 0)
            {
                TempData[WC.Error] = "Пустой ИД персоны";
                return RedirectToAction("Index");
            }

            var person = _personRepo.Find(id.GetValueOrDefault());

            if (person == null)
            {
                TempData[WC.Error] = "Не найдена персона с ID: " + id.ToString();
                return RedirectToAction("Index");

            }

            if (!(person.IsArchive.GetValueOrDefault()==true))
            {
                TempData[WC.Error] = "Персона '" + person.Name + " " + person.Surname + " " + person.Patronymic + "' уже НЕ в архиве";
                return RedirectToAction("Index");

            }

            PersonVM.PersonId = person.Id;
            PersonVM.PersonName = person.Name + " " + person.Surname + " " + person.Patronymic;

            if (String.IsNullOrEmpty(person.UserId))
                PersonVM.Email = "nouser@nouser.com";
            else
                PersonVM.Email = _userManager.FindByIdAsync(person.UserId).GetAwaiter().GetResult().Email;

            GetRoles(person.UserId);

            return View(PersonVM);
        }

        //POST - RestorePersonFromArchive
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RestorePersonFromArchive(PersonVM PersonVM)
        {

            Person person = _personRepo.FirstOrDefault(u => u.Id == PersonVM.PersonId);

            if (ModelState.IsValid)
            {

                if (person == null)
                {
                    TempData[WC.Error] = "Персона не найдена!";
                    GetRoles("");
                    return View(PersonVM);
                }

                try
                {
                    _personRepo.Update(person, UpdateMode.RestoreFromArchive);
                    _personRepo.Save();
                }
                catch (Exception expection)
                {
                    TempData[WC.Error] = "При обновлении персоны произошла ошибка!";
                    GetRoles(person.UserId);
                    return View(PersonVM);
                }

                TempData[WC.Success] = "Персона '" + person.Name + " " + person.Surname + " " + person.Patronymic + "' успешно восстановлена из архива.";
                return RedirectToAction("Index");

            }

            TempData[WC.Error] = "Не все или неверно заполнены данные";
            GetRoles(person.UserId);
            return View(PersonVM);

        }


        //GET - PrintBarCode
        public IActionResult PrintBarCode(int? id)
        {

            if (id == null || id == 0)
            {
                TempData[WC.Error] = "Пустой ИД персоны";
                return RedirectToAction("Index");
            }

            var person = _personRepo.Find(id.GetValueOrDefault());

            if (person == null)
            {
                TempData[WC.Error] = "Не найдена персона с ID: " + id.ToString();
                return RedirectToAction("Index");

            }

            if (person.IsArchive.GetValueOrDefault())
            {
                TempData[WC.Error] = "Персона '" + person.Name + " " + person.Surname + " " + person.Patronymic + "' уже у архиве";
                return RedirectToAction("Index");

            }

            if (String.IsNullOrEmpty(person.Barcode))
            {
                TempData[WC.Error] = "У персоны нет штрих-кода";
                return RedirectToAction("Index");

            }

            person.FullName = person.Surname + " " + person.Name + " " + person.Patronymic;
            return View(person);
        }



        //POST - PrintBarCode
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PrintBarCode(Person Person)
        {

                return RedirectToAction("Index");

        }



        // GET - Create
        public IActionResult Create()
        {            
            return View();
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Person person)
        {
            ModelState.Remove("Role");
            if (ModelState.IsValid)
            {            
                person.User = null;
                person.CreateDate = DateTime.Now;
                person.UserId = null;
                if (person.Patronymic == null)
                    person.Patronymic = "";
                person.FullName = person.Surname + " " + person.Name + " " + person.Patronymic;
                person.IsArchive = false;
                person.Barcode = "p-222";
                try
                { 
                _personRepo.Add(person);
                _personRepo.Save();                 
                }
                catch(Exception ex)
                {
                    TempData[WC.Error] = "Не удалось создать персону '" + person.FullName + "'";
                    return View(person);
                }

                try
                {                    
                    if(person!=null)
                    { 
                        person.Barcode = "p-" + (person.Id.ToString()).PadLeft(6, '0');
                        _personRepo.Update(person, UpdateMode.Update);
                        _personRepo.Save();
                    }
                }
                catch (Exception ex)
                {
                }


                TempData[WC.Success] = "Персона '" + person.FullName + "' успешно создана";
                return RedirectToAction("Index");
            }
            TempData[WC.Error] = "Не удалось создать персону. Не все обязательные поля заполнены или заполнены неверно.";

            return View(person);
        }


        //EDIT - GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                TempData[WC.Error] = "Пустой ИД персоны";
                return RedirectToAction("Index");
            }

            var person = _personRepo.Find(id.GetValueOrDefault());

            if (person == null)
            {
                TempData[WC.Error] = "Не найдена персона с ИД: " + id.ToString();
                return RedirectToAction("Index");

            }

            return View(person);
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Person obj)
        {
            ModelState.Remove("Role");
            if (ModelState.IsValid)
            {

                var person = _personRepo.Find(obj.Id);

                if(person==null)
                {
                    TempData[WC.Error] = "Персона не найдена!";                     
                    return View(obj);
                }

                person = obj;

                person.FullName = person.Surname + " " + person.Name + " " + person.Patronymic;                
                try
                {
                    _personRepo.Update(person, UpdateMode.Update);
                    _personRepo.Save();
                }
                catch (Exception ex)
                {
                    TempData[WC.Error] = "Не удалось обновить данные персоны '" + person.FullName + "'";
                    return View(person);
                }

                TempData[WC.Success] = "Персона '" + person.FullName + "' успешно обновлена";
                return RedirectToAction("Index");
            }
            TempData[WC.Error] = "Не удалось обновить данные персоны. Не все обязательные поля заполнены или заполнены неверно.";

            return View(obj);
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
