using DealHub_Domain.DashBoard;
using DealHub_Domain.Enum;
using DealHub_Domain.Helpers;
using DealHub_Service.Implemantations.APIServices;
using DealHubAPI.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace DealHubAPI.Controllers
{
    [RoutePrefix("Api/Manage_OBF")]
    public class OBFCreationController : BaseApiController
    {
        [HttpPost]
        [AllowAnonymous]
        [Route("CreateOBF")]
        public HttpResponseMessage CreateOBF(ObfCreationParameters model)
        {
            if (model == null)// Incase Post Object Is Null or Not Match and Object value is null
            {
                result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Object is null");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }
            if (ModelState.IsValid)
            {
                List<ObfCreationDetailsParameters> _ObfCreationDetailsParameters = ObfServices.ObfCreation(model);

                if (_ObfCreationDetailsParameters != null)
                {
                    if (_ObfCreationDetailsParameters.Count != 0)
                    {

                        return Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(_ObfCreationDetailsParameters));
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
        [Route("GetMasterOBF")]
        public HttpResponseMessage GetMasterOBF(string userid)
        {
            string json = ObfServices.GetMastersOBFCreation(userid);
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
        [Route("getmastersolutions")]
        public HttpResponseMessage get_master_solutions()
        {
            List<SolutionCategory> _SolutionCategory = ObfServices.get_master_solutions();
            if (_SolutionCategory != null)
            {
                if (_SolutionCategory.Count != 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(_SolutionCategory));
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
    }
}
