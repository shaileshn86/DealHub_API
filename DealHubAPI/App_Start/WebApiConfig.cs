using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Web.Http.Cors;
using WebApiThrottle;
namespace DealHubAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //config.EnableCors(new EnableCorsAttribute("*", "*", "*"));
            config.EnableCors();
            var cors = new System.Web.Http.Cors.EnableCorsAttribute("*", "*", "*");
            //config.EnableCors(cors);
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            
            config.EnableCors(cors);

            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            GlobalConfiguration.Configuration.MessageHandlers.Add(new Models.CustomLogDelegatHandler());
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.MessageHandlers.Add(new ThrottlingHandler()
            {
                Policy = new ThrottlePolicy(perSecond: 2, perMinute: 2*60, perHour: 2*60*60, perDay: 120 * 60 * 24)//, perWeek: 3000
                {
                    IpThrottling = true
                },
                Repository = new CacheRepository()
            });
           // SetThrottleURL(config);
        }

        /// <summary>
        /// Rate limit Url Settings
        /// </summary>
        /// <param name="config"></param>
        public static void SetThrottleURL(HttpConfiguration config)
        {
            config.MessageHandlers.Add(new ThrottlingHandler()
            {
                // Generic rate limit applied to ALL APIs
                Policy = new ThrottlePolicy(perSecond: 1, perMinute: 20, perHour: 200)
                {
                    IpThrottling = true,

                    IpRules = new Dictionary<string, RateLimits>
                            {
                                { "::1/10", new RateLimits { PerSecond = 2 } },
                                //{ "192.168.2.1", new RateLimits { PerMinute = 30, PerHour = 30*60, PerDay = 30*60*24 }}
                            },
                    //white list the "::1" IP to disable throttling on localhost
                    IpWhitelist = new List<string> { "127.0.0.1", "192.168.0.0/24" },

                    ClientThrottling = true,
                    EndpointThrottling = true,
                    
                //    EndpointRules = new Dictionary<string, RateLimits>
                //{ 
                ////Fine tune throttling per specific API here
                //// Login
                //{ "/Api/Auth/Login", new RateLimits { PerSecond = 1, PerMinute = 100, PerHour = 1000 } },
                //{ "/Api/Auth/GetMenuDetails", new RateLimits { PerSecond = 1, PerMinute = 100, PerHour = 1000 } },
                //{ "/Api/Auth/DeleteToken", new RateLimits { PerSecond = 1, PerMinute = 100, PerHour = 1000 } },
                //{ "/Api/Auth/RemindMe", new RateLimits { PerSecond = 1, PerMinute = 100, PerHour = 1000 } },
                //{ "/Api/Auth/ResetPassword", new RateLimits { PerSecond = 1, PerMinute = 100, PerHour = 1000 } },
                //{ "/Api/Auth/sendemail", new RateLimits { PerSecond = 1, PerMinute = 100, PerHour = 1000 } },
                //{ "/Api/Auth/UploadImage", new RateLimits { PerSecond = 5, PerMinute = 100, PerHour = 1000 } },
                //{ "/Api/Auth/GetClientKey", new RateLimits { PerSecond = 2, PerMinute = 100, PerHour = 1000 } },

                ////DashBoard
                //{ "/Api/DashBoard/GetDashBoardData", new RateLimits { PerSecond = 1, PerMinute = 100, PerHour = 1000 } },
                //{ "/Api/DashBoard/GetDashBoardDataCount", new RateLimits { PerSecond = 1, PerMinute = 100, PerHour = 1000 } },
                //{ "/Api/DashBoard/GetOBFSummaryDetails", new RateLimits { PerSecond = 1, PerMinute = 100, PerHour = 1000 } },
                //{ "/Api/DashBoard/GetDetailTimelineHistory", new RateLimits { PerSecond = 1, PerMinute = 100, PerHour = 1000 } },
                //{ "/Api/DashBoard/GetOBFSummaryDetails_version", new RateLimits { PerSecond = 1, PerMinute = 100, PerHour = 1000 } },
                //{ "/Api/DashBoard/SendEmailAlert", new RateLimits { PerSecond = 1, PerMinute = 100, PerHour = 1000 } },
                //{ "/Api/DashBoard/SendEmailAlert_OBFPPL", new RateLimits { PerSecond = 1, PerMinute = 100, PerHour = 1000 } },
                //{ "/Api/DashBoard/Get_System_Notification", new RateLimits { PerSecond = 1, PerMinute = 100, PerHour = 1000 } },
                //{ "/Api/DashBoard/Update_System_Notification", new RateLimits { PerSecond = 1, PerMinute = 100, PerHour = 1000 } },
                //{ "/Api/DashBoard/GetDashboardProgress", new RateLimits { PerSecond = 1, PerMinute = 100, PerHour = 1000 } },
                ////Manage_OBF
                //{ "/Api/Manage_OBF/CreateOBF", new RateLimits { PerSecond = 1, PerMinute = 100, PerHour = 1000 } },
                //{ "/Api/Manage_OBF/EditCustomerCodeandIo", new RateLimits { PerSecond = 1, PerMinute = 100, PerHour = 1000 } },
                //{ "/Api/Manage_OBF/SaveServiceSolutionSector", new RateLimits { PerSecond = 1, PerMinute = 100, PerHour = 1000 } },
                //{ "/Api/Manage_OBF/SubmitOBF", new RateLimits { PerSecond = 1, PerMinute = 100, PerHour = 1000 } },
                //{ "/Api/Manage_OBF/GetMasterOBF", new RateLimits { PerSecond = 1, PerMinute = 100, PerHour = 1000 } },
                //{ "/Api/Manage_OBF/getmastersolutions", new RateLimits { PerSecond = 1, PerMinute = 100, PerHour = 1000 } },
                //{ "/Api/Manage_OBF/geteditobfdata", new RateLimits { PerSecond = 1, PerMinute = 100, PerHour = 1000 } },
                //{ "/Api/Manage_OBF/ApproveRejectObf", new RateLimits { PerSecond = 1, PerMinute = 100, PerHour = 1000 } },
                //{ "/Api/Manage_OBF/SaveAttachmentDetails", new RateLimits { PerSecond = 1, PerMinute = 100, PerHour = 1000 } },
                //{ "/Api/Manage_OBF/GetOBFSummaryDataVersionWise", new RateLimits { PerSecond = 1, PerMinute = 100, PerHour = 1000 } },
                //{ "/Api/Manage_OBF/GetAttachmentDocument", new RateLimits { PerSecond = 1, PerMinute = 100, PerHour = 1000 } }
                


                //}
                },
                Repository = new CacheRepository()
            });
        }

    }

    
}
