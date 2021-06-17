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
    public class DashBoardController : ApiController
    {
        public ReponseMessage result = new ReponseMessage();
        [AuthenticationFilterDealhUb, HttpPost]

        [Route("GetDashBoardData")]
        public HttpResponseMessage GetDashBoardData(DashBoardParameters model)
        {
            if (model == null)// Incase Post Object Is Null or Not Match and Object value is null
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
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

        [AuthenticationFilterDealhUb, HttpPost]
        [Route("GetDashBoardDataCount")]
        public HttpResponseMessage GetDashBoardDataCount(DashBoardParameters model)
        {
            if (model == null)// Incase Post Object Is Null or Not Match and Object value is null
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
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

        [HttpGet]
        [AllowAnonymous]
        [Route("GetOBFSummaryDetails")]
        public HttpResponseMessage GetOBFSummaryDetails(string dh_id)
        {
            string json = DashBoardServices.GetOBFSummaryDetails(Convert.ToInt32(dh_id));
            if (json == "" || json == "error")
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, json);
            }


        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetDetailTimelineHistory")]
        public HttpResponseMessage GetDetailTimelineHistory(int dh_id,int dh_header_id)
        {
            
                List<timelinehistroy> _DashBoardDetailsParameters = DashBoardServices.GetDetailTimelineHistory(Convert.ToInt32(dh_id), Convert.ToInt32(dh_header_id));

                if (_DashBoardDetailsParameters != null)
                {
                    if (_DashBoardDetailsParameters.Count != 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(_DashBoardDetailsParameters));
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
            return null;
            
        }


        [HttpGet]
        [AllowAnonymous]
        [Route("GetOBFSummaryDetails_version")]
        public HttpResponseMessage GetOBFSummaryDetails_version(int dh_id, int dh_header_id)
        {
            string json = DashBoardServices.GetOBFSummaryDetails_version(dh_id,dh_header_id);
            if (json == "" || json == "error")
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, json);
            }


        }


        [AllowAnonymous, HttpPost]
        [Route("SendEmailAlert")]
        public HttpResponseMessage SendEmailAlert( int dh_header_id)
        {
            try
            {
                EmailSendingService.EmailSendTest();
                return Request.CreateResponse(HttpStatusCode.OK, "");
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "");

            }

            


        }


        [AllowAnonymous, HttpGet]
        [Route("SendEmailAlert_OBFPPL")]
        public HttpResponseMessage SendEmailAlert_OBFPPL(int _dh_header_id, int _is_shared)
        {
           

                List<commanmessges> _commanmessges = EmailSendingService.Email_Sending_Details(_dh_header_id,_is_shared);

                if (_commanmessges != null)
                {
                    if (_commanmessges.Count != 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(_commanmessges));
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
                return null;

           




        }


        [AllowAnonymous, HttpGet]
        [Route("Get_System_Notification")]
        public HttpResponseMessage Get_System_Notification(string _user_code)
        {


            string json = SystemNotificationService.Get_System_Notification(_user_code);
            if (json == "" || json == "error")
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, json);
            }





        }


        [HttpPost]
        [AuthenticationFilterDealhUb]
        [Route("Update_System_Notification")]
        public HttpResponseMessage Update_System_Notification(List<systemnotificationparameters> model)
        {
            if (model == null)// Incase Post Object Is Null or Not Match and Object value is null
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
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


        [HttpGet]
        [AllowAnonymous]
        [Route("GetDashboardProgress")]
        public HttpResponseMessage GetDashboardProgress(string dh_id)
        {
            string json = DashBoardServices.GetDashboardProgress(Convert.ToInt32(dh_id));
            if (json == "" || json == "error")
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, json);
            }


        }
    }
}
