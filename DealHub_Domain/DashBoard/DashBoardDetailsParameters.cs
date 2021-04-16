using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealHub_Domain.DashBoard
{
   public class DashBoardDetailsParameters
    {
        public uint ? obf_id { get; set; }
        public string project_name { get; set; }
        public string code { get; set; }
        public string opp_id { get; set; }
        public string created_by { get; set; }
        public string date_created { get; set; }

        public uint? vertical_id { get; set; }

        public string vertical { get; set; }
        public string project_type { get; set; }

        public string project_terms { get; set; }








    }
}
