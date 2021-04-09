using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DealHubAPI.Utility
{
    public class StaticGlobalKey
    {
        public static Dictionary<string, object> AppGlobalKey = new Dictionary<string, object>{
             {"HEADER-API-KEY","" } // Add here new key
         };
    }
}