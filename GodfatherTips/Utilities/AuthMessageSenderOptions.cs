using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GodfatherTips.Utilities
{
    public class AuthMessageSenderOptions
    {
        public int PortNumber { get; set; }

        public string SmtpServer { get; set; }

        public string ApiUser { get; set; }

        public string ApiKey { get; set; }
    }
}
