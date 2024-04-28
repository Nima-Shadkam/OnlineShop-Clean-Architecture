using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common
{
    public class Configs
    {

        public int RefreshTokenTimeout { get; set; }
        public int TokenTimeout { get; set; }
        public string DBConnection { get; set; }

        public string TokenKey { get; set; } 
        public string EncryptionKey { get; set; }

        public string Port { get; set; }

    }
}
