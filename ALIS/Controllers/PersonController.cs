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

        public PersonController(IPersonRepository personRepo , RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager,
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

        //EDIT - GET
        public IActionResult ActivateUserForPerson(int? id)
        {            

            if (id == null || id == 0)
            {
                return NotFound();
            }

            var person = _personRepo.Find(id.GetValueOrDefault());

            if (id == null)
            {
                return NotFound();
            }
            
            PersonVM.PersonId = person.Id;
            PersonVM.PersonName = person.Name + " " + person.Surname + " " + person.Patronymic;

            if (String.IsNullOrEmpty(person.UserId))
                PersonVM.Email = person.Email;

            GetRoles(person.UserId);

            return View(PersonVM);
        }

        //POST - EDIT
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
                        "/Account/Ligin",
                        pageHandler: null,
                        values: new { area = "Identity" },
                        protocol: Request.Scheme);
                    _emailSender.SendEmailAsync(
                    user.Email,
                    "ALIS: Предоставление доступа в систему",
                    $"Администратор предоставил Вам доступ в систему. Для входа используйте email '"+ newEmail + "' в качестве логина на <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>этой странице</a>.");

                }

                TempData[WC.Success] = "Успешно предоставлен доступ в систему для '" + user.FullName + "'";
                return RedirectToAction("Index");

            }

        TempData[WC.Error] = "Не все или неверно заполнены данные";
        GetRoles(person.UserId);
        return View(PersonVM);
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
