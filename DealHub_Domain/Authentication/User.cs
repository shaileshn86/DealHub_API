using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealHub_Domain.Authentication
{
    public class User
    {
        public string UserName { get; set; }
        public string Api_Key { get; set; }

        public string privilege_name { get; set; }

        public string CompanyName { get; set; }
        public string CompanyCode { get; set; }

        public string role_name { get; set; }
        public string UserCode { get; set; }

        public uint UserId { get; set; }

        public User()
        {
            CompanyName = "MAHINDRA LOGISTICS LTD";
            CompanyCode = "MLL";

        }
    }
}
