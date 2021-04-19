using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using DealHub_Domain.Entity.Logs;
using DealHub_Domain.Implemantations.ErrorLog;
using System;

namespace DealHubAPI.Models
{
    public class CustomLogDelegatHandler
    {

        // string _userName = "";

        //private bool ValidateCredentials(AuthenticationHeaderValue authenticationHeaderVal)
        //{
        //    try
        //    {
        //        if (authenticationHeaderVal != null && !String.IsNullOrEmpty(authenticationHeaderVal.Parameter))
        //        {
        //            string[] decodedCredentials = System.Text.Encoding.ASCII.GetString(Convert.FromBase64String(authenticationHeaderVal.Parameter)).Split(new[] { ':' });

        //            if (decodedCredentials[0].Equals("jasminder") && decodedCredentials[1].Equals("jasminder"))
        //            {
        //                _userName = "Jasminder Singh";
        //                return true;
        //            }
        //        }
        //        return false;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            //if (request.RequestUri.ToString().Contains("/api/PulsenetService") && request.Method.Method == "POST")
            //{
            //    List<string> RequestLogList = new List<string>(){
            //        "SetParameterValue",
            //    };

            //    if (RequestLogList.Any(s=> request.RequestUri.ToString().ToLower().Contains(s.ToLower())) && request.Method.Method == "POST")
            //    {
            //        LogMetadata log = new LogMetadata();

            //        log.requestMethod = request.Method.Method;
            //        log.requestTimestamp = DateTime.Now;
            //        log.requestUri = request.RequestUri.ToString();
            //        log.requestContentType = request.Content.Headers.ContentType.MediaType;
            //        if (request.Content != null)
            //            log.requestContent = await request.Content.ReadAsStringAsync();

            //        var response = await base.SendAsync(request, cancellationToken);


            //        log.responseStatusCode = ((int)response.StatusCode).ToString();
            //        log.responseTimestamp = DateTime.Now;
            //        if (response.Content != null)
            //        {
            //            log.responseContentType = response.Content.Headers.ContentType.MediaType;
            //            log.responseContent = await response.Content.ReadAsStringAsync();
            //        }
            //        await SendToLog(log);

            //        return response;
            //    }
            //    else
            //    {
            //        var response = await base.SendAsync(request, cancellationToken);
            //        //return await base.SendAsync(request, cancellationToken);
            //        return response;
            //    }
            //}
            //else
            //{
            //    var response = await base.SendAsync(request, cancellationToken);
            //    //return await base.SendAsync(request, cancellationToken);
            //    return response;
            //}
            return await base.SendAsync(request, cancellationToken);
        }


        private async Task<bool> SendToLog(LogMetadata logMetadata)
        {
            // TODO: Write code here to store the logMetadata instance to a pre-configured log store...
            // var result = await "";
            return await System.Threading.Tasks.Task.Run(() =>
            {
                string jsonData = JsonConvert.SerializeObject(logMetadata);
                LogApiRequestResponse Req = new LogApiRequestResponse();
                Req.LogRequestData = logMetadata.requestContent;
                Req.LogResponseData = logMetadata.responseContent;
                Req.LogRequestURL = logMetadata.requestUri;
                Req.ReponseStatusCode = logMetadata.responseStatusCode;

                LogApiRequestResponseService.Add(Req);

                return true;

            });
        }
    }
    public class CustomPrincipal : IPrincipal
    {
        public CustomPrincipal(string userName)
        {
            UserName = userName;
            Identity = new GenericIdentity(userName);
        }

        public string UserName { get; set; }
        public IIdentity Identity { get; set; }
        public bool IsInRole(string role)
        {
            if (role.Equals("user"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public class LogMetadata
    {
        public string requestContentType { get; set; }
        public string requestContent { get; set; }
        public string requestUri { get; set; }
        public string requestMethod { get; set; }
        public DateTime? requestTimestamp { get; set; }
        public string responseContentType { get; set; }
        public string responseContent { get; set; }
        public string responseStatusCode { get; set; }
        public DateTime? responseTimestamp { get; set; }
    }
}