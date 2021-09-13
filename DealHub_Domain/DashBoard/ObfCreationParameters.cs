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
      
        [Required(ErrorMessage = "project name is mandatory")]

        // [RegularExpression(@"([a-zA-Z0-9@#$&;|,.?-_ \s\n]+)", ErrorMessage = "Special characters are not allowed in Project name")]
       
       [RegularExpression(@"([\-a-zA-Z0-9@#$&;|,.?_()% \s\n]+)", ErrorMessage = "Special characters are not allowed in Project name")]
        public string _dh_project_name { get; set; }

        [Required(ErrorMessage = "opportunity id is mandatory")]
        [RegularExpression(@"([a-zA-Z0-9@#$&;|,.?-_ \s\n]+)", ErrorMessage = "Special characters are not allowed in opportunity id")]
        public string _opportunity_id { get; set; }

        [Required(ErrorMessage = "project location is mandatory")]
        [RegularExpression(@"([a-zA-Z0-9@#$&;|,.?-_ \s\n]+)", ErrorMessage = "Special characters are not allowed in project location")]
        public string _dh_location { get; set; }

        [Required(ErrorMessage = "vertical  is mandatory")]
        public int _vertical_id { get; set; }

        [Required(ErrorMessage = "vertical head  is mandatory")]
        public int _verticalhead_id { get; set; }

       // [RegularExpression(@"([a-zA-Z0-9@#$&;|,.?-_ \s\n]+)", ErrorMessage = "not valid expression")] --  commented by Kirti, because it contains some special characters
        [Required(ErrorMessage = "project Description  is mandatory")]
        public string _dh_desc { get; set; }

        public int _dh_phase_id { get; set; }
        public int _parent_dh_main_id { get; set; }

        [Required(ErrorMessage = "total revenue  is mandatory")]
        public decimal _total_revenue { get; set; }

        [Required(ErrorMessage = "total cost  is mandatory")]
        public decimal _total_cost { get; set; }

        [Required(ErrorMessage = "total cost  is mandatory")]
        public decimal _total_margin { get; set; }

        [Required(ErrorMessage = "total project life  is mandatory")]
        public string _total_project_life { get; set; }

        public decimal _irr_surplus_cash { get; set; }


        public decimal _ebt { get; set; }

        [Required(ErrorMessage = "total project life  is mandatory")]
        public decimal _capex { get; set; }

        public decimal _irr_borrowed_fund { get; set; }

        [RegularExpression(@"([a-zA-Z0-9@#$&;|,.?-_ \s\n]+)", ErrorMessage = "Special characters are not allowed in LOI PO Uploaded")]
        public string _is_loi_po_uploaded { get; set; }

        //[RegularExpression(@"([a-zA-Z0-9@#$&;|,.?-_ \s\n]+)", ErrorMessage = "not valid expression")] ----  commented by Kirti, because it contains some special characters
        [Required(ErrorMessage = "assumption and risks  is mandatory")]
        public string _assumptions_and_risks { get; set; }


        [RegularExpression(@"([a-zA-Z0-9@#$&;|,.?-_ \s\n]+)", ErrorMessage = "Special characters are not allowed in Active")]
        public string _active { get; set; }

        [RegularExpression(@"([a-zA-Z0-9@#$&;|,.?-_ \s\n]+)", ErrorMessage = "Special characters are not allowed in Status")]
        public string _status { get; set; }

        public int _is_saved { get; set; }

        public int _is_submitted { get; set; }


        [RegularExpression(@"([a-zA-Z0-9@#$&;|,.?-_ \s\n]+)", ErrorMessage = "Special characters are not allowed in LOI PO Uploaded")]
        public string _service_category { get; set; }

        [Required(ErrorMessage = "payment terms  is mandatory")]
        public int _payment_terms { get; set; }

        [RegularExpression(@"([a-zA-Z0-9@#$&;|,.?-_ \s\n]+)", ErrorMessage = "Special characters are not allowed in Mode Uploaded")]
        public string _mode { get; set; }


        public List<SaveAttachmentParameter> Attachments;

        public List<SaveServiceParameter> Services;

        public List<Customer_SAP_IO_Parameter> sapio { get; set; }
        [RegularExpression(@"([a-zA-Z0-9@#$&;|,.?-_ \s\n]+)", ErrorMessage = " Special characters are not allowed in Sap Customer Code")]
        public string _sap_customer_code { get; set; }


        public int _Sector_Id { get; set; }


        public int _SubSector_Id { get; set; }

        public string save_with_solution_sector { get; set; }


        public List<SubmitOBFParameters> _SubmitOBFParameters { get; set; }

        [Required(ErrorMessage = "customer  name  is mandatory")]
        [RegularExpression(@"([a-zA-Z0-9@#$&;|,.?-_ \s\n]+)", ErrorMessage = "Special characters are not allowed in Customer Name")]
        
        public string _customer_name { get; set; }

      //  [RegularExpression(@"([a-zA-Z0-9@#$&;|,.?-_ ()\s\n]+)", ErrorMessage = "Special characters are not allowed in Comments")]
        public string _dh_comment { get; set; }


        //[RegularExpression(@"([a-zA-Z0-9@#$&;|,.?-_ \s\n]+)", ErrorMessage = "not valid expression")] --  commented by Kirti, because it contains some special characters
        [Required(ErrorMessage = "loi/po  is mandatory")]
        public string _loi_po_details { get; set; }

        [Required(ErrorMessage = "payment terms  is mandatory")]
        //  [RegularExpression(@"([a-zA-Z0-9@#$&;|,.?-_ \s\n]+)", ErrorMessage = "not valid expression")] --  commented by Kirti, because it contains some special characters
        public string _payment_term_desc { get; set; }


        public int _solution_category_id { get; set; }

        [Required(ErrorMessage = "project type  is mandatory")]
        public int _projecttype { get; set; }






    }

    public class editobfarguement
    {
        [Required(ErrorMessage = "dh id  is mandatory")]
        public int dh_id { get; set; }

        [Required(ErrorMessage = "dh header id  is mandatory")]
        public int dh_header_id { get; set; }

        [Required(ErrorMessage = "user code  is mandatory")]
        [RegularExpression(@"([a-zA-Z0-9@#$&;|,.?-_ \s\n]+)", ErrorMessage = "Special characters are not allowed in User Code")]
        public string user_code { get; set; }
    }

    public class EditObfParameters : CommonParamter
    {
      
        public string _dh_project_name { get; set; }

        public int _projecttype { get; set; }

       
        public string _opportunity_id { get; set; }

       
        public string _dh_location { get; set; }

        public int _parent_dh_main_id { get; set; }

        public int _vertical_id { get; set; }

        public int _verticalhead_id { get; set; }

        
        public string _dh_desc { get; set; }
        
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
        
        public int _payment_terms { get; set; }


        public List<SaveAttachmentParameter> Attachments;

        public List<SaveServiceParameteredit> Services;

        public List<Customer_SAP_IO_Parameteredit> sapio { get; set; }
       
        public string _sap_customer_code { get; set; }


        public int _Sector_Id { get; set; }


        public int _SubSector_Id { get; set; }

       
        public string _customer_name { get; set; }

       
        public string _dh_comment { get; set; }


       
        public string _loi_po_details { get; set; }

        
        public string _payment_term_desc { get; set; }


        public int _solution_category_id { get; set; }


        public int _dh_phase_id { get; set; } // added  for ppl initiate part

        //public int _parent_dh_main_id { get; set; } // added  for ppl initiate part
        public string _version_name { get; set; }

    }

    public class previousversion {
        public decimal _total_revenue { get; set; }
        public decimal _total_cost { get; set; }

        public decimal _total_margin { get; set; }

        public string _total_project_life { get; set; }

        public decimal _irr_surplus_cash { get; set; }


        public decimal _ebt { get; set; }

        public decimal _capex { get; set; }

        public decimal _irr_borrowed_fund { get; set; }
        public int _payment_terms { get; set; }

        public string _version_name { get; set; }
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

    public class SaveServiceParameteredit
    {
       
        public string Solutioncategory { get; set; }
       
        public string value { get; set; }


        public List<Serviceslist> Serviceslist { get; set; }






    }


    public class Customer_SAP_IO_Parameter:CommonParamter
    {
       
        public string _Cust_SAP_IO_Number { get; set; }
        
    }

    public class Customer_SAP_IO_Parameteredit 
    {
       
        public string _Cust_SAP_IO_Number { get; set; }

    }

    public class SaveServiceSolutionParameters: CommonParamter
    {
        public List<SaveServiceParameter> Services;


        public int _Sector_Id { get; set; }


        public int _SubSector_Id { get; set; }

      
        public string _sap_customer_code { get; set; }




        public List<Customer_SAP_IO_Parameter> sapio { get; set; }

      
        public string _dh_comment { get; set; }

    }


    public class SubmitOBFParameters:CommonParamter
    {
        public string _active { get; set; }

        public int _is_submitted { get; set; }
    }


    public class ApproveRejectOBFParameter:CommonParamter
    {
       
        public int isapproved { get; set; }

        [RegularExpression(@"([a-zA-Z0-9@#$&;|,.?\-_()%  \s\n]+)", ErrorMessage = " Special characters are not allowed in Comments")]
        public string rejectcomment { get; set; }

        public int rejectionto { get; set; }


        public int exceptionalcase_cfo { get; set; }

        public int exceptioncase_ceo { get; set; }

        public int is_on_hold { get; set; }


        public int _marginal_exception_requested { get; set; }
    }


    public class SaveCommentsParameter: CommonParamter
    {
      
        public string _dh_comment { get; set; }
    }

    
    public class CommonParamter
    {
        public int _dh_id { get; set; }

        public int _dh_header_id { get; set; }

        public string _fname { get; set; }

        public string _fpath { get; set; }

       
        public string _created_by { get; set; }

    }

    public class GetObfMasterParameters
    {
        [Required(ErrorMessage = "user id is mandatory")]
        public string userid { get; set; }
    }

    public class GetOBFSummaryDataVersionWiseParameters
    {
        [Required(ErrorMessage = "dh id is mandatory")]
        public int dh_id { get; set; }

        [Required(ErrorMessage = "dh header id is mandatory")]
        public int dh_header_id { get; set; }
    }



}
