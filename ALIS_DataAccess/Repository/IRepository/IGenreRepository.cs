﻿using ALIS_DataAccess.Repository.IRepository;
using ALIS_Models;
using ALIS_Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALIS_DataAccess.Repository
{
    public interface IGenreRepository : IRepository<Genre>
    {
        void Update(Genre obj, UpdateMode UpdateMode);
    }
}
