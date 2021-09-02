﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALIS_Utility
{

    public enum UpdateMode
    {
        Update,
        MoveToArchive,
        RestoreFromArchive
    }


    public static class WC
    {
        public const string AdminRole = @"Admin";
        public const string ClientRole = @"Client";

        public const string Success = @"Success";
        public const string Error = @"Error";

        public static UpdateMode UpdateMode;

    }
}
