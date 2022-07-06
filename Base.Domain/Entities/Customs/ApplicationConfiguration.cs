using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Domain.Entities.Customs
{
    public class ApplicationConfiguration
    {
        public static ApplicationConfiguration Current { get; set; }

        public ConfigurationEMail EMail { get; set; }
        public string ContentRootPath { get; set; }

        public class ConfigurationEMail
        {
            public string Account { get; set; }
            public string Password { get; set; }
            public string AccountName { get; set; }
            public string Host { get; set; }
            public int Port { get; set; }
            public bool UseSSL { get; set; }
            public string HostUrl { get; set; }
        }
    }
}
