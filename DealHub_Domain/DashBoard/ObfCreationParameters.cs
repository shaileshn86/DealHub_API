using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DealHub_Domain.DashBoard
{
   public class ObfCreationParameters
    {

         public int _dh_header_id { get; set; }
        public int _dh_id { get; set; }
        [MaxLength(50)]
        public string _opportunity_id { get; set; }
        [MaxLength(1000)]
        public string _dh_desc { get; set; }
        [MaxLength(100)]
        public string _dh_project_names { get; set; }
        [MaxLength(100)]
        public string _dh_location { get; set; }

        [MaxLength(100)]
        public string _param_projection_period { get; set; }


       












    }
}
