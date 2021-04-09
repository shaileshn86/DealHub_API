using DealHub_Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.ServiceModel.Channels;

namespace DealHubAPI.Controllers
{
    public class BaseApiController : ApiController
    {
        public ReponseMessage result = new ReponseMessage();

        [ApiExplorerSettings(IgnoreApi = true)]
        public string GetRequestURL()
        {
            string baseUrl = Request.RequestUri.AbsoluteUri;//.GetLeftPart(UriPartial.Authority)+ Request.AbsolutePath;
            return baseUrl;
        }
        //Test for Git

        [ApiExplorerSettings(IgnoreApi = true)]
        public string GetUserIp()
        {
            return GetClientIp();
        }

        private string GetClientIp(HttpRequestMessage request = null)
        {
            request = request ?? Request;

            try
            {


                if (request.Properties.ContainsKey("MS_HttpContext"))
                {
                    return ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
                }
                else if (request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
                {
                    RemoteEndpointMessageProperty prop = (RemoteEndpointMessageProperty)this.Request.Properties[RemoteEndpointMessageProperty.Name];
                    return prop.Address;
                }
                else if (HttpContext.Current != null)
                {
                    return HttpContext.Current.Request.UserHostAddress;
                }
                else
                {
                    return "";
                }

            }
            catch (Exception)
            {

                return "";
            }
        }
    }
}
