using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DealHubAPI.Models
{
    public class LoginModel
    {
        [Required]
        public string _user_code { get; set; }
        [Required]
        public string _password { get; set; }
    }
}