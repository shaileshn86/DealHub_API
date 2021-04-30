﻿using System;
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

                    conn.Open();
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ObfCreationDetailsParameters _ObfCreationDetailsParameters = new ObfCreationDetailsParameters();

                            //_DashBoardDetailsParameters.obf_id = dr.IsNull<uint>("obf_id");
                            _ObfCreationDetailsParameters.Result = dr.IsNull<string>("result");
                            _ObfCreationDetailsParameters.dh_id = dr.IsNull<uint>("dh_id");
                            _ObfCreationDetailsParameters.dh_id = dr.IsNull<uint>("dh_header_id");

                            foreach (SaveAttachmentParameter attachement in filter.Attachments)
                            {
                                attachement._dh_id= Convert.ToInt32( dr.IsNull<uint>("dh_id"));
                                attachement._dh_header_id = Convert.ToInt32(dr.IsNull<uint>("dh_id"));
                            }


                            _ObfCreationData.Add(_ObfCreationDetailsParameters);

                        }
                    }

                    // call of save attachments
                    SaveAttachments(filter.Attachments);

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
                    MySqlCommand cmd = new MySqlCommand("SP_DH_GetMasters", conn);
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



    }
}