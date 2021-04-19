using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealHub_Domain.Entity.Logs
{
    public class LogApiRequestResponse
    {

        public long LogId { get; set; }

        public string LogRequestURL { get; set; }

        public string LogRequestData { get; set; }

        public string LogResponseData { get; set; }
        public string ReponseStatusCode { get; set; }
        public DateTime LogDateTime { get; set; }
        public int TotalRows { get; set; }


    }
}
