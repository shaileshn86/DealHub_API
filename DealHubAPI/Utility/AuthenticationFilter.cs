using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using DealHub_Service.Implemantations.APIServices;
using DealHub_Domain.Authentication;
using System.IO;
using DealHub_Domain.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using DealHub_Domain.Enum;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Net.Http;

namespace DealHubAPI.Utility
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class AuthenticationFilterDealhUb: AuthorizeAttribute
    {
        public HttpResponseMessage result = new HttpResponseMessage();
        protected override bool IsAuthorized(HttpActionContext filterContext)
        {
           try
            {
                var _token = filterContext.Request.Headers.SingleOrDefault(x => x.Key == "Authorization").Value;
                string _passtoken = _token.First();
                _passtoken = _passtoken.Replace("Bearer ", "").Trim();

                var userloginid = filterContext.Request.Headers.SingleOrDefault(x => x.Key == "_user_login").Value;
                string user_code = userloginid.First();

                return CheckIsAuthorized(_passtoken, user_code);
            }
            catch(Exception e)
            {
                return false;
            }
           
          
         
        }

        public bool CheckIsAuthorized(string _token,string user_code)
        {
            AuthenticationParameters _auth = new AuthenticationParameters();
            _auth._user_code = user_code;
            _auth._token = _token;
            string result = AuthenticationServices.GetToken(_auth);

            if (result!= "Authorised")
            {
                return false;
            }

            return true;
        }
        
    }
}