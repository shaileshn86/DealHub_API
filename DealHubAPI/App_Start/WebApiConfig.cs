using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Web.Http.Cors;
using WebApiThrottle;
using System.Configuration;

namespace DealHubAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //config.EnableCors(new EnableCorsAttribute("*", "*", "*"));
            //  config.EnableCors();
            string allowfromorign = ConfigurationManager.AppSettings["allowfromorigin"];
            var cors = new System.Web.Http.Cors.EnableCorsAttribute(allowfromorign, "*", "GET,POST");
            //config.EnableCors(cors);
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            
            config.EnableCors(cors);

            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            ///
            GlobalConfiguration.Configuration.MessageHandlers.Add(new Models.CustomLogDelegatHandler());
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );



            //config.MessageHandlers.Add(new ThrottlingHandler()
            //{
            //    Policy = new ThrottlePolicy(perSecond: 2, perMinute: 2*60, perHour: 2*60*60, perDay: 120 * 60 * 24)//, perWeek: persecond000
            //    {
            //        IpThrottling = true
            //    },
            //    Repository = new CacheRepository()
            //});

            SetThrottleURL(config);
 
           // SetThrottleURL(config);

        }

        /// <summary>
        /// Rate limit Url Settings
        /// </summary>
        /// <param name="config"></param>
        public static void SetThrottleURL(HttpConfiguration config)
        {

            int persecond = 2;
            int permin = 20;
            int perhour = 1000;
            config.MessageHandlers.Add(new ThrottlingHandler()
            {
                // Generic rate limit applied to ALL APIs
                Policy = new ThrottlePolicy(perSecond: persecond, perMinute: 200, perHour: 2000)
                {

                    IpThrottling = true,

                    //IpRules = new Dictionary<string, RateLimits>
                    //        {
                    //            { "::1/persecond", new RateLimits { PerSecond = 2 } },
                    //            //{ "192.168.2.1", new RateLimits { PerMinute = persecond0, PerHour = persecond0*60, PerDay = persecond0*60*24 }}
                    //        },
                    ////white list the "::1" IP to disable throttling on localhost
                    //IpWhitelist = new List<string> { "127.0.0.1", "192.168.0.0/24" },

                    ClientThrottling = true,
                    EndpointThrottling = true,




                    EndpointRules = new Dictionary<string, RateLimits>
                        { 
                        //Fine tune throttling per specific API here
                        // Login
                        { "/Api/Auth/Login", new RateLimits { PerSecond = persecond, PerMinute = permin, PerHour = perhour } },
                        { "/Api/Auth/GetMenuDetails", new RateLimits { PerSecond = persecond, PerMinute = permin, PerHour = perhour } },
                        { "/Api/Auth/DeleteToken", new RateLimits { PerSecond = persecond, PerMinute = permin, PerHour = perhour } },
                        { "/Api/Auth/RemindMe", new RateLimits { PerSecond = persecond, PerMinute = permin, PerHour = perhour } },
                        { "/Api/Auth/ResetPassword", new RateLimits { PerSecond =persecond, PerMinute = permin, PerHour = perhour } },
                        { "/Api/Auth/ResetPasswordDashboard", new RateLimits { PerSecond =persecond, PerMinute = permin, PerHour = perhour } },
                        { "/Api/Auth/sendemail", new RateLimits { PerSecond = persecond, PerMinute = permin, PerHour = perhour } },
                        { "/Api/Auth/UploadImage", new RateLimits { PerSecond = persecond, PerMinute = permin, PerHour = perhour } },
                        { "/Api/Auth/GetClientKey", new RateLimits { PerSecond = persecond, PerMinute = permin, PerHour = perhour } },

                        //DashBoard
                        { "/Api/DashBoard/GetDashBoardData", new RateLimits { PerSecond = persecond, PerMinute = permin, PerHour = perhour } },
                        { "/Api/DashBoard/GetDashBoardDataCount", new RateLimits { PerSecond = persecond, PerMinute = permin, PerHour = perhour } },
                        { "/Api/DashBoard/GetOBFSummaryDetails", new RateLimits { PerSecond = persecond, PerMinute = permin, PerHour = perhour } },
                        { "/Api/DashBoard/GetDetailTimelineHistory", new RateLimits { PerSecond = persecond, PerMinute = permin, PerHour = perhour } },
                        { "/Api/DashBoard/GetOBFSummaryDetails_version", new RateLimits { PerSecond = persecond, PerMinute = permin, PerHour = perhour } },
                        { "/Api/DashBoard/SendEmailAlert", new RateLimits { PerSecond = persecond, PerMinute = permin, PerHour = perhour } },
                        { "/Api/DashBoard/SendEmailAlert_OBFPPL", new RateLimits { PerSecond = persecond, PerMinute = permin, PerHour = perhour } },
                        { "/Api/DashBoard/Get_System_Notification", new RateLimits { PerSecond = persecond, PerMinute = permin, PerHour = perhour } },
                        { "/Api/DashBoard/Update_System_Notification", new RateLimits { PerSecond = persecond, PerMinute = permin, PerHour = perhour } },
                        { "/Api/DashBoard/GetDashboardProgress", new RateLimits { PerSecond = persecond, PerMinute = permin, PerHour = perhour } },
                        //Manage_OBF
                        { "/Api/Manage_OBF/CreateOBF", new RateLimits { PerSecond = persecond, PerMinute = permin, PerHour = perhour } },
                        { "/Api/Manage_OBF/EditCustomerCodeandIo", new RateLimits { PerSecond = persecond, PerMinute = permin, PerHour = perhour } },
                        { "/Api/Manage_OBF/SaveServiceSolutionSector", new RateLimits { PerSecond = persecond, PerMinute = permin, PerHour = perhour } },
                        { "/Api/Manage_OBF/SubmitOBF", new RateLimits { PerSecond = persecond, PerMinute = permin, PerHour = perhour } },
                        { "/Api/Manage_OBF/GetMasterOBF", new RateLimits { PerSecond = persecond, PerMinute = permin, PerHour = perhour } },
                        { "/Api/Manage_OBF/getmastersolutions", new RateLimits { PerSecond = persecond, PerMinute = permin, PerHour = perhour } },
                        { "/Api/Manage_OBF/geteditobfdata", new RateLimits { PerSecond = persecond, PerMinute = permin, PerHour = perhour } },
                        { "/Api/Manage_OBF/ApproveRejectObf", new RateLimits { PerSecond = persecond, PerMinute = permin, PerHour = perhour } },
                        { "/Api/Manage_OBF/SaveAttachmentDetails", new RateLimits { PerSecond = persecond, PerMinute = permin, PerHour = perhour } },
                        { "/Api/Manage_OBF/GetOBFSummaryDataVersionWise", new RateLimits { PerSecond = persecond, PerMinute = permin, PerHour = perhour } },
                        { "/Api/Manage_OBF/GetAttachmentDocument", new RateLimits { PerSecond = persecond, PerMinute = permin, PerHour = perhour } }



                        }
                },
                Repository = new CacheRepository()
            });
        }


    }


}
