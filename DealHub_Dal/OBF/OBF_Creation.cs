using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DealHub_Domain.DashBoard;
using MySql.Data.MySqlClient;
using System.Data;
using DealHub_Dal.Extensions;
using Newtonsoft.Json;

namespace DealHub_Dal.OBF
{
    public class OBF_Creation: BaseDAL
    {
        public static List<ObfCreationDetailsParameters> ObfCreation(ObfCreationParameters filter)
        {
            List<ObfCreationDetailsParameters> _ObfCreationData = new List<ObfCreationDetailsParameters>();
            try
            {
                //sp_auth_user
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_manage_dh_header", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@_dh_id", MySqlDbType.UInt32).Value = filter._dh_id;
                    cmd.Parameters.Add("@_dh_project_name", MySqlDbType.String).Value = filter._dh_project_name;
                    cmd.Parameters.Add("@_opportunity_id", MySqlDbType.String).Value = filter._opportunity_id;
                    cmd.Parameters.Add("@_dh_location", MySqlDbType.String).Value = filter._dh_location;
                    cmd.Parameters.Add("@_vertical_id", MySqlDbType.UInt32).Value = filter._vertical_id;
                    cmd.Parameters.Add("@_verticalhead_id", MySqlDbType.UInt32).Value = filter._verticalhead_id;
                    cmd.Parameters.Add("@_dh_desc", MySqlDbType.String).Value = filter._dh_desc;
                    cmd.Parameters.Add("@_dh_phase_id", MySqlDbType.UInt32).Value = filter._dh_phase_id;
                    cmd.Parameters.Add("@_parent_dh_main_id", MySqlDbType.UInt32).Value = filter._parent_dh_main_id;
                    cmd.Parameters.Add("@_dh_header_id", MySqlDbType.UInt32).Value = filter._dh_header_id;
                    cmd.Parameters.Add("@_total_revenue", MySqlDbType.Decimal).Value = filter._total_revenue;
                    cmd.Parameters.Add("@_total_cost", MySqlDbType.Decimal).Value = filter._total_cost;
                    cmd.Parameters.Add("@_total_margin", MySqlDbType.Decimal).Value = filter._total_margin;
                    cmd.Parameters.Add("@_total_project_life", MySqlDbType.String).Value = filter._total_project_life;
                    cmd.Parameters.Add("@_irr_surplus_cash", MySqlDbType.Decimal).Value = filter._irr_surplus_cash;
                    cmd.Parameters.Add("@_ebt", MySqlDbType.Decimal).Value = filter._ebt;
                    cmd.Parameters.Add("@_capex", MySqlDbType.Decimal).Value = filter._capex;
                    cmd.Parameters.Add("@_irr_borrowed_fund", MySqlDbType.Decimal).Value = filter._irr_borrowed_fund;
                    cmd.Parameters.Add("@_is_loi_po_uploaded", MySqlDbType.String).Value = filter._is_loi_po_uploaded;
                    cmd.Parameters.Add("@_assumptions_and_risks", MySqlDbType.String).Value = filter._assumptions_and_risks;
                    cmd.Parameters.Add("@_fname", MySqlDbType.String).Value = filter._fname;
                    cmd.Parameters.Add("@_fpath", MySqlDbType.String).Value = filter._fpath;
                    cmd.Parameters.Add("@_active", MySqlDbType.String).Value = filter._active;
                    cmd.Parameters.Add("@_status", MySqlDbType.String).Value = filter._status;
                    cmd.Parameters.Add("@_is_saved", MySqlDbType.UInt32).Value = filter._is_saved;
                    cmd.Parameters.Add("@_is_submitted", MySqlDbType.UInt32).Value = filter._is_submitted;
                    cmd.Parameters.Add("@_created_by", MySqlDbType.String).Value = filter._created_by;
                    cmd.Parameters.Add("@_service_category", MySqlDbType.String).Value = filter._service_category;
                    cmd.Parameters.Add("@_payment_terms", MySqlDbType.UInt32).Value = filter._payment_terms;
                    cmd.Parameters.Add("@_mode", MySqlDbType.String).Value = filter._mode;
                    cmd.Parameters.Add("@_customer_name", MySqlDbType.String).Value = filter._customer_name;

                    cmd.Parameters.Add("@_loi_po_details", MySqlDbType.String).Value = filter._loi_po_details;
                    cmd.Parameters.Add("@_payment_term_desc", MySqlDbType.String).Value = filter._payment_term_desc;
                    cmd.Parameters.Add("@_solution_category_id", MySqlDbType.UInt32).Value = filter._solution_category_id;
                    cmd.Parameters.Add("@_domain_id", MySqlDbType.UInt32).Value = filter._projecttype;

                    conn.Open();
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ObfCreationDetailsParameters _ObfCreationDetailsParameters = new ObfCreationDetailsParameters();

                            //_DashBoardDetailsParameters.obf_id = dr.IsNull<uint>("obf_id");
                            _ObfCreationDetailsParameters.Result = dr.IsNull<string>("result");
                            if (_ObfCreationDetailsParameters.Result != "success")
                            {
                                _ObfCreationData.Add(_ObfCreationDetailsParameters);
                                return _ObfCreationData;
                            }
                            _ObfCreationDetailsParameters.dh_id = dr.IsNull<uint>("dh_id");
                            _ObfCreationDetailsParameters.dh_header_id = dr.IsNull<uint>("dh_header_id");

                          


                            filter._dh_header_id = Convert.ToInt32( _ObfCreationDetailsParameters.dh_header_id);
                            

                            foreach (SaveAttachmentParameter attachement in filter.Attachments)
                            {
                                attachement._dh_id= Convert.ToInt32( dr.IsNull<uint>("dh_id"));
                                attachement._dh_header_id = Convert.ToInt32(dr.IsNull<uint>("dh_header_id"));
                                attachement._created_by = filter._created_by;
                               
                            }


                            foreach (SaveServiceParameter service in filter.Services)
                            {
                              
                                service._dh_header_id = Convert.ToInt32(dr.IsNull<uint>("dh_header_id"));
                                service._created_by = filter._created_by;
                                
                            }

                            foreach (SaveServiceParameter service in filter.Services)
                            {

                                service._dh_header_id = Convert.ToInt32(dr.IsNull<uint>("dh_header_id"));
                                service._created_by = filter._created_by;
                                
                            }


                            foreach (SaveServiceParameter service in filter.Services)
                            {

                                service._dh_header_id = Convert.ToInt32(dr.IsNull<uint>("dh_header_id"));
                                service._created_by = filter._created_by;
                                
                            }

                            foreach (SubmitOBFParameters Submitobf in filter._SubmitOBFParameters)
                            {
                                Submitobf._dh_id = Convert.ToInt32(dr.IsNull<uint>("dh_id"));
                                Submitobf._dh_header_id = Convert.ToInt32(dr.IsNull<uint>("dh_header_id"));
                                Submitobf._created_by = filter._created_by;
                                Submitobf._is_submitted = filter._is_submitted;
                                Submitobf._active = filter._active;
                            }

                            foreach (Customer_SAP_IO_Parameter SAPIO in filter.sapio)
                            {
                                SAPIO._dh_id = Convert.ToInt32(dr.IsNull<uint>("dh_id"));
                                SAPIO._dh_header_id = Convert.ToInt32(dr.IsNull<uint>("dh_header_id"));
                                SAPIO._created_by = filter._created_by;
                                
                            }

                           

                            _ObfCreationData.Add(_ObfCreationDetailsParameters);

                        }
                    }

