using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealHub_Domain.Authentication
{
    public class User
    {
        public string UN { get; set; }
        public string Api_Key { get; set; }

        public uint ispasswordchanged { get; set; }
        public string PN { get; set; }

        public string CN { get; set; }
        public string CC { get; set; }

        public string RN { get; set; }
        public string UC { get; set; }

        public string UI { get; set; }
        public string AntiforgeryKey { get; set; }
        public User()
        {
            CN = "MAHINDRA LOGISTICS LTD";
            CC = "MLL";
            //CompanyName = "MAHINDRA LOGISTICS LTD";
            //CompanyCode = "MLL";
            

        }
    }
}
