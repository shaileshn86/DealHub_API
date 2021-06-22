using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using DealHubAPI.CommonFunctions;
using System.Configuration;

namespace DealHubAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest()
        {
            //Context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetAllowResponseInBrowserHistory(false);
            HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache, no-store");
            HttpContext.Current.Response.AddHeader("Pragma", "no-cache");
            HttpContext.Current.Response.AddHeader("X-Xss-Protection", "1");

            HttpContext.Current.Response.AddHeader("X-Frame-Options", "DENY");
            HttpContext.Current.Response.AddHeader("Referrer-Policy", "no-referrer");
            HttpContext.Current.Response.AddHeader("X-Content-Type-Options", "nosniff");
            HttpContext.Current.Response.AddHeader("Strict-Transport-Security", "max-age=31536000");

            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetNoStore();
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetValidUntilExpires(true);
        }

        protected void Application_PreSendRequestHeaders()
        {
            Response.Headers.Remove("ETag");
            Response.Headers.Remove("Server");
            Response.Headers.Remove("X-AspNet-Version");
            Response.Headers.Remove("X-AspNetMvc-Version");
            Response.Headers.Remove("X-Frame-Options");
            Response.AddHeader("X-Frame-Options", "AllowAll");
        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
            Exception exception = Server.GetLastError();
            if (exception != null)
            {
                Exception ex = Server.GetLastError().GetBaseException();
                //System.Diagnostics.EventLog.WriteEntry("DealHub ERROR : ",
                //"MESSAGE: " + ex.Message +
                //"\nSOURCE: " + ex.Source +
                //"\nFORM: " + Request.Form.ToString() +
                //"\nQUERYSTRING: " + Request.QueryString.ToString() +
                //"\nTARGETSITE: " + ex.TargetSite +
                //"\nSTACKTRACE: " + ex.StackTrace,
                //System.Diagnostics.EventLogEntryType.Error);
                ////Logger.Log(exception);
                //Server.ClearError();
                //Server.Transfer("~/ErrorPage.aspx");
                ILogger LogEvent = new WriteLogToFile();
                string exceptionDetails = 
                "DealHub ERROR : " +
                "MESSAGE: " + ex.Message +
                "\nSOURCE: " + ex.Source +
                "\nFORM: " + Request.Form.ToString() +
                "\nQUERYSTRING: " + Request.QueryString.ToString() +
                "\nTARGETSITE: " + ex.TargetSite +
                "\nSTACKTRACE: " + ex.StackTrace;


                LogEvent.LogEvent(ConfigurationManager.AppSettings["logfilepath"].ToString(), exceptionDetails, true);

            }
        }

    }
}
