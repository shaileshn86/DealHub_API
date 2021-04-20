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

        [Required]
        public long ErrorLogId { get; set; }
        [MaxLength(256)]
        public string Message { get; set; }
        [MaxLength(21844)]
        public string SourceStackTrace { get; set; }
        [MaxLength(2400)]
        public string Parameters { get; set; }
        [MaxLength(45)]
        public string ActionName { get; set; }
        [MaxLength(45)]
        public string PageName { get; set; }
        [MaxLength(1024)]
        public string URL { get; set; }
        [MaxLength(45)]
        public string AppId { get; set; }
        public string IpAddress { get; set; }
        public DateTime? CreatedDate { get; set; }
        [MaxLength(45)]
        public string CreatedBy { get; set; }

    }
}