                    // call of save attachments
                    SaveAttachments(filter.Attachments);

                    if (filter.save_with_solution_sector=="Y")
                    {
                        SaveServiceSolutionParameters SSP = new SaveServiceSolutionParameters();
                        SSP._dh_header_id = filter._dh_header_id;
                        SSP._Sector_Id = filter._Sector_Id;
                        SSP._SubSector_Id = filter._SubSector_Id;
                        SSP._created_by = filter._created_by;
                        SaveServices(filter.Services);
                        SaveSectorSubSector(SSP);
                        SaveCustomer_SAP_IO_Number(filter.sapio, filter._sap_customer_code);
                        if (filter._dh_comment !="")
                        {
                            SaveCommentsParameter _SaveCommentsParameter = new SaveCommentsParameter();
                            _SaveCommentsParameter._dh_header_id = filter._dh_header_id;
                            _SaveCommentsParameter._dh_comment = filter._dh_comment;
                            _SaveCommentsParameter._created_by = filter._created_by;
                            SaveComments(_SaveCommentsParameter);
                        }
                        

                    }

                    if (filter._is_submitted==1)
                    {
                        
                        submit_dh_headers(filter._SubmitOBFParameters[0]);
                    }


                }
                return _ObfCreationData;
            }
            catch (Exception e)
            {
                ObfCreationDetailsParameters _ObfCreationDetailsParameters = new ObfCreationDetailsParameters();

                //_DashBoardDetailsParameters.obf_id = dr.IsNull<uint>("obf_id");
                _ObfCreationDetailsParameters.Result = "Failure";



                _ObfCreationData.Add(_ObfCreationDetailsParameters);
                return _ObfCreationData;
            }
        }

        public static List<ObfCreationDetailsParameters> editcustomercodeandio(ObfCreationParameters filter)
        {
            List<ObfCreationDetailsParameters> _editcustomercodeandio = new List<ObfCreationDetailsParameters>();
            try
            {
                //sp_auth_user
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_edit_sap_customer_code", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@_dh_header_id", MySqlDbType.UInt32).Value = filter._dh_header_id;
                    cmd.Parameters.Add("@_sap_customer_code", MySqlDbType.String).Value = filter._sap_customer_code;
                    cmd.Parameters.Add("@_user_id", MySqlDbType.String).Value = filter._created_by;

                    conn.Open();
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ObfCreationDetailsParameters _ObfCreationDetailsParameters = new ObfCreationDetailsParameters();

                            //_DashBoardDetailsParameters.obf_id = dr.IsNull<uint>("obf_id");
                            _ObfCreationDetailsParameters.Result = dr.IsNull<string>("status");
                            _ObfCreationDetailsParameters.dh_id = Convert.ToUInt32(filter._dh_id);
                            _ObfCreationDetailsParameters.dh_header_id = Convert.ToUInt32(filter._dh_header_id);

                            filter._dh_header_id = Convert.ToInt32(_ObfCreationDetailsParameters.dh_header_id);
                            
                            foreach (Customer_SAP_IO_Parameter SAPIO in filter.sapio)
                            {
                                SAPIO._dh_id = Convert.ToInt32(filter._dh_id);
                                SAPIO._dh_header_id = Convert.ToInt32(filter._dh_header_id);
                                SAPIO._created_by = filter._created_by;

                            }
                            _editcustomercodeandio.Add(_ObfCreationDetailsParameters);

                        }
                    }

                    
                        SaveCustomer_SAP_IO_Number(filter.sapio, filter._sap_customer_code);
                }
                return _editcustomercodeandio;
            }
            catch (Exception e)
            {
                ObfCreationDetailsParameters _ObfCreationDetailsParameters = new ObfCreationDetailsParameters();

                //_DashBoardDetailsParameters.obf_id = dr.IsNull<uint>("obf_id");
                _ObfCreationDetailsParameters.Result = "Failure";



                _editcustomercodeandio.Add(_ObfCreationDetailsParameters);
                return _editcustomercodeandio;
            }
        }


        public static List<SaveAttachementDetailsParameters> SaveServiceSolutionSector(SaveServiceSolutionParameters filter)
        {
            List<SaveAttachementDetailsParameters> _SaveAttachementDetailsParameters = new List<SaveAttachementDetailsParameters>();
            try
            {
                foreach (SaveServiceParameter f in filter.Services)
                {
                    f._dh_id = filter._dh_id;
                    f._dh_header_id = filter._dh_header_id;
                    f._created_by = filter._created_by;
                    f._fname = filter._fname;
                    f._fpath = filter._fpath;
                }

                foreach(Customer_SAP_IO_Parameter CSIP in filter.sapio)
                {
                    CSIP._dh_id = filter._dh_id;
                    CSIP._dh_header_id = filter._dh_header_id;
                    CSIP._created_by = filter._created_by;
                }

               

               _SaveAttachementDetailsParameters= SaveServices(filter.Services);
                _SaveAttachementDetailsParameters = SaveSectorSubSector(filter);
                if (filter.sapio.Count !=0)
                {
                    _SaveAttachementDetailsParameters = SaveCustomer_SAP_IO_Number(filter.sapio, filter._sap_customer_code);
                }


                if (filter._dh_comment != "")
                {
                    SaveCommentsParameter _SaveCommentsParameter = new SaveCommentsParameter();
                    _SaveCommentsParameter._dh_header_id = filter._dh_header_id;
                    _SaveCommentsParameter._dh_comment = filter._dh_comment;
                    _SaveCommentsParameter._created_by = filter._created_by;
                    _SaveAttachementDetailsParameters = SaveComments(_SaveCommentsParameter);
                }

                return _SaveAttachementDetailsParameters;
            }
            catch(Exception ex)
            {
                _SaveAttachementDetailsParameters = new List<SaveAttachementDetailsParameters>();

                SaveAttachementDetailsParameters _Details = new SaveAttachementDetailsParameters();
                _Details.status = "Failed";
                _Details.message = "Error in saving parameters";
                _SaveAttachementDetailsParameters.Add(_Details);

                return _SaveAttachementDetailsParameters;
            }

        }

       


        public static List<SaveAttachementDetailsParameters> submit_dh_headers(SubmitOBFParameters filter)
        {
            List<SaveAttachementDetailsParameters> _SaveAttachementDetailsParameters = new List<SaveAttachementDetailsParameters>();
            try
            {

                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        MySqlCommand cmd = new MySqlCommand("sp_submit_dh_header", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@_dh_header_id", MySqlDbType.UInt32).Value = filter._dh_header_id;
                        cmd.Parameters.Add("@_dh_id", MySqlDbType.UInt32).Value = filter._dh_id;
                       
                        cmd.Parameters.Add("@_user_id", MySqlDbType.String).Value = filter._created_by;
                        cmd.Parameters.Add("@_active", MySqlDbType.String).Value = filter._active;
                        cmd.Parameters.Add("@_is_submitted", MySqlDbType.String).Value = filter._is_submitted;

                        conn.Open();
                        using (IDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                SaveAttachementDetailsParameters _Details = new SaveAttachementDetailsParameters();
                                _Details.status = dr.IsNull<string>("status");
                                _Details.message = dr.IsNull<string>("message");
                                _Details.dh_header_id = Convert.ToUInt32( filter._dh_header_id);
                                _Details.dh_id = Convert.ToUInt32(filter._dh_id);

                                _SaveAttachementDetailsParameters.Add(_Details);
                            }
                        }
                    }

                    try
                    {
                        EmailSender_DAL.Email_Sending_Details(filter._dh_header_id, 0);
                    }
                    catch (Exception ex)
                    {

                    }

                    return _SaveAttachementDetailsParameters;
                }
                catch (Exception ex)
                {
                    _SaveAttachementDetailsParameters = new List<SaveAttachementDetailsParameters>();

                    SaveAttachementDetailsParameters _Details = new SaveAttachementDetailsParameters();
                    _Details.status = "Failed";
                    _Details.message = "Error in saving parameters";
                    _SaveAttachementDetailsParameters.Add(_Details);

                    return _SaveAttachementDetailsParameters;
                }

                return _SaveAttachementDetailsParameters;
            }
            catch (Exception ex)
            {
                _SaveAttachementDetailsParameters = new List<SaveAttachementDetailsParameters>();

                SaveAttachementDetailsParameters _Details = new SaveAttachementDetailsParameters();
                _Details.status = "Failed";
                _Details.message = "Error in saving parameters";
                _SaveAttachementDetailsParameters.Add(_Details);

                return _SaveAttachementDetailsParameters;
            }

        }


        public static List<SaveAttachementDetailsParameters> SaveSectorSubSector(SaveServiceSolutionParameters filter)
        {
            List<SaveAttachementDetailsParameters> _SaveAttachementDetailsParameters = new List<SaveAttachementDetailsParameters>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_save_dh_sector_subsector", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@_dh_header_id", MySqlDbType.UInt32).Value = filter._dh_header_id;
                    cmd.Parameters.Add("@_Sector_Id", MySqlDbType.UInt32).Value = filter._Sector_Id;
                    cmd.Parameters.Add("@_SubSector_Id", MySqlDbType.UInt32).Value = filter._SubSector_Id;
                    cmd.Parameters.Add("@_user_id", MySqlDbType.String).Value = filter._created_by;
                    conn.Open();
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            SaveAttachementDetailsParameters _Details = new SaveAttachementDetailsParameters();
                            _Details.status = dr.IsNull<string>("status");
                            _Details.message = dr.IsNull<string>("message");
                            _Details.dh_header_id = Convert.ToUInt32(filter._dh_header_id);
                            _Details.dh_id = Convert.ToUInt32(filter._dh_id);
                            _SaveAttachementDetailsParameters.Add(_Details);
                        }
                    }
                }

                return _SaveAttachementDetailsParameters;
            }
            catch(Exception ex)
            {
                _SaveAttachementDetailsParameters = new List<SaveAttachementDetailsParameters>();

                SaveAttachementDetailsParameters _Details = new SaveAttachementDetailsParameters();
                _Details.status = "Failed";
                _Details.message = "Error in saving parameters";
                _SaveAttachementDetailsParameters.Add(_Details);

                return _SaveAttachementDetailsParameters;
            }
        }


        public static List<SaveAttachementDetailsParameters> SaveServices(List<SaveServiceParameter> filters)
        {
            List<SaveAttachementDetailsParameters> _SaveAttachementDetailsParameters = new List<SaveAttachementDetailsParameters>();
            try
            {
                foreach (SaveServiceParameter filter in filters)
                {

                    foreach (Serviceslist SL in filter.Serviceslist)
                    {
                        using (MySqlConnection conn = new MySqlConnection(connectionString))
                        {
                            MySqlCommand cmd = new MySqlCommand("sp_save_dh_services", conn);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@_dh_header_id", MySqlDbType.UInt32).Value = filter._dh_header_id;
                            cmd.Parameters.Add("@solution_id", MySqlDbType.UInt32).Value = SL.value;
                            cmd.Parameters.Add("@solutioncategory_id", MySqlDbType.UInt32).Value = filter.value;
                            cmd.Parameters.Add("@solution_Name", MySqlDbType.String).Value = SL.viewValue;
                            cmd.Parameters.Add("@_user_id", MySqlDbType.String).Value = filter._created_by;
                            conn.Open();
                            using (IDataReader dr = cmd.ExecuteReader())
                            {
                                while (dr.Read())
                                {
                                    SaveAttachementDetailsParameters _Details = new SaveAttachementDetailsParameters();
                                    _Details.status = dr.IsNull<string>("status");
                                    _Details.message = dr.IsNull<string>("message");
                                    _Details.dh_header_id = Convert.ToUInt32(filter._dh_header_id);
                                    _Details.dh_id = Convert.ToUInt32(filter._dh_id);
                                    _SaveAttachementDetailsParameters.Add(_Details);
                                }
                            }
                        }
                    }
                 
                }

                    return _SaveAttachementDetailsParameters;
            }
            catch(Exception ex)
            {
                _SaveAttachementDetailsParameters = new List<SaveAttachementDetailsParameters>();

                SaveAttachementDetailsParameters _Details = new SaveAttachementDetailsParameters();
                _Details.status = "Failed";
                _Details.message = "Error in saving parameters";
                _SaveAttachementDetailsParameters.Add(_Details);

                return _SaveAttachementDetailsParameters;
            }
        }

        public static List<SaveAttachementDetailsParameters> SaveAttachments(List<SaveAttachmentParameter> filters )
        {
            List<SaveAttachementDetailsParameters> _SaveAttachementDetailsParameters = new List<SaveAttachementDetailsParameters>();
            try
            {
                    
                   foreach (SaveAttachmentParameter filter in filters)
                    {
                        using (MySqlConnection conn = new MySqlConnection(connectionString))
                        {
                            MySqlCommand cmd = new MySqlCommand("sp_save_dh_attachments", conn);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@_dh_id", MySqlDbType.UInt32).Value = filter._dh_id;
                            cmd.Parameters.Add("@_dh_header_id", MySqlDbType.UInt32).Value = filter._dh_header_id;
                            cmd.Parameters.Add("@_fname", MySqlDbType.String).Value = filter._fname;
                            cmd.Parameters.Add("@_fpath", MySqlDbType.String).Value = filter._fpath;
                            cmd.Parameters.Add("@_description", MySqlDbType.String).Value = filter._description;
                            cmd.Parameters.Add("@_user_id", MySqlDbType.String).Value = filter._created_by;
                            conn.Open();
                            using (IDataReader dr = cmd.ExecuteReader())
                            {
                                while (dr.Read())
                                {
                                    SaveAttachementDetailsParameters _Details = new SaveAttachementDetailsParameters();
                                    _Details.status= dr.IsNull<string>("status");
                                    _Details.message = dr.IsNull<string>("message");
                                   _SaveAttachementDetailsParameters.Add(_Details);
                                 }
                            }

                        }
                    }  

                    return _SaveAttachementDetailsParameters;
            }
            catch(Exception ex)
            {
                _SaveAttachementDetailsParameters = new List<SaveAttachementDetailsParameters>();

                SaveAttachementDetailsParameters _Details = new SaveAttachementDetailsParameters();
                _Details.status = "Failed";
                _Details.message = "Error in saving parameters";
                _SaveAttachementDetailsParameters.Add(_Details);

                return _SaveAttachementDetailsParameters;
            }
        }

        public static string GetMastersOBFCreation(string userid)
        {
            try
            {

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    MySqlDataAdapter DA = new MySqlDataAdapter();
                    MySqlCommand cmd = new MySqlCommand("sp_get_master_list", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("_user_id", MySqlDbType.String).Value = userid;
                    DA.SelectCommand = cmd;
                    cmd.Connection = new MySqlConnection(connectionString);
                    DataSet ds = new DataSet();
                    DA.Fill(ds);

                    DataSet rds= ds.GetTableName();

                    return  JsonConvert.SerializeObject(rds, Formatting.Indented); ;

                }

            }
            catch(Exception ex)
            {
                return "error";
            }

        }

        public static EditObfParameters getEditObf(editobfarguement filter)
        {
            EditObfParameters editobf = new EditObfParameters();
            editobf.Services = new List<SaveServiceParameteredit>();
            editobf.Attachments = new List<SaveAttachmentParameter>();
            editobf.sapio = new List<Customer_SAP_IO_Parameteredit>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    MySqlDataAdapter DA = new MySqlDataAdapter();
                    MySqlCommand cmd = new MySqlCommand("sp_getEditObfData", conn);
                    cmd.Parameters.Add("dh_id", MySqlDbType.UInt32).Value = filter.dh_id;
                    cmd.Parameters.Add("dh_header_id", MySqlDbType.UInt32).Value = filter.dh_header_id;
                    cmd.Parameters.Add("usercode", MySqlDbType.VarChar).Value = filter.user_code;
                    cmd.CommandType = CommandType.StoredProcedure;

                    DA.SelectCommand = cmd;
                    cmd.Connection = new MySqlConnection(connectionString);
                    DataSet ds = new DataSet();
                    DA.Fill(ds);

                    DataSet rds = ds.GetTableName();
                    
                    
                    if (rds.Tables["Sap_IO_number"] != null)
                    { 
                    DataTable Dt_Sap_IO_number = rds.Tables["Sap_IO_number"].Copy();
                        foreach (DataRow dr in Dt_Sap_IO_number.Rows)
                        {
                            Customer_SAP_IO_Parameteredit sap_io = new Customer_SAP_IO_Parameteredit();
                            sap_io._Cust_SAP_IO_Number = dr["cust_sap_io_number"].ToString();
                            editobf.sapio.Add(sap_io);
                        }
                    }

                    if (rds.Tables["uploaddata"] != null)
                    {
                        DataTable Dt_UploadDetails = rds.Tables["uploaddata"].Copy();
                        foreach (DataRow Row in Dt_UploadDetails.Rows)
                        {
                            editobf._dh_id = Convert.ToInt32(Row["dh_id"]);
                            editobf._dh_header_id = Convert.ToInt32(Row["dh_header_id"]);
                            editobf._fname = Row["filename"].ToString();
                            editobf._fpath = Row["filepath"].ToString();
                            editobf._created_by = Row["created_by"].ToString();
                            editobf._created_by = Row["created_by"].ToString();
                            editobf._dh_project_name = Row["dh_project_name"].ToString();
                            //editobf._projecttype = getprojecttypebyID(Convert.ToInt32(Row["domain_id"].ToString().Trim() == ""?"0": Row["domain_id"].ToString()));
                            editobf._projecttype = Convert.ToInt32(Row["domain_id"].ToString().Trim() == "" ? "0" : Row["domain_id"].ToString());
                            editobf._opportunity_id = Row["opportunity_id"].ToString();
                            editobf._dh_location = Row["dh_location"].ToString();
                            editobf._parent_dh_main_id = Convert.ToInt32(Row["parent_dh_main_id"].ToString() == ""?"0": Row["parent_dh_main_id"].ToString());
                            editobf._vertical_id = Convert.ToInt32(Row["vertical_id"].ToString());
                            editobf._verticalhead_id = Convert.ToInt32(Row["verticalhead_id"].ToString());
                            editobf._dh_desc = Row["dh_desc"].ToString();
                            editobf._total_revenue = Convert.ToDecimal(Row["total_revenue"].ToString());
                            editobf._total_cost = Convert.ToDecimal(Row["total_cost"].ToString());
                            editobf._total_margin = Convert.ToDecimal(Row["total_margin"].ToString());
                            editobf._total_project_life = Row["total_project_life"].ToString();
                            editobf._irr_surplus_cash = Convert.ToDecimal(Row["irr_surplus_cash"].ToString());
                            editobf._ebt = Convert.ToDecimal(Row["ebt"].ToString());
                            editobf._capex = Convert.ToDecimal(Row["capex"].ToString());
                            editobf._irr_borrowed_fund = Convert.ToDecimal(Row["irr_borrowed_fund"].ToString());
                            editobf._is_loi_po_uploaded = Row["is_loi_po_uploaded"].ToString();
                            editobf._assumptions_and_risks = Row["assumptions_and_risks"].ToString();
                            editobf._payment_terms = Convert.ToInt32(Row["payment_terms"].ToString());
                            editobf._sap_customer_code = Row["sap_customer_code"].ToString();
                            editobf._Sector_Id = Convert.ToInt32(Row["Sector_Id"].ToString());
                            editobf._SubSector_Id = Convert.ToInt32(Row["SubSector_Id"].ToString());
                            editobf._customer_name = Row["customer_name"].ToString();
                            editobf._dh_comment = Row["dh_comment"].ToString();
                            editobf._loi_po_details = Row["loi_po_details"].ToString();
                            editobf._payment_term_desc = Row["payment_term_desc"].ToString();
                            editobf._solution_category_id = Convert.ToInt32(Row["solution_category_id"].ToString());
                        }
                    }


                    if (rds.Tables["SolutionServices"] != null)
                    {
                        DataTable Dt_SolutionServices = rds.Tables["SolutionServices"].Copy();
                        DataView dv_distinctsolcategory = new DataView(Dt_SolutionServices);

                        DataTable dt_distinctsolcategory = dv_distinctsolcategory.ToTable(true, "solutioncategory_id", "solutioncategory_name");

                        foreach (DataRow row in dt_distinctsolcategory.Rows)
                    {
                        SaveServiceParameteredit sc = new SaveServiceParameteredit();
                        sc.Serviceslist = new List<Serviceslist>();
                        sc.value = row["solutioncategory_id"].ToString();
                        sc.Solutioncategory = row["solutioncategory_name"].ToString();

                        DataRow[] Row_Solutions_In_Category = Dt_SolutionServices.Select("Solutioncategory_Id=" + sc.value);

                        foreach(DataRow dr in Row_Solutions_In_Category)
                        {
                            Serviceslist servlist = new Serviceslist();
                            servlist.value = dr["solution_id"].ToString();
                            servlist.viewValue = dr["solution_name"].ToString();
                            sc.Serviceslist.Add(servlist);
                        }
                        editobf.Services.Add(sc);
                    }
                    }

                    if (rds.Tables["Attachments"] != null)
                    {
                        DataTable Dt_Attachments = rds.Tables["Attachments"].Copy();

                        foreach (DataRow dr in Dt_Attachments.Rows)
                    {
                        SaveAttachmentParameter attachments = new SaveAttachmentParameter();
                        attachments._dh_id = editobf._dh_id;
                        attachments._dh_header_id = editobf._dh_header_id;
                        attachments._created_by = editobf._created_by;
                        attachments._fname = dr["filename"].ToString();
                        attachments._fpath = dr["filepath"].ToString();
                        attachments._description = dr["description"].ToString();
                        editobf.Attachments.Add(attachments);
                    }
                    }







                    return editobf;

                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string getprojecttypebyID(int domain_id)
        {
            string result = "";
            try
            {
                DataSet ds = new DataSet();
                
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("select domain_name from mst_domains where domain_id=@domain_id", conn);
                    cmd.Parameters.AddWithValue("@domain_id", domain_id);
                   
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(ds);
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            result = ds.Tables[0].Rows[0]["domain_name"].ToString();
                        }
                        else
                        {
                            result = "N/A";
                        }
                    }
                    else
                    {
                        result = "N/A";
                    }
                  }
                return result;
            }
            catch (Exception ex)
            {
                result = "N/A";
                return result;
            }
        }

        public static List<SolutionCategory> get_master_solutions(string userid)
        {
            List<SolutionCategory> _SolutionCategory = new List<SolutionCategory>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    MySqlDataAdapter DA = new MySqlDataAdapter();
                    MySqlCommand cmd = new MySqlCommand("sp_get_master_solutions", conn);
                    cmd.Parameters.Add("_user_id", MySqlDbType.String).Value = userid;
                    cmd.CommandType = CommandType.StoredProcedure;
                   
                    DA.SelectCommand = cmd;
                    cmd.Connection = new MySqlConnection(connectionString);
                    DataSet ds = new DataSet();
                    DA.Fill(ds);

                    DataSet rds = ds.GetTableName();

                    DataTable Dt_SolutionCategory = rds.Tables["solutioncategory"].Copy();

                    DataTable Dt_solutions = rds.Tables["solutions"].Copy();

                    
                    foreach( DataRow Row in Dt_SolutionCategory.Rows)
                    {
                        SolutionCategory sc = new SolutionCategory();
                        UInt64 Solutioncategory_Id = Convert.ToUInt64(Row["value"]);
                        sc.value = Row["value"].ToString();
                        string SolutionCategory= Row["viewValue"].ToString();
                        sc.viewValue = SolutionCategory;
                        sc.Solutioncategory = SolutionCategory;
                        sc.Solutionservices = new List<SolutionServices>();

                        //DataRow[] Row_Solutionservices = Dt_solutions.Select("Solutioncategory_Id<=" + Solutioncategory_Id);


                        for (UInt64 Add_noof_Solutionservices=1;Add_noof_Solutionservices <=Solutioncategory_Id;Add_noof_Solutionservices++)
                        {
                            SolutionServices ServiceObj = new SolutionServices();
                          
                            ServiceObj.Serviceslist = new List<Serviceslist>();


                            DataRow[] Row_Solutions_In_Category = Dt_solutions.Select("Solutioncategory_Id=" + Add_noof_Solutionservices.ToString());

                            ServiceObj.Solutioncategory = Row_Solutions_In_Category[0]["solutioncategory_name"].ToString();
                            ServiceObj.value = Row_Solutions_In_Category[0]["Solutioncategory_Id"].ToString();

                            foreach (DataRow Rw in Row_Solutions_In_Category)
                            {
                                Serviceslist SL = new Serviceslist();

                                SL.value = Rw["value"].ToString();
                                SL.viewValue = Rw["viewValue"].ToString();

                                ServiceObj.Serviceslist.Add(SL);
                            }


                            sc.Solutionservices.Add(ServiceObj);
                        }

                        







                        _SolutionCategory.Add(sc);
                    }

                   


                    return _SolutionCategory;

                }
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public static List<commanmessges> ApproveRejectObf(ApproveRejectOBFParameter filters)
        {
            List<commanmessges> _commanmessges = new List<commanmessges>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_dh_approve_reject", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@dhheaderid", MySqlDbType.UInt32).Value = filters._dh_header_id;
                    cmd.Parameters.Add("@_user_id", MySqlDbType.String).Value = filters._created_by;
                    cmd.Parameters.Add("@isapproved", MySqlDbType.UInt32).Value = filters.isapproved;
                    cmd.Parameters.Add("@rejectcomment", MySqlDbType.String).Value = filters.rejectcomment;
                    cmd.Parameters.Add("@rejectionto", MySqlDbType.UInt32).Value = filters.rejectionto;
                    cmd.Parameters.Add("@exceptionalcase_cfo", MySqlDbType.UInt32).Value = filters.exceptionalcase_cfo;
                    cmd.Parameters.Add("@exceptioncase_ceo", MySqlDbType.UInt32).Value = filters.exceptioncase_ceo;
                    cmd.Parameters.Add("@is_on_hold", MySqlDbType.UInt32).Value = filters.is_on_hold;
                    cmd.Parameters.Add("@_marginal_exception_requested", MySqlDbType.UInt32).Value = filters._marginal_exception_requested;
                    conn.Open();
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            commanmessges _Details = new commanmessges();
                            _Details.status = dr.IsNull<string>("status");
                            _Details.message = dr.IsNull<string>("message");
                           
                            _commanmessges.Add(_Details);
                        }
                    }
                }

                try
                {
                    EmailSender_DAL.Email_Sending_Details(filters._dh_header_id, 0);
                }
                catch(Exception ex)
                {

                }

                return _commanmessges;
            }
            catch (Exception ex)
            {
                _commanmessges = new List<commanmessges>();

                commanmessges _Details = new commanmessges();
                _Details.status = "Failed";
                _Details.message = "Error in saving parameters";
                _commanmessges.Add(_Details);

                return _commanmessges;
            }

        }

        public static List<SaveAttachementDetailsParameters> SaveCustomer_SAP_IO_Number(List<Customer_SAP_IO_Parameter> filters,string _sap_customer_code)
        {
            List<SaveAttachementDetailsParameters> _SaveAttachementDetailsParameters = new List<SaveAttachementDetailsParameters>();
            try
            {
                foreach (Customer_SAP_IO_Parameter filter in filters)
                {

                   
                        using (MySqlConnection conn = new MySqlConnection(connectionString))
                        {
                            MySqlCommand cmd = new MySqlCommand("sp_save_dh_cust_sap_io_number", conn);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@_dh_header_id", MySqlDbType.UInt32).Value = filter._dh_header_id;
                            cmd.Parameters.Add("@_Cust_SAP_IO_Number", MySqlDbType.String).Value = filter._Cust_SAP_IO_Number;
                            cmd.Parameters.Add("@_sap_customer_code", MySqlDbType.String).Value = _sap_customer_code;
                            cmd.Parameters.Add("@_user_id", MySqlDbType.String).Value = filter._created_by;
                            conn.Open();
                            using (IDataReader dr = cmd.ExecuteReader())
                            {
                                while (dr.Read())
                                {
                                    SaveAttachementDetailsParameters _Details = new SaveAttachementDetailsParameters();
                                    _Details.status = dr.IsNull<string>("status");
                                    _Details.message = dr.IsNull<string>("message");
                                    _Details.dh_header_id = Convert.ToUInt32(filter._dh_header_id);
                                    _Details.dh_id = Convert.ToUInt32(filter._dh_id);
                                    _SaveAttachementDetailsParameters.Add(_Details);
                                }
                            }
                        }
                    

                }

                return _SaveAttachementDetailsParameters;
            }
            catch (Exception ex)
            {
                _SaveAttachementDetailsParameters = new List<SaveAttachementDetailsParameters>();

                SaveAttachementDetailsParameters _Details = new SaveAttachementDetailsParameters();
                _Details.status = "Failed";
                _Details.message = "Error in saving parameters";
                _SaveAttachementDetailsParameters.Add(_Details);

                return _SaveAttachementDetailsParameters;
            }
        }

        public static List<SaveAttachementDetailsParameters> SaveComments(SaveCommentsParameter filter)
        {
            List<SaveAttachementDetailsParameters> _SaveAttachementDetailsParameters = new List<SaveAttachementDetailsParameters>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_add_dh_comments", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@_dh_header_id", MySqlDbType.UInt32).Value = filter._dh_header_id;
                    cmd.Parameters.Add("@_dh_comment", MySqlDbType.String).Value = filter._dh_comment;
                   
                    cmd.Parameters.Add("@_user_id", MySqlDbType.String).Value = filter._created_by;
                    conn.Open();
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            SaveAttachementDetailsParameters _Details = new SaveAttachementDetailsParameters();
                            _Details.status = dr.IsNull<string>("status");
                            _Details.message = dr.IsNull<string>("message");
                            _Details.dh_header_id = Convert.ToUInt32(filter._dh_header_id);
                            _Details.dh_id = Convert.ToUInt32(filter._dh_id);
                            _SaveAttachementDetailsParameters.Add(_Details);
                        }
                    }
                }
                return _SaveAttachementDetailsParameters;
            }
            catch(Exception ex)
            {
                _SaveAttachementDetailsParameters = new List<SaveAttachementDetailsParameters>();

                SaveAttachementDetailsParameters _Details = new SaveAttachementDetailsParameters();
                _Details.status = "Failed";
                _Details.message = "Error in saving comments";
                _SaveAttachementDetailsParameters.Add(_Details);

                return _SaveAttachementDetailsParameters;
            }

         
        }


       

        public static List<SaveAttachementDetailsParameters> SaveAttachments_OBFSummary(List<SaveAttachmentParameter> filters)
        {
            List<SaveAttachementDetailsParameters> _SaveAttachementDetailsParameters = new List<SaveAttachementDetailsParameters>();
            try
            {
                int i = 0;
              
                    foreach (SaveAttachmentParameter filter in filters)
                    {

                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        if (i == 0)
                        {
                            MySqlCommand cmd1 = new MySqlCommand("sp_delete_attachment_descriptionwise", conn);
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.Parameters.Add("_dh_header_id", MySqlDbType.String).Value = filter._dh_header_id;
                            cmd1.Parameters.Add("_description", MySqlDbType.String).Value = filter._description;
                            conn.Open();
                            // cmd1.Connection = new MySqlConnection(conn);
                            int result = cmd1.ExecuteNonQuery();
                            i++;

                            conn.Close();
                        }
                        if (filter._fname != "Remove all Details" && filter._fpath != "Remove all Details")
                        {
                            MySqlCommand cmd = new MySqlCommand("sp_save_dh_attachments", conn);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@_dh_id", MySqlDbType.UInt32).Value = filter._dh_id;
                            cmd.Parameters.Add("@_dh_header_id", MySqlDbType.UInt32).Value = filter._dh_header_id;
                            cmd.Parameters.Add("@_fname", MySqlDbType.String).Value = filter._fname;
                            cmd.Parameters.Add("@_fpath", MySqlDbType.String).Value = filter._fpath;
                            cmd.Parameters.Add("@_description", MySqlDbType.String).Value = filter._description;
                            cmd.Parameters.Add("@_user_id", MySqlDbType.String).Value = filter._created_by;
                            conn.Open();
                            using (IDataReader dr = cmd.ExecuteReader())
                            {
                                while (dr.Read())
                                {
                                    SaveAttachementDetailsParameters _Details = new SaveAttachementDetailsParameters();
                                    _Details.status = dr.IsNull<string>("status");
                                    _Details.message = dr.IsNull<string>("message");
                                    _SaveAttachementDetailsParameters.Add(_Details);
                                }
                            }

                        }
                        else
                        {
                            SaveAttachementDetailsParameters _Details = new SaveAttachementDetailsParameters();
                            _Details.status = "Success";
                            _Details.message = "Successfull";
                            _SaveAttachementDetailsParameters.Add(_Details);
                        }
                    }
                    }
                

                return _SaveAttachementDetailsParameters;
            }
            catch (Exception ex)
            {
                _SaveAttachementDetailsParameters = new List<SaveAttachementDetailsParameters>();

                SaveAttachementDetailsParameters _Details = new SaveAttachementDetailsParameters();
                _Details.status = "Failed";
                _Details.message = "Error in saving parameters";
                _SaveAttachementDetailsParameters.Add(_Details);

                return _SaveAttachementDetailsParameters;
            }
        }

        public static string GetOBFSummaryDataVersionWise(int dh_id,int dh_header_id)
        {
            try
            {

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    MySqlDataAdapter DA = new MySqlDataAdapter();
                    MySqlCommand cmd = new MySqlCommand("sp_getOBFSummaryData_versionwise", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("dh_id", MySqlDbType.String).Value = dh_id;
                    cmd.Parameters.Add("dh_header_id", MySqlDbType.String).Value = dh_header_id;
                    DA.SelectCommand = cmd;
                    cmd.Connection = new MySqlConnection(connectionString);
                    DataSet ds = new DataSet();
                    DA.Fill(ds);

                    DataSet rds = ds.GetTableName();

                    return JsonConvert.SerializeObject(rds, Formatting.Indented); ;

                }

            }
            catch (Exception ex)
            {
                return "error";
            }

        }
        public static string GetAttachmentDocument(int dh_id, int dh_header_id)
        {
            try
            {

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    MySqlDataAdapter DA = new MySqlDataAdapter();
                    MySqlCommand cmd = new MySqlCommand("sp_get_dh_attachments", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("_dh_id", MySqlDbType.String).Value = dh_id;
                    cmd.Parameters.Add("_dh_header_id", MySqlDbType.String).Value = dh_header_id;
                    DA.SelectCommand = cmd;
                    cmd.Connection = new MySqlConnection(connectionString);
                    DataSet ds = new DataSet();
                    DA.Fill(ds);

                    DataSet rds = ds.GetTableName();

                    return JsonConvert.SerializeObject(rds, Formatting.Indented); ;

                }

            }
            catch (Exception ex)
            {
                return "error";
            }

        }
        
    }
}
