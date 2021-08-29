using ALIS_DataAccess.Data;
using ALIS_Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ALIS_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALIS_DataAccess.Repository;

namespace ALIS_DataAccess.Initializer
{
    public class DbInitializer : IDbInitializer
    {

        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IPersonRepository _personRepo;

        public DbInitializer(ApplicationDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IPersonRepository personRepo)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _personRepo = personRepo;
        }

        public void Initialize()
        {
            try
            {
                if(_db.Database.GetPendingMigrations().Count()>0)
                {
                    _db.Database.Migrate();
                }
            }
            catch(Exception ex)
            {

            }

            if (!_roleManager.RoleExistsAsync(WC.AdminRole).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(WC.AdminRole)).GetAwaiter().GetResult();
            }
            
            if (!_roleManager.RoleExistsAsync(WC.ClientRole).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(WC.ClientRole)).GetAwaiter().GetResult();
            }
            

            Person person1 = _personRepo.FirstOrDefault(u => u.Name == "Екатерина" && u.Surname == "Сенотрусова" && u.Patronymic == "Сергеевна");
            Person person2 = _personRepo.FirstOrDefault(u => u.Name == "Михаил" && u.Surname == "Сенотрусов" && u.Patronymic == "Сергеевич");
            Person person3 = _personRepo.FirstOrDefault(u => u.Name == "Денис" && u.Surname == "Богомолов" && u.Patronymic == "Сергеевич");


            if (String.IsNullOrEmpty(person1.UserId))
            {
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = person1.Email,
                    Email = person1.Email,
                    EmailConfirmed = true,
                    FullName = person1.Surname + " " + person1.Name + " " + person1.Patronymic,
                    PhoneNumber = person1.PhoneNumber,
                }, "Admin123*").GetAwaiter().GetResult();

                ApplicationUser user1 = _db.ApplicationUser.FirstOrDefault(u => u.Email == person1.Email);
                _userManager.AddToRoleAsync(user1, WC.AdminRole).GetAwaiter().GetResult();

                person1.UserId = user1.Id;
                _personRepo.Save();
            }

            if (String.IsNullOrEmpty(person2.UserId))
            {
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = person2.Email,
                    Email = person2.Email,
                    EmailConfirmed = true,
                    FullName = person2.Surname + " " + person2.Name + " " + person2.Patronymic,
                    PhoneNumber = person2.PhoneNumber,
                }, "Admin123*").GetAwaiter().GetResult();

                ApplicationUser user2 = _db.ApplicationUser.FirstOrDefault(u => u.Email == person2.Email);
                _userManager.AddToRoleAsync(user2, WC.AdminRole).GetAwaiter().GetResult();

                person2.UserId = user2.Id;
                _personRepo.Save();
            }


            if (String.IsNullOrEmpty(person3.UserId))
            {
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = person3.Email,
                    Email = person3.Email,
                    EmailConfirmed = true,
                    FullName = person3.Surname + " " + person3.Name + " " + person3.Patronymic,
                    PhoneNumber = person3.PhoneNumber,
                }, "Admin123*").GetAwaiter().GetResult();

                ApplicationUser user3 = _db.ApplicationUser.FirstOrDefault(u => u.Email == person3.Email);
                _userManager.AddToRoleAsync(user3, WC.AdminRole).GetAwaiter().GetResult();

                person3.UserId = user3.Id;
                _personRepo.Save();
            }

        }
    }
}
