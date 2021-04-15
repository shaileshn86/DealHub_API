using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealHub_Domain.Authentication
{
   public class AuthenticationDetailParameters
    {
        public string user_code { get; set; }
        public string password { get; set; }

        public string status { get; set; }

        public string token { get; set; }
    }
}
