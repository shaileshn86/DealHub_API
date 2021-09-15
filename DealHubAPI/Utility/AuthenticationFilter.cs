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
using DealHub_Service.Implemantations.ErrorLog;

namespace DealHubAPI.Utility
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class AuthenticationFilterDealhUb: AuthorizeAttribute
    {
        public HttpResponseMessage result = new HttpResponseMessage();
        protected override bool IsAuthorized(HttpActionContext filterContext)
        {
            string user_code = "";
            string _passtoken = "";
            string AntiforgeryKey = "";
           try
            {
                var _token = filterContext.Request.Headers.SingleOrDefault(x => x.Key == "Authorization").Value;
                 _passtoken = _token.First();
                _passtoken = _passtoken.Replace("Bearer ", "").Trim();

                var userloginid = filterContext.Request.Headers.SingleOrDefault(x => x.Key == "_user_login").Value;
                 user_code = userloginid.First();
                user_code = AuthenticationServices.DecryptStringAES(CommonFunctions.CommonKeyClass.Key, user_code);
                //added 
                //var RequestId = filterContext.Request.Headers.SingleOrDefault(x => x.Key == "_RequestId").Value;
                // AntiforgeryKey = RequestId.First();

                //bool Isverify = DealHubAPI.Utility.AnitiforgeryVerify.VerifyRequestKey(user_code, AntiforgeryKey);
                //if (Isverify==false)
                //{
                //    return false;
                //}

                return CheckIsAuthorized(_passtoken, user_code);
            }
            catch(Exception e)
            {
                ErrorService.writeloginfile("Error in Catch : usercode"+user_code+" token:"+_passtoken+" Exception:"+e.ToString());
                return false;
            }
           
          
         
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            actionContext.Response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Content = new StringContent("You are unauthorized to access this resource")
            };
        }

        public bool CheckIsAuthorized(string _token,string user_code)
        {
            AuthenticationParameters _auth = new AuthenticationParameters();
            _auth._user_code = user_code;
            _auth._token = _token;
            string result = AuthenticationServices.GetToken(_auth);
            ErrorService.writeloginfile("UserCode :"+user_code+" Token :"+_token+" Result:"+result);
            if (result!= "Authorised")
            {
                return false;
            }

            return true;
        }
        
    }
}