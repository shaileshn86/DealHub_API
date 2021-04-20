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
using System.Web.Http.Cors;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Threading.Tasks;
using DealHub_Domain.MenuBinding;

namespace DealHubAPI.Controllers
{
    [RoutePrefix("Api/Auth")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
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
                        result = new ReponseMessage(MsgNo: HttpStatusCode.Unauthorized.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "", Validation: ModelState.AllErrors());
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, result);
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
        [HttpPost]
        [AllowAnonymous]
        [Route("ResetPassword")]
        public HttpResponseMessage ResetPassword(AuthenticationParameters model)
        {
            if (model == null)// Incase Post Object Is Null or Not Match and Object value is null
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }
            if (ModelState.IsValid)
            {

                string Authenticated = AuthenticationServices.ResetPassword(model);

                return Request.CreateResponse(HttpStatusCode.OK, Authenticated);

            }
            else
            {

                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "", Validation: ModelState.AllErrors());
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }
            return null;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("sendemail")]

        public async Task sendemail(AuthenticationParameters model)
        {
            var message = new MailMessage();
            var ToEmailId = AuthenticationServices.sendmail(model._user_code);
            message.To.Add(new MailAddress(ToEmailId));
            message.From = new MailAddress("ankita.aherkar96@gmail.com");
            message.Subject = "Reset Password";
            message.Body = "Reset Password Link http://localhost:4200/ResetPassword?Usercode="+model._user_code;
            message.IsBodyHtml = true;
            using (var smtp = new SmtpClient("smtp.gmail.com", 587))
            {

                smtp.Credentials = new NetworkCredential("ankita.aherkar96@gmail.com", "Mumbai@12345");
                smtp.EnableSsl = true;
                //smtp.Send(message);
                await smtp.SendMailAsync(message);
                await Task.FromResult(0);

            }
          
        }
    }
}
