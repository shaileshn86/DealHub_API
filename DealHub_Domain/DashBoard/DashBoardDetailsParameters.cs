using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealHub_Domain.DashBoard
{
   public class DashBoardDetailsParameters
    {
        //public string ApprovalStatus { get; set; }
        public uint dh_id { get; set; }
        public string CurrentStatus { get; set; }
        //public string DetailedOBF { get; set; }

        //public string FinalAgg { get; set; }
        //public uint ? obf_id { get; set; }
        public string ProjectName { get; set; }
        public string Code { get; set; }
        public string Opp_Id { get; set; }
        public string Created_On { get; set; }
        public string Created_By { get; set; }
       

        //public uint? vertical_id { get; set; }

        public string vertical { get; set; }
        public string Project_Type { get; set; }

        public int Payament_Terms { get; set; }

        public Decimal? Capex { get; set; }

        public Decimal? Total_Cost { get; set; }

        public Decimal? Total_Revenue { get; set; }

        public Decimal? Gross_Margin { get; set; }


      









    }


    public class DashBoardDetailsCountParameters
    {
       public long _draft_obf { get; set; }

       public long _draft_ppl { get; set; }

        public decimal _draft { get; set; }

       public long _submitted_obf { get; set; }


       

        public long _submitted_ppl { get; set; }

        public decimal _submitted { get; set; }


        public long _approved_obf { get; set; }
        public long _approved_ppl { get; set; }

        public long _approved { get; set; }
        
        public long _rejected_obf { get; set; }

        public long _rejected_ppl { get; set; }

        public decimal _rejected { get; set; }



        public decimal _pendingobf { get; set; }
        public decimal _pendingppl { get; set; }

        public Decimal _TotalPending { get; set; }

        public long _totalapprovedppl { get; set; }




        public long _totalapprovedobf { get; set; }

    }
}
