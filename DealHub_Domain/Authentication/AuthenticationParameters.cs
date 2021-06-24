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
       // [RegularExpression(@"([a-zA-Z0-9@#$&;|,.?-_]+)", ErrorMessage = "not valid expression")]
        public string _user_code { get; set; }

       // [RegularExpression(@"([a-zA-Z0-9@#$&;|,.?-_]+)", ErrorMessage = "not valid expression")]
        public string _password { get; set; }

        public string _token { get; set; }
        public string _SecretKey { get; set; }
        
        public string _attempt { get; set; }
        public string _ClientId { get; set; }
    }

    public class fileinfo
    {
        public string _filename { get; set; }
    }

    public class TokenRequestParameter
    {
        public string _user_code { get; set; }
        public string token { get; set; }
    }
}
