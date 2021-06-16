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
using System.Configuration;
using System.Web;
using System.IO;
using DealHub_Domain.DashBoard;
using DealHub_Dal.OBF;
using DealHubAPI.CommonFunctions;

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
                string password = AuthenticationServices.DecryptStringAES(model._SecretKey,model._password);
                model._password = password;
                List<AuthenticationDetailParameters> _AuthenticationDetailParameters = AuthenticationServices.GetAuthenticateUser(model);
                foreach (AuthenticationDetailParameters auth in _AuthenticationDetailParameters)
                {
                    if(auth.status!= "success")
                    {
                       
                            result = new ReponseMessage(MsgNo: HttpStatusCode.Unauthorized.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: auth.status, Validation: ModelState.AllErrors());
                            return Request.CreateResponse(HttpStatusCode.Unauthorized, result);
                        
                        
                    }
                    else
                    {
                       LoginResponse login = new LoginResponse();
                        string key = Utility.SecretkeyGenerator.CreateToken(auth.user_code,auth.password);
                        login.user.Api_Key = key;
                        login.user.UserCode = auth.user_code;
                        login.user.privilege_name = auth.privilege_name;
                        login.user.role_name = auth.role_name;
                        login.user.UserName = auth.UserName;
                        login.user.UserId = auth.user_id;
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
            //var message = new MailMessage();
            var ToEmailId = AuthenticationServices.sendmail(model._user_code);
            //message.To.Add(new MailAddress(ToEmailId));
            //message.From = new MailAddress("ankita.aherkar96@gmail.com");
            //message.Subject = "Reset Password";
            //message.Body = "Reset Password Link http://localhost:4200/ResetPassword?Usercode=" + model._user_code;
            //message.IsBodyHtml = true;
            //using (var smtp = new SmtpClient("smtp.gmail.com", 587))
            //{
            //    smtp.Host = "10.2.202.42";
            //    smtp.Credentials = new NetworkCredential("ankita.aherkar96@gmail.com", "Mumbai@12345");
            //    smtp.EnableSsl = true;
            //    //smtp.Send(message);
            //    await smtp.SendMailAsync(message);
            //    await Task.FromResult(0);
            var message = new MailMessage();
            
            message.To.Add(new MailAddress(ToEmailId));
            message.From = new MailAddress("ankita.aherkar96@gmail.com");
            message.Subject = "Reset Password";
            message.Body = "Reset Password Link http://localhost:4200/ResetPassword";
            message.IsBodyHtml = true;
            using (var smtp = new SmtpClient("smtp.gmail.com", 587))
            {

                //smtp.Credentials = new NetworkCredential("ankita.aherkar96@gmail.com", "Mumbai@12345");
                smtp.EnableSsl = true;
                //smtp.Send(message);
                await smtp.SendMailAsync(message);
                await Task.FromResult(0);

            //}

            }
          
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("UploadImage")]
        public HttpResponseMessage UploadImage()
        {
            string imageName = null;
            HttpResponseMessage msg = new HttpResponseMessage();
            var httpRequest = HttpContext.Current.Request;
            //Upload Image
            string DocsPathMain =  ConfigurationManager.AppSettings["APIURL"];//"http://localhost:52229";
            string docpath = "";
            var postedFilenew = httpRequest.Files;
            string filepathdetails = "";
            try
            {
                foreach (string fileName in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[fileName];
                    // var postedFile = httpRequest.Files["Image"];
                    //Create custom filename
                    if (postedFile != null)
                    {
                        IFileExtensionValidation _ValidateExtension = new FileExtensionValidation();
                        if (! _ValidateExtension.ValidateUploadedExtension(Path.GetExtension(postedFile.FileName), ','))
                        {
                            //throw new Exception("File Extension not allowed for upload");
                            msg = Request.CreateResponse(HttpStatusCode.NotAcceptable, "File not uploaded : File format not supported");
                            return msg;
                        }

                        //imageName = new String(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "-");
                        imageName = new String(Path.GetFileNameWithoutExtension(postedFile.FileName).ToArray()).Replace(" ", "-");
                        imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile.FileName);
                        string urlpath = string.Format("/DealHubFiles/{0}/{1}", DateTime.Now.ToString("yyMMdd"), DateTime.Now.Hour.ToString().PadLeft(2, '0')); // "~/Images/" + DateTime.Now.ToString("yymmssfff") + "/" + cDateTime.Now.Hour.ToString().PadLeft(2, '0');
                        docpath = DocsPathMain + urlpath + "/" +imageName; ;
                        string folderpath = string.Format("~/DealHubFiles/{0}/{1}", DateTime.Now.ToString("yyMMdd"), DateTime.Now.Hour.ToString().PadLeft(2, '0')); // "~/Images/" + DateTime.Now.ToString("yymmssfff") + "/" + cDateTime.Now.Hour.ToString().PadLeft(2, '0');
                        


                        if (!Directory.Exists(HttpContext.Current.Server.MapPath(folderpath)) )
                        {
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath(folderpath));
                        }

                        var filePath = HttpContext.Current.Server.MapPath(folderpath + "/" + imageName);
                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);

                        }
                        else
                        {
                            postedFile.SaveAs(filePath);
                        }
                        filepathdetails += docpath.ToString() + ",";
                        msg = Request.CreateResponse(HttpStatusCode.OK, filepathdetails);
                    }
                    else
                    {
                        msg = Request.CreateResponse(HttpStatusCode.BadRequest, "File not uploaded : " + imageName);
                    }
                }
                
            }
            catch (Exception ex)
            {
                msg = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
            return msg;

        }

        
    }
}
