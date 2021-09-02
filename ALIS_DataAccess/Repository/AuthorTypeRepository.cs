using ALIS_DataAccess.Data;
using ALIS_Models;
using ALIS_Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALIS_DataAccess.Repository
{
    public class AuthorTypeRepository : Repository<AuthorType>, IAuthorTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public AuthorTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(AuthorType obj, UpdateMode updateMode = UpdateMode.Update)
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
                            break;
                        }
                    case UpdateMode.RestoreFromArchive:
                        {
                            objFromDb.IsArchive = false;
                            break;
                        }
                    default:
                        {
                            objFromDb.Name = obj.Name;
                            break;
                        }
                }

            }
        }
    }
}
