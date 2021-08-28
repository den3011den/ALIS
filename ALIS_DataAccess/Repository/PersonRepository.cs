using ALIS_DataAccess.Data;
using ALIS_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALIS_DataAccess.Repository
{
    public class PublisherRepository : Repository<Publisher>, IPublisherRepository
    {
        private readonly ApplicationDbContext _db;

        public PublisherRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
