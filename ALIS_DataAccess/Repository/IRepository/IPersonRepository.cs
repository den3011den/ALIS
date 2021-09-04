using ALIS_DataAccess.Repository.IRepository;
using ALIS_Models;
using ALIS_Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ALIS_DataAccess.Repository
{
    public interface IPersonRepository : IRepository<Person>
    {
        void Update(Person obj, UpdateMode UpdateMode, bool needАctivateUser = false);

    }
}
