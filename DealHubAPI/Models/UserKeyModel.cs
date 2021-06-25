using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DealHubAPI.Models
{
    public class UserKeyModel
    {
        public  string ClientID { get; set; }
        public string Secretkey { get; set; }
        public DateTime?  StampDate{ get; set; }
    }

    //public class UserKey
    //{
    //    public string ClientID { get; set; }
    //    public string Secretkey { get; set; }
    //    public TimeSpan? Timespan { get; set; }
    //}
}