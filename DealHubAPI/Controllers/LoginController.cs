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
using DealHub_Service.Implemantations.ErrorLog;

namespace DealHubAPI.Controllers
{
    
    [RoutePrefix("Api/Auth")]
   
    public class LoginController : BaseApiController
    {
        public static List<UserKeyModel> LoginKey = new List<UserKeyModel>();
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
               string _SecretKey=  VerifyClientIDkey(model);
                if (_SecretKey == "401")
                {
                    result = new ReponseMessage(MsgNo: HttpStatusCode.Unauthorized.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Invalid client!");
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, result);

                }
               // string password = AuthenticationServices.DecryptStringAES(model._SecretKey, model._password);
                string password = AuthenticationServices.DecryptStringAES(_SecretKey, model._password);
                password = AuthenticationServices.ReturnMD5Hash(password);
                model._password = password;

                string usercode = AuthenticationServices.DecryptStringAES(_SecretKey, model._user_code);
                //password = AuthenticationServices.ReturnMD5Hash(password);
                model._user_code = usercode;
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
                        Random rnd = new Random();
                        int randomnum = rnd.Next(110000, 999999);
                        string Keynew = "0c24f9de!b";
                        Keynew = Keynew + randomnum;
                        string key = Utility.SecretkeyGenerator.CreateToken(auth.user_code,auth.password);
                        login.user.Api_Key = key+ "*$"+ randomnum;
                        //login.user.UserCode = auth.user_code;
                        //login.user.UC = AuthenticationServices.EncryptStringAES(CommonFunctions.CommonKeyClass.Key, auth.user_code); 
                        // login.user.privilege_name = auth.privilege_name;
                        //login.user.role_name = auth.role_name;
                        // login.user.UserId = auth.user_id;
                        login.user.UC = AuthenticationServices.EncryptStringAES(Keynew, auth.user_code);
                        login.user.PN = AuthenticationServices.EncryptStringAES(Keynew, auth.privilege_name);
                        login.user.RN = AuthenticationServices.EncryptStringAES(Keynew, auth.role_name);
                        login.user.UN = auth.UserName;
                        login.user.UI = AuthenticationServices.EncryptStringAES(Keynew, auth.user_id.ToString());
                        login.user.ispasswordchanged = auth.ispasswordchanged;
                        login.user.AntiforgeryKey = AnitiforgeryVerify.RequestKey(auth.user_code);
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


