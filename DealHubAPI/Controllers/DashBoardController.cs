using DealHubAPI.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DealHub_Domain.DashBoard;
using DealHub_Domain.Helpers;
using DealHub_Service.Implemantations.APIServices;
using DealHub_Domain.Enum;
using Newtonsoft.Json;
using DealHub_Service.Implemantations;

namespace DealHubAPI.Controllers
{
    [RoutePrefix("Api/DashBoard")]
    [AuthenticationFilterDealhUb]
    public class DashBoardController : ApiController
    {
        public ReponseMessage result = new ReponseMessage();
        [HttpPost]
        [Route("GetDashBoardData")]
        public HttpResponseMessage GetDashBoardData(DashBoardParameters model)
        {
            if (model == null)// Incase Post Object Is Null or Not Match and Object value is null
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: commaonerrormessage.errormessage);
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }
            if (ModelState.IsValid)

            {
                List<DashBoardDetailsParameters> _DashBoardDetailsParameters = DashBoardServices.GetDashBoardData(model);

                if (_DashBoardDetailsParameters != null)
                {
                    if (_DashBoardDetailsParameters.Count != 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(_DashBoardDetailsParameters));
                    }
                    else
                    {
                        result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: commaonerrormessage.datanotfound);
                        return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                    }



                }
                else
                {
                    result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: commaonerrormessage.errormessage);
                    return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                }

            }
            else
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: ModelState.ReturnErrors());
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);

            }

            return null;
        }

        [AuthenticationFilterDealhUb, HttpPost]
        [Route("GetDashBoardDataCount")]
        public HttpResponseMessage GetDashBoardDataCount(DashBoardParameters model)
        {
            if (model == null)// Incase Post Object Is Null or Not Match and Object value is null
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: commaonerrormessage.errormessage);
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }
            if (ModelState.IsValid)
            {
                List<DashBoardDetailsCountParameters> _DashBoardDetailsCountParameters = DashBoardServices.GetDashBoardDataCount(model);

                if (_DashBoardDetailsCountParameters != null)
                {
                    if (_DashBoardDetailsCountParameters.Count != 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(_DashBoardDetailsCountParameters));
                    }
                    else
                    {
                        result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: commaonerrormessage.datanotfound);
                        return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                    }



                }
                else
                {
                    result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: commaonerrormessage.errormessage);
                    return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                }

            }
            else
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: ModelState.ReturnErrors());
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);

            }

            return null;
        }

        /// <summary>
        /// OBF SummaryDetails 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // Updated By Ramajor
        // [AllowAnonymous]
        [HttpPost]
        [Route("GetOBFSummaryDetails")]
        public HttpResponseMessage GetOBFSummaryDetails(CommonDetailsParameter model)
        {
            try
            {


                if (model == null)
                {
                    result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: commaonerrormessage.errormessage);
                    return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                }
                else
                if (model.dh_id == 0)
                {
                    result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "DhId Must be required!");
                    return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                }

                if (ModelState.IsValid)
                {
                    string json = DashBoardServices.GetOBFSummaryDetails(Convert.ToInt32(model.dh_id));

                    if (json == "" || json == "error")
                    {
                        result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: commaonerrormessage.datanotfound);
                        return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, json);
                    }
                }
                else
                {
                    string jsonOBJ = JsonConvert.SerializeObject(ModelState.AllErrors());
                    result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: ModelState.ReturnErrors());
                    return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                }


            }
            catch (Exception ex)
            {

                string jsonOBJ = JsonConvert.SerializeObject(model);
                // LogExceptionToDB(ex, "Login", GetRequestURL(), jsonOBJ, "UserController", GetUserIp());
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: commaonerrormessage.errormessage);

                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }


        }

        /// <summary>
        /// Detail Timeline History
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // Updated By Ramajor
        //  [AllowAnonymous]
        [HttpPost]
        [Route("GetDetailTimelineHistory")]
        public HttpResponseMessage GetDetailTimelineHistory(CommonDetailsParameter model)
        {
            try
            {
                if (model == null)
                {
                    result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: commaonerrormessage.errormessage);
                    return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                }
                else
            if (model.dh_id == 0 || model.dh_header_id == 0)
                {
                    result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "dh_id or  dh_header_id Must be required!");
                    return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                }


                List<timelinehistroy> _DashBoardDetailsParameters = DashBoardServices.GetDetailTimelineHistory(Convert.ToInt32(model.dh_id), Convert.ToInt32(model.dh_header_id));
                if (_DashBoardDetailsParameters.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, _DashBoardDetailsParameters);


                }
                else
                {
                    result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: commaonerrormessage.datanotfound);
                    return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                }

                //if (_DashBoardDetailsParameters != null)
                //{
                //    if (_DashBoardDetailsParameters.Count != 0)
                //    {
                //  return Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(_DashBoardDetailsParameters));

                //}
                //else
                //{
                //    result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: commaonerrormessage.errormessage);
                //    return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                //}



                //}
                //else
                //{
                //    result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: commaonerrormessage.errormessage);
                //    return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                //}


            }
            catch (Exception ex)
            {


                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: commaonerrormessage.errormessage);

                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }


        }

        /// <summary>
        /// OBF SummaryDetails Version
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // Updated By Ramajor
        [HttpPost]
        // [AllowAnonymous]
        [Route("GetOBFSummaryDetails_version")]
        public HttpResponseMessage GetOBFSummaryDetails_version(CommonDetailsParameter model)
        {
            try
            {
                if (model == null)
                {
                    result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: commaonerrormessage.errormessage);
                    return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                }
                else
           if (model.dh_id == 0 || model.dh_header_id == 0)
                {
                    result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "dh_id or  dh_header_id Must be required!");
                    return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                }

                string json = DashBoardServices.GetOBFSummaryDetails_version(Convert.ToInt32(model.dh_id), Convert.ToInt32(model.dh_header_id));
                if (json == "")
                {
                    result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: commaonerrormessage.datanotfound);
                    return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, json);
                }
            }
            catch (Exception ex)
            {

                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: commaonerrormessage.errormessage);

                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }

        }


        [AllowAnonymous, HttpPost]
        [Route("SendEmailAlert")]
        public HttpResponseMessage SendEmailAlert(testforregularexpression model)
        {
            try
            {
                if (model != null)
                {
                    if (ModelState.IsValid)
                    {
                        EmailSendingService.EmailSendTest();
                        return Request.CreateResponse(HttpStatusCode.OK, model.testmail);
                    }
                    else
                    {
                        result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: ModelState.ReturnErrors());
                        return Request.CreateResponse(HttpStatusCode.BadRequest, result);

                    }
                }
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, "Invalid value in model");

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "");

            }




        }

        /// <summary>
        /// Send Email Alert OBFPPL
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // Updated By Ramajor
        [HttpPost]
        // [AllowAnonymous]
        [Route("SendEmailAlert_OBFPPL")]
        public HttpResponseMessage SendEmailAlert_OBFPPL(EntityMainParameter model)//int _dh_header_id, int _is_shared
        {
            try
            {

                if (model == null)
                {
                    result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: commaonerrormessage.errormessage);
                    return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                }
                else
              if (model._dh_header_id == 0)
                {
                    result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "dh_header_id must be required!");
                    return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                }

                List<commanmessges> _commanmessges = EmailSendingService.Email_Sending_Details(model._dh_header_id, model._is_shared);

                if (_commanmessges != null)
                {
                    if (_commanmessges.Count != 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(_commanmessges));
                    }
                    else
                    {
                        result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: commaonerrormessage.errormessage);
                        return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                    }



                }
                else
                {
                    result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: commaonerrormessage.errormessage);
                    return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                }



            }
            catch (Exception)
            {

                throw;
            }



        }

        /// <summary>
        /// Get System Notification
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // Updated By Ramajor
        //[AllowAnonymous]
        [HttpPost]
        [Route("Get_System_Notification")]
        public HttpResponseMessage Get_System_Notification(DashBoardParameters model)//string _user_code
        {
            try
            {

                if (model == null)
                {
                    result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: commaonerrormessage.errormessage);
                    return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                }
                if (string.IsNullOrEmpty(model._user_code))
                {
                    result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "User code must be required!");
                    return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                }

                string json = SystemNotificationService.Get_System_Notification(model._user_code);
                if (json == "")
                {
                    result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: commaonerrormessage.datanotfound);
                    return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, json);
                }

            }
            catch (Exception ex)
            {

                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: commaonerrormessage.errormessage);

                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }



        }


        [HttpPost]
        [AuthenticationFilterDealhUb]
        [Route("Update_System_Notification")]
        public HttpResponseMessage Update_System_Notification(List<systemnotificationparameters> model)
        {
            if (model == null)// Incase Post Object Is Null or Not Match and Object value is null
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: commaonerrormessage.errormessage);
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }
            if (ModelState.IsValid)
            {
                List<commanmessges> _commanmessges = SystemNotificationService.Update_System_Notification(model);

                if (_commanmessges != null)
                {
                    if (_commanmessges.Count != 0)
                    {

                        return Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(_commanmessges));
                    }
                    else
                    {
                        result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: commaonerrormessage.errormessage);
                        return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                    }



                }
                else
                {
                    result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: commaonerrormessage.errormessage);
                    return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                }
            }
            else
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: ModelState.ReturnErrors());
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);

            }
            return null;
        }
        /// <summary>
        ///  Dashboard Progress
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // Updated By Ramajor
        [HttpPost]
        //  [AllowAnonymous]
        [Route("GetDashboardProgress")]
        public HttpResponseMessage GetDashboardProgress(CommonDetailsParameter model)//string dh_id
        {
            try
            {


                string json = DashBoardServices.GetDashboardProgress(Convert.ToInt32(model.dh_id));
                if (json == "")
                {
                    result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: commaonerrormessage.datanotfound);
                    return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, json);
                }

            }
            catch (Exception ex)
            {

                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: ex.Message);

                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }
        }


        [Route("ShareOBF")]
        [AllowAnonymous]
        public HttpResponseMessage ShareOBF(ShareEmailParameters model)
        {
            List<commanmessges> _commanmessges = EmailSendingService.ShareEmail(model);

            if (_commanmessges != null)
            {
                if (_commanmessges.Count != 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(_commanmessges));
                }
                else
                {
                    result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: commaonerrormessage.errormessage);
                    return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                }
            }
            return null;

            //    if (!string.IsNullOrEmpty(cookieToken))
            //    {
            //        response.Headers.AddCookies(new[]
            //        {
            //    new System.Net.Http.Headers.CookieHeaderValue("xsrf-token", cookieToken)
            //    {
            //        Expires = DateTimeOffset.Now.AddMinutes(10),
            //        Path = "/"
            //    }
            //});
            //    }

            //    return response;
            //}
        }
    }
}
