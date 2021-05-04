using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealHub_Domain.DashBoard
{
   public class DashBoardDetailsParameters
    {
        public string ApprovalStatus { get; set; }

        public string CurrentStatus { get; set; }
        public string DetailedOBF { get; set; }

        public string FinalAgg { get; set; }
        //public uint ? obf_id { get; set; }
        public string ProjectName { get; set; }
        public string Code { get; set; }
        public string Opp_Id { get; set; }
        public string Created_On { get; set; }
        public string Created_By { get; set; }
       

        //public uint? vertical_id { get; set; }

        public string vertical { get; set; }
        public string Project_Type { get; set; }

        public string Payament_Terms { get; set; }

        public Decimal? Capex { get; set; }

        public Decimal? Total_Cost { get; set; }

        public string  Total_Revenue { get; set; }

        public string  Gross_Margin { get; set; }


      









    }


    public class DashBoardDetailsCountParameters
    {
        public long count { get; set; }


        public int process_id { get; set; }

        public string process_code { get; set; }
    }
}
