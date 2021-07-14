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

namespace DealHub_Dal.DashBoard
{
    public class DashBoard:BaseDAL
    {
        public static List<DashBoardDetailsParameters> GetDashBoardData(DashBoardParameters filter)
        {
            List<DashBoardDetailsParameters> DashBoardData = new List<DashBoardDetailsParameters>();
            try
            {
                //sp_auth_user
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_getdashboardgriddata", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@_user_code", MySqlDbType.String).Value = filter._user_code;
                    conn.Open();
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            DashBoardDetailsParameters _DashBoardDetailsParameters = new DashBoardDetailsParameters();

                           
                            _DashBoardDetailsParameters.dh_id = dr.IsNull<uint>("dh_id");
                            _DashBoardDetailsParameters.dh_header_id = dr.IsNull<uint>("dh_header_id");
                            _DashBoardDetailsParameters.Current_Status = dr.IsNull<string>("CurrentStatus");
                            _DashBoardDetailsParameters.Project_Name = dr.IsNull<string>("dh_project_name");
                            _DashBoardDetailsParameters.Code = dr.IsNull<string>("dh_code");
                            _DashBoardDetailsParameters.Opp_Id = dr.IsNull<string>("opportunity_id");
                            _DashBoardDetailsParameters.Created_On = dr.IsNull<DateTime>("createdon");
                            _DashBoardDetailsParameters.Created_By = dr.IsNull<uint>("createdby");
                           
                            _DashBoardDetailsParameters.Total_Cost = dr.IsNull<decimal>("total_cost");
                            _DashBoardDetailsParameters.Total_Revenue = dr.IsNull<decimal>("total_revenue");
                            _DashBoardDetailsParameters.Gross_Margin = dr.IsNull<decimal>("total_margin");
                            _DashBoardDetailsParameters.mainobf = dr.IsNull<string>("mainobf");
                           
                            _DashBoardDetailsParameters.version_name = dr.IsNull<string>("version_name");
                            _DashBoardDetailsParameters.Current_Status = dr.IsNull<string>("currentstatus");
                            _DashBoardDetailsParameters.shortcurrentstatus = dr.IsNull<string>("shortcurrentstatus");
                           
                            _DashBoardDetailsParameters.phase_code = dr.IsNull<string>("phase_code");
                            _DashBoardDetailsParameters.ppl_init = dr.IsNull<int>("ppl_init");
                            _DashBoardDetailsParameters.ppl_status = dr.IsNull<string>("ppl_status");

                            _DashBoardDetailsParameters.customer_name = dr.IsNull<string>("customer_name");
                            _DashBoardDetailsParameters.dh_location = dr.IsNull<string>("dh_location");
                            _DashBoardDetailsParameters.Vertical_Name = dr.IsNull<string>("Vertical_name");
                            _DashBoardDetailsParameters.sap_customer_code = dr.IsNull<string>("sap_customer_code");
                            _DashBoardDetailsParameters.sector_name = dr.IsNull<string>("sector_name");
                            _DashBoardDetailsParameters.subsector_name = dr.IsNull<string>("subsector_name");
                            _DashBoardDetailsParameters.solutioncategory_name = dr.IsNull<string>("solutioncategory_name");
                            _DashBoardDetailsParameters.currentstatus_search = dr.IsNull<string>("currentstatus_search");
                            _DashBoardDetailsParameters.is_submitted = dr.IsNull<int>("is_submitted");
                            _DashBoardDetailsParameters.Project_Type = dr.IsNull<string>("project_type");
                            _DashBoardDetailsParameters.progresspercentage = dr.IsNull<int>("progresspercentage");
                            DashBoardData.Add(_DashBoardDetailsParameters);

                        }
                    }
                }
                return DashBoardData;
            }
            catch (Exception e)
            {
                return null;

            }
        }


        public static List<DashBoardDetailsCountParameters> GetDashBoardDataCount(DashBoardParameters filter)
        {
            List<DashBoardDetailsCountParameters> DashBoardData = new List<DashBoardDetailsCountParameters>();
            try
            {
                //sp_auth_user
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_getdashboardcount", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@_user_code", MySqlDbType.String).Value = filter._user_code;
                    conn.Open();
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            DashBoardDetailsCountParameters _DashBoardDetailsCountParameters = new DashBoardDetailsCountParameters();

                            _DashBoardDetailsCountParameters._draft_obf = dr.IsNull<long>("_draft_obf");
                            _DashBoardDetailsCountParameters._draft_ppl = dr.IsNull<long>("_draft_ppl");
                            _DashBoardDetailsCountParameters._draft = _DashBoardDetailsCountParameters._draft_obf + _DashBoardDetailsCountParameters._draft_ppl;

                            _DashBoardDetailsCountParameters._submitted_obf = dr.IsNull<long>("_submitted_obf");
                            _DashBoardDetailsCountParameters._submitted_ppl = dr.IsNull<long>("_submitted_ppl");
                            _DashBoardDetailsCountParameters._submitted = _DashBoardDetailsCountParameters._submitted_obf + _DashBoardDetailsCountParameters._submitted_ppl;


                            _DashBoardDetailsCountParameters._rejected_obf = dr.IsNull<long>("_rejected_obf");
                            _DashBoardDetailsCountParameters._rejected_ppl = dr.IsNull<long>("_rejected_ppl");
                            _DashBoardDetailsCountParameters._rejected = _DashBoardDetailsCountParameters._rejected_obf + _DashBoardDetailsCountParameters._rejected_ppl;

                            _DashBoardDetailsCountParameters._approved_obf = dr.IsNull<long>("_approved_obf");
                            _DashBoardDetailsCountParameters._approved_ppl = dr.IsNull<long>("_approved_ppl");
                            _DashBoardDetailsCountParameters._approved = _DashBoardDetailsCountParameters._approved_obf + _DashBoardDetailsCountParameters._approved_ppl;

                            Decimal _pendingobf = _DashBoardDetailsCountParameters._submitted_obf - (_DashBoardDetailsCountParameters._approved_obf + _DashBoardDetailsCountParameters._rejected_obf);

                            Decimal _pendingppl = _DashBoardDetailsCountParameters._submitted_ppl - (_DashBoardDetailsCountParameters._approved_ppl + _DashBoardDetailsCountParameters._rejected_ppl );

                            _DashBoardDetailsCountParameters._pendingobf = _pendingobf;
                            _DashBoardDetailsCountParameters._pendingppl = _pendingppl;
                            _DashBoardDetailsCountParameters._TotalPending = _pendingobf + _pendingppl;


                            _DashBoardDetailsCountParameters._totalapprovedppl = dr.IsNull<long>("_totalapprovedppl");
                            _DashBoardDetailsCountParameters._totalapprovedobf = dr.IsNull<long>("_totalapprovedobf");


                            DashBoardData.Add(_DashBoardDetailsCountParameters);

                        }
                    }
                }
                return DashBoardData;
            }
            catch (Exception e)
            {
                return null;

            }
        }

        public static string GetOBFSummaryDetails(int dh_id)
        {
            try
            {

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    MySqlDataAdapter DA = new MySqlDataAdapter();
                    MySqlCommand cmd = new MySqlCommand("sp_getOBFSummaryData", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("dh_id", MySqlDbType.String).Value = dh_id;
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

        public static List<timelinehistroy> GetDetailTimelineHistory(int dh_id,int dh_header_id)
        {
            try
            {
                List<timelinehistroy> TimelineData = new List<timelinehistroy>();
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_dh_get_detailedtimeline", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@_dh_id", MySqlDbType.String).Value = dh_id;
                    cmd.Parameters.Add("@_dh_header_id", MySqlDbType.String).Value = dh_header_id;
                    conn.Open(); ;
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            timelinehistroy _timelinehistroy = new timelinehistroy();


                            _timelinehistroy.dh_id = dr.IsNull<uint>("dh_id");
                            _timelinehistroy.dh_header_id = dr.IsNull<uint>("dh_header_id");
                            _timelinehistroy.username = dr.IsNull<string>("username");
                            _timelinehistroy.currentstatus = dr.IsNull<string>("currentstatus");
                            _timelinehistroy.comments = dr.IsNull<string>("comments");
                            _timelinehistroy.TimeLine= dr.IsNull<string>("TimeLine");
                            _timelinehistroy.actions = dr.IsNull<string>("actions");
                            //_timelinehistroy.TimeLine = "ABCGFDETGHUNKKJNSYSGDHDJHDHDJHDJHDJHDJDHJDHH";
                            TimelineData.Add(_timelinehistroy);

                        }
                    }


                    return TimelineData;
                }

            }
            catch 
            {
                throw;
            }

        }

        public static string GetOBFSummaryDetails_version(int dh_id,int dh_header_id)
        {
            try
            {

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    MySqlDataAdapter DA = new MySqlDataAdapter();
                    MySqlCommand cmd = new MySqlCommand("sp_getOBFSummaryData_versionwise", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@dh_id", MySqlDbType.String).Value = dh_id;
                    cmd.Parameters.Add("@dh_header_id", MySqlDbType.String).Value = dh_header_id;
                    DA.SelectCommand = cmd;
                    cmd.Connection = new MySqlConnection(connectionString);
                    DataSet ds = new DataSet();
                    DA.Fill(ds);

                    DataSet rds = ds.GetTableName();

                    return JsonConvert.SerializeObject(rds, Formatting.Indented); ;

                }

            }
            
            catch 
            {
                throw;
            }

        }



        public static string GetDashboardProgress(int dh_id)
        {
            try
            {

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    MySqlDataAdapter DA = new MySqlDataAdapter();
                    MySqlCommand cmd = new MySqlCommand("sp_get_dashboard_progress", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("_dh_id", MySqlDbType.String).Value = dh_id;
                    DA.SelectCommand = cmd;
                    cmd.Connection = new MySqlConnection(connectionString);
                    DataSet ds = new DataSet();
                    DA.Fill(ds);

                    DataSet rds = ds.GetTableName();

                    return JsonConvert.SerializeObject(rds, Formatting.Indented); ;

                }

            }
            catch //(Exception ex)
            {
                throw;
               // return "error";
            }

        }
    }
}
