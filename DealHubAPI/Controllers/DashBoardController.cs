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
        [AllowAnonymous, HttpPost]

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

        [AllowAnonymous, HttpPost]
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
    }
}
