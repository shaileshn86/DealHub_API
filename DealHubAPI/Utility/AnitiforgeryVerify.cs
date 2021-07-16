using DealHub_Service.Implemantations.ErrorLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace DealHubAPI.Utility
{
    public class AnitiforgeryVerify
    {
        private static string secretkey = "b14ca5898a4e4133bbce2ea2315a2021";// "$!$030!m0l0l";
        public static bool VerifyRequestKey(string UserCode, string _requestkey)
        {
            var _UserAgent = HttpContext.Current.Request.UserAgent;
            var _browserInfo = HttpContext.Current.Request.Browser.ToString() + HttpContext.Current.Request.Browser.Version.ToString() + _UserAgent.ToString() + "~" + HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            string _RequestBrowserInfo = string.Empty;
            string _RequestIPAddress = string.Empty;
            string UserId = string.Empty;

            char[] _separator = new char[] { '^' };
            if (!string.IsNullOrEmpty(_requestkey))
            {
                string originalplaintext = DecryptString(secretkey, _requestkey);
                string[] _splitStrings = originalplaintext.Split(_separator);

                if (_splitStrings.Count() > 0)
                {
                    UserId = _splitStrings[0];
                    string Ticks = _splitStrings[1];
                    string dummyGuid = _splitStrings[3];

                    if (_splitStrings[2].Count() > 0)
                    {
                        string[] _userBrowserInfo = _splitStrings[2].Split('~');
                        if (_userBrowserInfo.Count() > 0)
                        {
                            _RequestBrowserInfo = _userBrowserInfo[0];
                            _RequestIPAddress = _userBrowserInfo[1];
                        }
                    }
                }
            }

            //************************************************************//

            string _currentUseripAddress;
            if (string.IsNullOrEmpty(HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]))
            {
                _currentUseripAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            else
            {
                _currentUseripAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
            }

            System.Net.IPAddress result;
            if (!System.Net.IPAddress.TryParse(_currentUseripAddress, out result))
            {
                result = System.Net.IPAddress.None;
            }

            if (_RequestIPAddress != "" && _RequestIPAddress != string.Empty)
            {
                //Same way we can validate browser info also...
                string _currentBrowserInfo = HttpContext.Current.Request.Browser.Browser.ToString() + HttpContext.Current.Request.Browser.Version.ToString() + _UserAgent.ToString();
                //Request.Browser.Browser + Request.Browser.Version + Request.UserAgent;

               // ErrorService.writeloginfile("Antiforgery verification : IP Address:" + _currentUseripAddress + ", Browser:" + _currentBrowserInfo + ", UserCode :" + UserCode + ", Antifrogerykey :" + _requestkey);
                if (_RequestIPAddress != _currentUseripAddress || _currentBrowserInfo != _RequestBrowserInfo || UserCode.ToString().ToLower() != UserId.ToString().ToLower())
                {

                    ErrorService.writeloginfile("Antiforgery verification : IP Address:" + _currentUseripAddress+", Browser:"+ _currentBrowserInfo +", UserCode :"+ UserCode+", Antifrogerykey :"+ _requestkey);

                    return false;


                    /************************/
                    //if (Request.IsAuthenticated)
                    //{
                    //    FormsAuthentication.SignOut();


                    //}


                    //Session.Clear();
                    //Session.RemoveAll();
                    //Session.Abandon(); // Session Expire but cookie do exist


                    //Response.Cookies.Clear();

                    //if (Request.Cookies["ASP.NET_SessionId"] != null)
                    //{
                    //    Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
                    //    Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddSeconds(-20);
                    //}

                    //if (Request.Cookies[".ASPXAUTHWMS"] != null)
                    //{
                    //    Response.Cookies[".ASPXAUTHWMS"].Value = string.Empty;
                    //    Response.Cookies[".ASPXAUTHWMS"].Expires = DateTime.Now.AddSeconds(-20);
                    //}


                    //if (Request.Cookies["__AntiXsrfToken"] != null)
                    //{
                    //    Response.Cookies["__AntiXsrfToken"].Value = string.Empty;
                    //    Response.Cookies["__AntiXsrfToken"].Expires = DateTime.Now.AddSeconds(-20);
                    //}





                    //Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));





                    // Response.Redirect("~/Login.aspx");
                }
                else
                {
                    //Valid User
                    //if (Request != null)

                    //{
                    //    // Added By Ramajor
                    //    // If User open new tab in same browser and user login exist then redirect to home or an otherwise logout and opne tab

                    //    if (Request.Url.ToString().ToLower().Contains("login.aspx"))
                    //    {
                    //        Response.Redirect("~/Home.aspx");

                    //    }
                    //}
                    return true;
                }


            }
            else
            {
                return false;
            }
        }
        public static string RequestKey(string UserCode)
        {
            // Added by Ramajor 21/06/2021
            //string _browserInfo = Request.Browser.Browser + Request.Browser.Version + Request.UserAgent + "~" + Request.ServerVariables["REMOTE_ADDR"];
            //HttpRequestMessage request
            var _UserAgent = HttpContext.Current.Request.UserAgent;
            var _browserInfo = HttpContext.Current.Request.Browser.Browser + HttpContext.Current.Request.Browser.Version.ToString() + _UserAgent.ToString() + "~" + HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            //var userAgent = HttpContext.Current.Request.Headers.GetValues("User-Agent");

            //if (request.Properties.ContainsKey("MS_HttpContext"))
            //{
            //    return IPAddress.Parse(((HttpContextBase)request.Properties["MS_HttpContext"]).Request.UserHostAddress).ToString();
            //}
            //if (request.Properties.ContainsKey("MS_OwinContext"))
            //{
            //    return IPAddress.Parse(((OwinContext)request.Properties["MS_OwinContext"]).Request.RemoteIpAddress).ToString();
            //}
            //return String.Empty;



            string _sessionValue = UserCode + "^" + DateTime.Now.Ticks + "^"   + _browserInfo + "^" + System.Guid.NewGuid();
           string _encryptedString= EncryptString(secretkey, _sessionValue);
            ErrorService.writeloginfile("Login Antifrogery : Session value:" + _sessionValue + ", AntifrogeryKey:" + _encryptedString);
            //  byte[] _encodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(_sessionValue);
            //  string _encryptedString = System.Convert.ToBase64String(_encodeAsBytes);
            return _encryptedString;

        }


        
            public static string EncryptString(string key, string plainText)
            {
                byte[] iv = new byte[16];
                byte[] array;

                using (Aes aes = Aes.Create())
                {
                    aes.Key = Encoding.UTF8.GetBytes(key);
                    aes.IV = iv;

                    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                            {
                                streamWriter.Write(plainText);
                            }

                            array = memoryStream.ToArray();
                        }
                    }
                }

                return Convert.ToBase64String(array);
            }

            public static string DecryptString(string key, string cipherText)
            {
                byte[] iv = new byte[16];
                byte[] buffer = Convert.FromBase64String(cipherText);

                using (Aes aes = Aes.Create())
                {
                    aes.Key = Encoding.UTF8.GetBytes(key);
                    aes.IV = iv;
                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                    using (MemoryStream memoryStream = new MemoryStream(buffer))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                            {
                                return streamReader.ReadToEnd();
                            }
                        }
                    }
                }
            }
        

    }
}