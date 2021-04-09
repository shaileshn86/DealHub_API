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
            return null;
        }
    }
}
