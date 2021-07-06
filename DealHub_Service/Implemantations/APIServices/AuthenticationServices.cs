using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DealHub_Dal.Authentication;
using DealHub_Domain.Authentication;
using System.Security.Cryptography;

namespace DealHub_Service.Implemantations.APIServices
{
    public class AuthenticationServices
    {
        public static List<AuthenticationDetailParameters> GetAuthenticateUser(AuthenticationParameters filter)
        {
            //return Authentication.AutheticateUser(filter);
            return Authentication.AutheticateUserwithattempts(filter);
        }

        public static DeleteTokenResponse DeleteToken(string usercode)
        {
            return Authentication.deleteToken(usercode);
        }
        public static string DecryptStringAES(string Secretkey, string pwd)
        {
            return ASEEncryptDecrypt.DecryptStringAES(Secretkey, pwd);
        }

        public static string ReturnMD5Hash(string input)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] originalBytes = ASCIIEncoding.Default.GetBytes(input);
            byte[] encodedBytes = md5.ComputeHash(originalBytes);
            md5.Clear();

            string hashedString = BitConverter.ToString(encodedBytes);
            hashedString = hashedString.Replace("-", string.Empty).ToLower();
            return hashedString;
        }

        public static int UpdateToken(AuthenticationParameters filter)
        {
            return Authentication.UpdateToken(filter);
        }

        public static string GetToken(AuthenticationParameters filter)
        {
            return Authentication.GetToken(filter);
        }
        public static string ResetPassword(AuthenticationParameters filter)
        {
            return Authentication.ResetPassword(filter);
        }

        public static string ResetPasswordDashboard(AuthenticationParameters filter)
        {
            return Authentication.ResetPasswordDashboard(filter);
        }
        public static string sendmail(string UserCode)
        {
            return Authentication.sendmail(UserCode);

        }

    }
}
