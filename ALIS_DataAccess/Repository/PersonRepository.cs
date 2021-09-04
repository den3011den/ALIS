using ALIS_DataAccess.Data;
using ALIS_Models;
using ALIS_Utility;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ALIS_DataAccess.Repository
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        public PersonRepository(ApplicationDbContext db, UserManager<IdentityUser> userManager) : base(db)
        {
            _db = db;
            _userManager = userManager;            
        }


        public IEnumerable<Person> GetAll(Expression<Func<Person, bool>> filter = null, Func<IQueryable<Person>, IOrderedQueryable<Person>> orderBy = null, string includeProperties = null, bool isTracking = true)
        {
            IEnumerable <Person> personList = base.GetAll(filter, orderBy, includeProperties, isTracking);
            foreach(Person person in personList)
            {
                if (!String.IsNullOrEmpty(person.UserId))
                {
                    var user = _userManager.FindByIdAsync(person.UserId).Result;

                    if (user !=null)
                    {
                        person.User = user;

                        IList<string> userRoles = _userManager.GetRolesAsync(user).GetAwaiter().GetResult();

                        if (userRoles.Contains(WC.AdminRole))
                            person.RoleName = WC.AdminRole;
                        else
                            if (userRoles.Contains(WC.ClientRole))
                                person.RoleName = WC.ClientRole;

                        if (person.User.LockoutEnabled)
                            if (person.User.LockoutEnd > DateTime.Now)
                            {
                                person.IsNotActive = true;
                            }
                    }
                }
            }
            return personList;
        }

        public void Update(Person obj, UpdateMode updateMode, bool needАctivateUser = false)
        {

            var objFromDb = base.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {

                switch (updateMode)
                {
                    case UpdateMode.Update:
                        {
                            objFromDb.Name = obj.Name;
                            objFromDb.Surname = obj.Surname;
                            objFromDb.Patronymic = obj.Patronymic;
                            objFromDb.CreateDate = obj.CreateDate;
                            objFromDb.Birthday = obj.Birthday;
                            objFromDb.PhoneNumber = obj.PhoneNumber;
                            objFromDb.AltPhoneNumber = obj.AltPhoneNumber;
                            objFromDb.Email = obj.Email;
                            objFromDb.Barcode = obj.Barcode;
                            objFromDb.HomeAddres = obj.HomeAddres;
                            objFromDb.GroupNumber = obj.GroupNumber;
                            objFromDb.PasportSerial = obj.PasportSerial;
                            objFromDb.PasportNumber = obj.PasportNumber;
                            objFromDb.PasportIssuedBy = obj.PasportIssuedBy;
                            objFromDb.PasportIssueDate = obj.PasportIssueDate;
                            objFromDb.IsArchive = obj.IsArchive;
                            objFromDb.User = obj.User;
                            objFromDb.UserId = obj.UserId;
                            objFromDb.RoleName = obj.RoleName;
                            objFromDb.IsNotActive = obj.IsNotActive;
                            break;
                        }
                    case UpdateMode.MoveToArchive:
                        {
                            objFromDb.IsArchive = true;
                            if (!String.IsNullOrEmpty(objFromDb.UserId))
                            {
                                ApplicationUser user = (ApplicationUser)_userManager.FindByIdAsync(objFromDb.UserId).Result;
                                if (user != null)
                                {
                                    user.LockoutEnd = DateTime.Now.AddYears(200);
                                    _userManager.UpdateAsync(user);
                                }
                            }
                            break;
                        }
                    case UpdateMode.RestoreFromArchive:
                        {
                            objFromDb.IsArchive = false;
                            if (needАctivateUser)
                                if (!String.IsNullOrEmpty(objFromDb.UserId))
                                {
                                    ApplicationUser user = (ApplicationUser)_userManager.FindByIdAsync(objFromDb.UserId).Result;
                                    if (user != null)
                                    {
                                        user.LockoutEnd = null;
                                        _userManager.UpdateAsync(user);
                                    }
                                }
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }

            }

        }
    }
}
