using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealHub_Domain.Entity.Logs
{
    public class Errorlogs
    {
        public long ErrorLogId { get; set; }

        public string Message { get; set; }

        public string SourceStackTrace { get; set; }

        public string Parameters { get; set; }

        public string ActionName { get; set; }

        public string PageName { get; set; }

        public string URL { get; set; }

        public string AppId { get; set; }
        public string IpAddress { get; set; }
        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

    }
}
