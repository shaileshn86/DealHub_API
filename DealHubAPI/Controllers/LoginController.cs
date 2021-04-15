using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DealHubAPI.Models;
using DealHub_Domain.Authentication;
using DealHub_Service.Implemantations.APIServices;
using DealHub_Domain.Helpers;
using DealHubAPI.Utility;
using DealHub_Domain.Enum;
using DealHub_Domain.Authentication;
using DealHub_Domain.MenuBinding;
using Newtonsoft.Json;

namespace DealHubAPI.Controllers
{
    [RoutePrefix("Api/Auth")]
    public class LoginController : BaseApiController
    {
        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public HttpResponseMessage VerifyLogin(AuthenticationParameters model)
        {
            if (model == null)// Incase Post Object Is Null or Not Match and Object value is null
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }
            if (ModelState.IsValid)
            {

                List<AuthenticationDetailParameters> _AuthenticationDetailParameters = AuthenticationServices.GetAuthenticateUser(model);
                foreach (AuthenticationDetailParameters auth in _AuthenticationDetailParameters)
                {
                    if(auth.status!= "success")
                    {
                        result = new ReponseMessage(MsgNo: HttpStatusCode.Unauthorized.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Login Failed", Validation: ModelState.AllErrors());
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    else
                    {
                       LoginResponse login = new LoginResponse();
                        string key = Utility.SecretkeyGenerator.CreateToken(auth.user_code,auth.password);
                        login.user.Api_Key = key;
                        login.user.UserName = model._user_code;

                        model._token = key;
                        int tokeupdated = AuthenticationServices.UpdateToken(model);
                        //  result = new ReponseMessage(KeyName: "api_key", Code: key, MsgNo: HttpStatusCode.OK.ToCode(), MsgType: MsgTypeEnum.S.ToString(), Message: "Success");
                        //Provided username and password is incorrect
                        
                        return Request.CreateResponse(HttpStatusCode.OK, login);
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, _AuthenticationDetailParameters);

            }
            else
            {

                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "", Validation: ModelState.AllErrors());
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }
            return null;
        }

        [AuthenticationFilterDealhUb,HttpPost]
        
        [Route("GetMenuDetails")]
        public HttpResponseMessage GetMenus(MenuBindingParameter model)
        {
            if (model == null)// Incase Post Object Is Null or Not Match and Object value is null
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }
            if (ModelState.IsValid)
            {
                List<MenuBindingDetailsParameter> _MenuBindingDetailsParameter = MenuBindingServices.GetMenus(model);

                if (_MenuBindingDetailsParameter !=null)
                {
                    if (_MenuBindingDetailsParameter.Count !=0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(_MenuBindingDetailsParameter));
                    }
                    else
                    {
                        result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                        return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                    }


                    
                }
                else
                {
                    result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                    return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                }

            }

                return null;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("RemindMe")]
        public HttpResponseMessage RemindMe(AuthenticationParameters model)
        {
            if (model == null)// Incase Post Object Is Null or Not Match and Object value is null
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }
            if (ModelState.IsValid)
            {

               string Authenticated = AuthenticationServices.GetToken(model);
                
               return Request.CreateResponse(HttpStatusCode.OK, Authenticated);

            }
            else
            {

                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "", Validation: ModelState.AllErrors());
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }
            return null;
        }

    }
}
