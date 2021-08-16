using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealHub_Domain.Authentication
{
   public class AuthenticationDetailParameters
    {
        public uint user_id { get; set; }
        public string user_code { get; set; }
        public string password { get; set; }

        public string status { get; set; }

        public string token { get; set; }

        public string privilege_name { get; set; }

        public string role_name { get; set; }

        public string UserName { get; set; }

        public uint ispasswordchanged { get; set; }


    }

    public class DeleteTokenResponse
    {
        public string result { get; set; }
    }
 
}
