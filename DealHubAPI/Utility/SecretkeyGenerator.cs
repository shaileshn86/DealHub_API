using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using DealHubAPI.Utility;

namespace DealHubAPI.Utility
{
    public class SecretkeyGenerator
    {
        public string GetSecretKey()
        {

            System.Security.Cryptography.RandomNumberGenerator cryptoRandomDataGenerator = new System.Security.Cryptography.RNGCryptoServiceProvider();
            byte[] buffer = new byte[48];
            cryptoRandomDataGenerator.GetBytes(buffer);
            string secretkey = Convert.ToBase64String(buffer);
            return secretkey;
        }

        public static string GetClientId()
        {
            Guid gid = Guid.NewGuid();
            string clientId = gid.ToString();
            return clientId;
        }

        public static string UserSecretkey()

        {
            string user = "admin";
            string password = "123456";
            byte[] salt = { 65, 66, 67, 68, 69, 70, 71 };
            string Haskey = "";


            using (var derivedBytes = new System.Security.Cryptography.Rfc2898DeriveBytes(user, salt, iterations: 49999))
            {
                //salt = derivedBytes.Salt;
                //byte[] key = derivedBytes.GetBytes(16); // 128 bits key
                Haskey = Convert.ToBase64String(salt);
            }

            using (var derivedBytes = new System.Security.Cryptography.Rfc2898DeriveBytes(password, salt, iterations: 49999))
            {
                // salt = derivedBytes.Salt;
                byte[] key = derivedBytes.GetBytes(16); // 128 bits key
                Haskey = Haskey + "-" + Convert.ToBase64String(key);
            }

            return Haskey;
        }

        public static string CreateToken(string _userid,string _password)
        {

            string token = "";
            string user = _userid;
            string password = _password;
            string secret = ConfigurationManager.AppSettings["OBFNet_Api_Secret"];
            string message = user + "&" + password;
            string message2 = password + "&" + user;
            var encoding = new System.Text.ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(secret);
            byte[] messageBytes = encoding.GetBytes(message);
            byte[] messageBytes2 = encoding.GetBytes(message2);

            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                token = Convert.ToBase64String(hashmessage);
            }

            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes2);
                token = token + Convert.ToBase64String(hashmessage);
            }

            StaticGlobalKey.AppGlobalKey["HEADER-API-KEY"] = token;
            return token;
        }
    }
}