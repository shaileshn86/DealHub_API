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
using System.Threading.Tasks;
using System.Text;
using DealHub_Domain.Entity.Logs;
using DealHub_Service.Implemantations.ErrorLog;

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

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<bool> LogExceptionToDB(Exception ex, string ActionName, string url, string Parameters, string PageName, string IpAddress)
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Exception: " + ex.Message);
                sb.AppendLine("Source: " + ex.Source);
                sb.AppendLine("Stack Trace: ");
                if (ex.StackTrace != null)
                {
                    sb.AppendLine(ex.StackTrace);
                }
                if (ex.InnerException != null)
                {
                    sb.AppendLine(", Inner Exception Type: ");
                    sb.Append(ex.InnerException.GetType().ToString());
                    sb.AppendLine(", Inner Exception: ");
                    sb.Append(ex.InnerException.Message);                 
                    sb.AppendLine(" ,Inner Source: ");
                    sb.Append(ex.InnerException.Source);
                    if (ex.InnerException.StackTrace != null)
                    {
                        sb.AppendLine(" , Inner Stack Trace: ");
                        sb.Append(ex.InnerException.StackTrace);
                    }

                }
                sb.AppendLine("Exception Type: ");
                sb.Append(ex.GetType().ToString());



                Errorlogs error = new Errorlogs();
                error.ActionName = ActionName;
                error.AppId = "envoiceapi";
                error.CreatedBy = "MpoddEINVOICE";
                error.Message = ex.Message.ToString();
                error.SourceStackTrace = sb.ToString();
                error.URL = url;
                error.PageName = PageName;
                error.Parameters = Parameters;
                error.IpAddress = IpAddress;
                ErrorService.Add(error);
                return true;
                return true;
            }
            );
        }
    }
}
