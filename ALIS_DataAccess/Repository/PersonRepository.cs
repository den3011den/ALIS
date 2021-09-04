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
                            if(needАctivateUser)
                                if(!String.IsNullOrEmpty(objFromDb.UserId))
                                    {
                                        ApplicationUser user = (ApplicationUser) _userManager.FindByIdAsync(objFromDb.UserId).Result;
                                        if (user!=null) {                               
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
