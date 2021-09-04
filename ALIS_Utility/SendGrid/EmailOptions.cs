using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALIS_Utility.SendGrid
{
    public class EmailOptions
    {
        //public string SendGridUser { get; set; }
        //public string SendGridKey { get; set; }

        public string TpuSmtpServer { get; set; }
        public string TpuSmtpPort { get; set; }
        public string TpuSmtpUserName { get; set; }
        public string TpuSmtpUserPassword { get; set; }

    }
}