        [AuthenticationFilterDealhUb,HttpPost]
        //[AllowAnonymous]
        [Route("DeleteToken")]
        public HttpResponseMessage DeleteToken(TokenRequestParameter model)
        {
            if (model == null)// Incase Post Object Is Null or Not Match and Object value is null
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }
            if (ModelState.IsValid)
            {
                string usercode = AuthenticationServices.DecryptStringAES(CommonFunctions.CommonKeyClass.Key, model._user_code);
                model._user_code = usercode;
                DeleteTokenResponse _DeleteTokenResponse = AuthenticationServices.DeleteToken(model._user_code);

                if (_DeleteTokenResponse != null)
                {
                    if (_DeleteTokenResponse.result == "Success")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(_DeleteTokenResponse));
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
            try
            {
                if (model == null)// Incase Post Object Is Null or Not Match and Object value is null
                {
                    result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                    return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                }
                if (ModelState.IsValid)
                {
                    string _SecretKey = VerifyClientIDkey(model);
                    if (_SecretKey == "401")
                    {
                        result = new ReponseMessage(MsgNo: HttpStatusCode.Unauthorized.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Invalid client!");
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, result);

                    }
                    // string password = AuthenticationServices.DecryptStringAES(model._SecretKey, model._password);
                    string password = AuthenticationServices.DecryptStringAES(_SecretKey, model._password);
                    password = AuthenticationServices.ReturnMD5Hash(password);
                    model._password = password;

                    string usercode = AuthenticationServices.DecryptStringAES(_SecretKey, model._user_code);
                    //currentpassword = AuthenticationServices.ReturnMD5Hash(currentpassword);
                    model._user_code = usercode;

                    string Authenticated = AuthenticationServices.ResetPassword(model);

                   // return Request.CreateResponse(HttpStatusCode.OK, Authenticated);
                    if (Authenticated != "success")
                        throw new Exception(Authenticated);
                    else if (Authenticated == "success")
                        return Request.CreateResponse(HttpStatusCode.OK, "Password updated successfully");
                    else
                        return Request.CreateResponse(HttpStatusCode.OK, Authenticated);

                }
                else
                {

                    result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "", Validation: ModelState.AllErrors());
                    return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                }
                return null;
            }
            catch (Exception ex)
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: ex.Message, Validation: ModelState.AllErrors());
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("ResetPasswordDashboard")]
        public HttpResponseMessage ResetPasswordDashboard(AuthenticationParameters model)
        {
            try
            {
                if (model == null)// Incase Post Object Is Null or Not Match and Object value is null
                {
                    result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                    return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                }
                if (ModelState.IsValid)
                {
                    string _SecretKey = VerifyClientIDkey(model);
                    if (_SecretKey == "401")
                    {
                        result = new ReponseMessage(MsgNo: HttpStatusCode.Unauthorized.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Invalid client!");
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, result);

                    }
                    // string password = AuthenticationServices.DecryptStringAES(model._SecretKey, model._password);
                    string password = AuthenticationServices.DecryptStringAES(_SecretKey, model._password);
                    password = AuthenticationServices.ReturnMD5Hash(password);
                    model._password = password;

                    string currentpassword = AuthenticationServices.DecryptStringAES(_SecretKey, model._CurrentPassword);
                    currentpassword = AuthenticationServices.ReturnMD5Hash(currentpassword);
                    model._CurrentPassword = currentpassword;

                    string usercode = AuthenticationServices.DecryptStringAES(_SecretKey, model._user_code);
                    //currentpassword = AuthenticationServices.ReturnMD5Hash(currentpassword);
                    model._user_code = usercode;

                    string Authenticated = AuthenticationServices.ResetPasswordDashboard(model);

                    if (Authenticated != "success")
                        throw new Exception(Authenticated);
                    else if (Authenticated == "success")
                        return Request.CreateResponse(HttpStatusCode.OK, "Password updated successfully");
                    else
                        return Request.CreateResponse(HttpStatusCode.OK, Authenticated);

                }
                else
                {

                    result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "", Validation: ModelState.AllErrors());
                    return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                }
                return null;
            }
            catch (Exception ex)
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: ex.Message, Validation: ModelState.AllErrors());
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("sendemail")]

        public HttpResponseMessage sendemail(AuthenticationParameters model)
        {
            //var message = new MailMessage();
          //  var ToEmailId = AuthenticationServices.sendmail(model._user_code);
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

            //below code commented by Kirti on 16-06-2021 for reset password mail send functioanlity 
            //var message = new MailMessage();

            //message.To.Add(new MailAddress(ToEmailId));
            //message.From = new MailAddress("ankita.aherkar96@gmail.com");
            //message.Subject = "Reset Password";
            //message.Body = "Reset Password Link http://localhost:4200/ResetPassword";
            //message.IsBodyHtml = true;
            //using (var smtp = new SmtpClient("smtp.gmail.com", 587))
            //{

            //    //smtp.Credentials = new NetworkCredential("ankita.aherkar96@gmail.com", "Mumbai@12345");
            //    smtp.EnableSsl = true;
            //    //smtp.Send(message);
            //    await smtp.SendMailAsync(message);
            //    await Task.FromResult(0);

            ////}

            //}

            try
            {
                string _SecretKey = VerifyClientIDkey(model);
                if (_SecretKey == "401")
                {
                    result = new ReponseMessage(MsgNo: HttpStatusCode.Unauthorized.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Invalid client!");
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, result);

                }
                // string password = AuthenticationServices.DecryptStringAES(model._SecretKey, model._password);
                //string password = AuthenticationServices.DecryptStringAES(_SecretKey, model._password);
                //password = AuthenticationServices.ReturnMD5Hash(password);
                //model._password = password;
                string usercode = AuthenticationServices.DecryptStringAES(_SecretKey, model._user_code);
                string[] usercodearr = usercode.Split('*'); 
                //usercode = AuthenticationServices.ReturnMD5Hash(usercode);
                model._user_code = usercodearr[0];
                var ToEmailId = AuthenticationServices.sendmail(model._user_code);
                if (ToEmailId == "No Result UnAuthorized")
                    throw new Exception("No email id found");
                EmailSendingProperties EP = new EmailSendingProperties();
                EP.SendTo = new List<EmailToCCParameters>();
                EP.SendCC = new List<EmailToCCParameters>();
                EP.Attachment = new List<EmailAttachmentParameters>();
                EmailToCCParameters To = new EmailToCCParameters();
                To.email_id = ToEmailId;
                EP.SendTo.Add(To);
                EP.subject = "Reset Password";
                EP.body = "Reset Password Link :    " + usercodearr[1];
                EmailSender ES = new EmailSender();
                ES.sendEmail(EP);
                return Request.CreateResponse(HttpStatusCode.OK, "Mail send");
            }
            catch (Exception ex)
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message:ex.Message.ToString());
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
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
                   // ErrorService.writeloginfile("Hit given to :" + postedFile.FileName);
                    // var postedFile = httpRequest.Files["Image"];
                    //Create custom filename
                    if (postedFile != null)
                    {
                        IFileExtensionValidation _ValidateExtension = new FileExtensionValidation();
                        if (! _ValidateExtension.ValidateUploadedExtension(Path.GetExtension(postedFile.FileName), ','))
                        {
                            //throw new Exception("File Extension not allowed for upload");
                            msg = Request.CreateResponse(HttpStatusCode.NotAcceptable, "File not uploaded : File format not supported");
                          //  ErrorService.writeloginfile("Invalid extension of file name " + postedFile.FileName);
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
                          bool IsValidFile=  ValidateFileType(filePath,"All");
                            // IsValidFile == false then delete save file
                            if (!IsValidFile) { 
                                if (File.Exists(filePath))
                                {
                                    File.Delete(filePath);
                                   // ErrorService.writeloginfile("Invalid extension of file name as per magic number " + postedFile.FileName);
                                    return Request.CreateResponse(HttpStatusCode.BadRequest, "File not uploaded : " + imageName+", because file format is not proper");

                                }
                            }
                        }
                        filepathdetails += docpath.ToString() + ",";
                        

                        msg = Request.CreateResponse(HttpStatusCode.OK, filepathdetails);
                    }
                    else
                    {
                       // ErrorService.writeloginfile("Bad Request " + postedFile.FileName);
                        msg = Request.CreateResponse(HttpStatusCode.BadRequest, "File not uploaded : " + imageName);
                    }
                }
                
            }
            catch (Exception ex)
            {
               // ErrorService.writeloginfile("exception in load " + ex.ToString());
                msg = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
            return msg;

        }

        [HttpPost]
        [AllowAnonymous]
        [Route("UploadObfFile")]
        public HttpResponseMessage UploadObfFile()
        {
            string imageName = null;
            HttpResponseMessage msg = new HttpResponseMessage();
            var httpRequest = HttpContext.Current.Request;
            //Upload Image
            string DocsPathMain = ConfigurationManager.AppSettings["APIURL"];//"http://localhost:52229";
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
                        if (!_ValidateExtension.ValidateObfUploadedExtension(Path.GetExtension(postedFile.FileName), ','))
                        {
                            //throw new Exception("File Extension not allowed for upload");
                            msg = Request.CreateResponse(HttpStatusCode.NotAcceptable, "File not uploaded : File format not supported");
                            return msg;
                        }

                        //imageName = new String(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "-");
                        imageName = new String(Path.GetFileNameWithoutExtension(postedFile.FileName).ToArray()).Replace(" ", "-");
                        imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile.FileName);
                        string urlpath = string.Format("/DealHubFiles/{0}/{1}", DateTime.Now.ToString("yyMMdd"), DateTime.Now.Hour.ToString().PadLeft(2, '0')); // "~/Images/" + DateTime.Now.ToString("yymmssfff") + "/" + cDateTime.Now.Hour.ToString().PadLeft(2, '0');
                        docpath = DocsPathMain + urlpath + "/" + imageName; ;
                        string folderpath = string.Format("~/DealHubFiles/{0}/{1}", DateTime.Now.ToString("yyMMdd"), DateTime.Now.Hour.ToString().PadLeft(2, '0')); // "~/Images/" + DateTime.Now.ToString("yymmssfff") + "/" + cDateTime.Now.Hour.ToString().PadLeft(2, '0');



                        if (!Directory.Exists(HttpContext.Current.Server.MapPath(folderpath)))
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
                            bool IsValidFile = ValidateFileType(filePath, "obf");
                            // IsValidFile == false then delete save file
                            if (!IsValidFile)
                            {
                                if (File.Exists(filePath))
                                {
                                    File.Delete(filePath);
                                    return Request.CreateResponse(HttpStatusCode.BadRequest, "File not uploaded : " + imageName + ", because file format is not proper");
                                }
                            }
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

        [AuthenticationFilterDealhUb, HttpPost]
        [Route("deletefile")]
        public HttpResponseMessage deletefile(fileinfo filename)
        {
            string imageName = null;
            HttpResponseMessage msg = new HttpResponseMessage();
            var httpRequest = HttpContext.Current.Request;
            //Upload Image
            string DocsPathMain = ConfigurationManager.AppSettings["APIURL"];//"http://localhost:52229";
            string docpath = "";
            var postedFilenew = httpRequest.Files;
            string filepathdetails = "";
            try
            {
               
                        var filePath = filename.ToString();
                   FileInfo file = new FileInfo(filePath);
                    if (file.Exists)
                        {
                            file.Delete();
                            msg = Request.CreateResponse(HttpStatusCode.OK, "File deleted successfully");
                         }
                        else
                        {
                          msg = Request.CreateResponse(HttpStatusCode.BadRequest,"File not deleted");
                        }
                       // filepathdetails += docpath.ToString() + ",";
                        
                
            }
            catch (Exception ex)
            {
                msg = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
            return msg;

        }
      
        [HttpPost]
        [AllowAnonymous]
        [Route("GetClientKey")]
        public HttpResponseMessage GetClientKey()
        {
            try
            {
               
                //  string aa = DealHubAPI.Utility.AnitiforgeryVerify.RequestKey("123");
                //  string aa2 = DealHubAPI.Utility.AnitiforgeryVerify.VerifyRequestKey("123", aa);
                LoginKey.RemoveAll(s => (DateTime.Now.Subtract(Convert.ToDateTime(s.StampDate.Value)).Minutes > 5));


                Random rand = new Random();
                int randomNumber = rand.Next(1000, 9999);
                int randomNumber2 = rand.Next(1000, 9999);
                //string key = "$!$030!m0l0l" + randomNumber.ToString()+ randomNumber2.ToString();
                string key = Guid.NewGuid().ToString().Replace("-", "!").Substring(0, 12) + randomNumber.ToString() + randomNumber2.ToString();
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(key);
                string currentsecretkey = System.Convert.ToBase64String(plainTextBytes);

                //var userkey= LoginKey.Where(u=>u.KeyID)
                UserKeyModel userkey = new UserKeyModel() { ClientID = Guid.NewGuid().ToString(), Secretkey = currentsecretkey, StampDate = null };
                UserKeyModel userMankey = new UserKeyModel() { ClientID = userkey.ClientID, Secretkey = userkey.Secretkey, StampDate = DateTime.Now };

                // LoginKey.Add(userkey);
                LoginKey.Add(userMankey);
                if (userkey == null)
                {
                    throw new Exception("user key not generated");
                }
                ErrorService.writeloginfile("userkey  " + userkey);
                return Request.CreateResponse(HttpStatusCode.OK, userkey);
            }
            catch (Exception ex)
            {
                ErrorService.writeloginfile("exception in clientkey:  " + ex.ToString());
                return Request.CreateResponse(HttpStatusCode.BadRequest,ex.Message.ToString());
            }
        }
        
       // [HttpPost]
       // [AllowAnonymous]
       // [Route("MyVerifyLogin")]
       // [Throttle(Count =5 ,timeUnit = TimeUnit.Minute)]
        protected string VerifyClientIDkey(AuthenticationParameters model)
        {
            try
            {

                // var skey = LoginKey.Where(u => u.ClientID == model._SecretKey).FirstOrDefault();
                var skey = LoginKey.Where(u => u.ClientID == model._ClientId).FirstOrDefault();
                if (skey != null)
                    {
                        byte[] keyBytes = Convert.FromBase64String(skey.Secretkey);
                        string SecretText = System.Text.Encoding.UTF8.GetString(keyBytes);
                        string mainSecretkey = SecretText.Remove(SecretText.Length - 4);
                        LoginKey.Remove(skey);

                        return mainSecretkey;
                    }
                    else
                    {
                        return HttpStatusCode.Unauthorized.ToCode();
                    }
            }
            catch (Exception ex)
            {
                // Log Error in Error Log
                return HttpStatusCode.Unauthorized.ToCode();
            }
        }


       
    }
}
