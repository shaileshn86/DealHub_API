using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DealHub_Dal.Authentication;
using DealHub_Domain.Authentication;

namespace DealHub_Service.Implemantations.APIServices
{
    public class AuthenticationServices
    {
        public static List<AuthenticationDetailParameters> GetAuthenticateUser(AuthenticationParameters filter)
        {
            return Authentication.AutheticateUser(filter);
        }

        public static int UpdateToken(AuthenticationParameters filter)
        {
            return Authentication.UpdateToken(filter);
        }

        public static string GetToken(AuthenticationParameters filter)
        {
            return Authentication.GetToken(filter);
        }



    }
}
