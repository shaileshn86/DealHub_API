using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DealHub_Domain.DashBoard
{
   public class ObfCreationParameters: CommonParamter
    {

        

        public string _dh_project_name { get; set; }

        public string _opportunity_id { get; set; }

        public string _dh_location { get; set; }

        public int _vertical_id { get; set; }

        public int _verticalhead_id { get; set; }


        public string _dh_desc { get; set; }

        public int _dh_phase_id { get; set; }
        public int _parent_dh_main_id { get; set; }
      

        public decimal _total_revenue { get; set; }
        public decimal _total_cost { get; set; }

        public decimal _total_margin { get; set; }

        public string _total_project_life { get; set; }

        public decimal _irr_surplus_cash { get; set; }


        public decimal _ebt { get; set; }

        public decimal _capex { get; set; }

        public decimal _irr_borrowed_fund { get; set; }

        public string _is_loi_po_uploaded { get; set; }

        public string _assumptions_and_risks { get; set; }

     

        public string _active { get; set; }

        public string _status { get; set; }

        public int _is_saved { get; set; }

        public int _is_submitted { get; set; }

     

        public string _service_category { get; set; }

        public int _payment_terms { get; set; }

        public string _mode { get; set; }


        public List<SaveAttachmentParameter> Attachments;

        public List<SaveServiceParameter> Services;


        public int _Sector_Id { get; set; }


        public int _SubSector_Id { get; set; }

        public string save_with_solution_sector { get; set; }


        public List<SubmitOBFParameters> _SubmitOBFParameters { get; set; }









    }


    public class SaveAttachmentParameter: CommonParamter
    {
        public string _description { get; set; }
    }


    public class SaveServiceParameter:CommonParamter
    {
        public string Solutioncategory { get; set; }

        public string value { get; set; }
        public List<Serviceslist> Serviceslist { get; set; }

       
    }

    public class SaveServiceSolutionParameters: CommonParamter
    {
        public List<SaveServiceParameter> Services;


        public int _Sector_Id { get; set; }


        public int _SubSector_Id { get; set; }

        public SubmitOBFParameters SubmitOBF { get; set; }
    }


    public class SubmitOBFParameters:CommonParamter
    {
        public string _active { get; set; }

        public int _is_submitted { get; set; }
    }


    public class ApproveRejectOBFParameter:CommonParamter
    {
        public int isapproveds { get; set; }

        public string rejectcomment { get; set; }

        public int rejectionto { get; set; }
    }


    public class CommonParamter
    {
        public int _dh_id { get; set; }

        public int _dh_header_id { get; set; }

        public string _fname { get; set; }

        public string _fpath { get; set; }

        public string _created_by { get; set; }

    }
}
