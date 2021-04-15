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
    public class AuthenticationFilterDealhUb: AuthorizationFilterAttribute 
    {
        public HttpResponseMessage result = new HttpResponseMessage();
        public override void OnAuthorization(HttpActionContext filterContext)
        {
           var _token = filterContext.Request.Headers.SingleOrDefault(x => x.Key == "Authorization").Value;
            string _passtoken = _token.First();
            _passtoken = _passtoken.Replace("Bearer ", "").Trim();
            string rawRequest;
            using (var stream = new StreamReader(filterContext.Request.Content.ReadAsStreamAsync().Result))
            {
                stream.BaseStream.Position = 0;
                rawRequest = stream.ReadToEnd();
            }
            JObject json = JObject.Parse(rawRequest);
            string user_code = json["_user_code"].ToString();

            if (!IsAuthorized(_passtoken,user_code))
            {
               // result = new ReponseMessage(MsgNo: HttpStatusCode.Unauthorized.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Login Failed", Validation: null);
               filterContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                return;
            }
            //else
            //{
            //    filterContext.Response = new HttpResponseMessage(HttpStatusCode.OK);
            //    return;
            //}


            //base.OnAuthorization(filterContext);
        }

        public bool IsAuthorized(string _token,string user_code)
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