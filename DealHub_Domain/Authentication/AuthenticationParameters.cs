using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DealHub_Domain.Authentication
{
    public class AuthenticationParameters
    {
        [Required]
        public string _user_code { get; set; }
       
        public string _password { get; set; }

        public string _token { get; set; }
    }
}
