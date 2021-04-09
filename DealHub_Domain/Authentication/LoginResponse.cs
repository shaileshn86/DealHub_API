using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealHub_Domain.Authentication
{
  public  class LoginResponse
    {
        public User user { get; set; }
        public LoginResponse()
        {
            user = new User();
        }
    }
}
