using DealHub_Domain.Enum;
using DealHub_Domain.Helpers;
using DealHubAPI.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using System.Text;

namespace DealHubAPI.Models
{
    public class CustomLogDelegatHandler : DelegatingHandler
    {

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            

           
                var response = await base.SendAsync(request, cancellationToken);
                
            if (response.StatusCode == (System.Net.HttpStatusCode)429)
            {
                
                ReponseMessage result = new ReponseMessage(MsgNo: HttpStatusCode.BadRequest.ToCode(), MsgType: MsgTypeEnum.E.ToString(), Message: "Too Many Requests");
                // return response.Content= result
                string jsonresult = JsonConvert.SerializeObject(result);
              //  var response429 = new HttpResponseMessage((HttpStatusCode)429);

               //  response429.Content = new StringContent(jsonresult.ToString(), Encoding.UTF8, "application/json");
                response.Content = new StringContent(jsonresult.ToString(), Encoding.UTF8, "application/json");
                response.Headers.Add("Access-Control-Allow-Origin", "*");
               // response.Headers.Add("Access-Control-Allow-Headers", "authorization,content-type");
                //var tsc = new TaskCompletionSource<HttpResponseMessage>();
               // tsc.SetResult(response);
               // return await tsc.Task;
            }
         
             
            return response;
           

        }

    }
}