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
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        private readonly ApplicationDbContext _db;

        public GenreRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Genre obj, UpdateMode updateMode = UpdateMode.Update)
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
