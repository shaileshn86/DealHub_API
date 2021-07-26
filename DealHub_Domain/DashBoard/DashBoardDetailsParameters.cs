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
        public uint dh_header_id { get; set; }
        public uint dh_phase_id { get; set; }
        public string phase_code { get; set; }
        public string Current_Status { get; set; }
        
        public string Project_Name { get; set; }
        public string Code { get; set; }
        public string Opp_Id { get; set; }
        public DateTime Created_On { get; set; }
        public DateTime onhold_datetime { get; set; }
        
        public uint Created_By { get; set; }
       

        public Decimal? Total_Cost { get; set; }

        public Decimal? Total_Revenue { get; set; }

        public Decimal? Gross_Margin { get; set; }
        public string mainobf { get; set; }
        public string version_name { get; set; }
        public string currentstatus { get; set; }

        public string shortcurrentstatus { get; set; }
        public int ppl_init { get; set; }

        public string ppl_status { get; set; }

        public string customer_name { get; set; }

        public string dh_location { get; set; }

        public string Vertical_Name { get; set; }

        public string sap_customer_code { get; set; }

        public string sector_name { get; set; }

        public string subsector_name { get; set; }

        public string solutioncategory_name { get; set; }

        public string currentstatus_search { get; set; }

        public int is_submitted { get; set; }

        public string Project_Type { get; set; }

       public int progresspercentage { get; set; }

      public int IsFinalAggUpload { get; set; }
      public int is_on_hold { get; set; }
      public string onholdcomment { get; set; }
      public string onhold_commentedby { get; set; }
        
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

    public class timelinehistroy
    {
        public uint dh_id { get; set; }
        public uint dh_header_id { get; set; }
        public string username { get; set; }
        public string currentstatus { get; set; }
        public string comments { get; set; }
        public string TimeLine { get; set; }
        public string actions { get; set; }
    }

}
