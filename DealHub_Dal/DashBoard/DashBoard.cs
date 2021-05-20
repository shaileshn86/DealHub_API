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
                    MySqlCommand cmd = new MySqlCommand("sp_GetDashBoardData", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@_user_code", MySqlDbType.String).Value = filter._user_code;
                    conn.Open();
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            DashBoardDetailsParameters _DashBoardDetailsParameters = new DashBoardDetailsParameters();

                            //_DashBoardDetailsParameters.obf_id = dr.IsNull<uint>("obf_id");
                            //_DashBoardDetailsParameters.ApprovalStatus = dr.IsNull<string>("ApprovalStatus");
                            _DashBoardDetailsParameters.dh_id = dr.IsNull<uint>("dh_id");
                            
                            _DashBoardDetailsParameters.CurrentStatus = dr.IsNull<string>("CurrentStatus");
                            //_DashBoardDetailsParameters.DetailedOBF = dr.IsNull<string>("DetailedOBF");
                            //_DashBoardDetailsParameters.FinalAgg = dr.IsNull<string>("FinalAgg");
                            _DashBoardDetailsParameters.ProjectName = dr.IsNull<string>("ProjectName");
                            _DashBoardDetailsParameters.Code = dr.IsNull<string>("code");
                            _DashBoardDetailsParameters.Opp_Id = dr.IsNull<string>("oppid");
                            _DashBoardDetailsParameters.Created_On = dr.IsNull<string>("datecreated");
                            _DashBoardDetailsParameters.Created_By = dr.IsNull<string>("createdby");
                            
                           // _DashBoardDetailsParameters.vertical_id = dr.IsNull<uint>("vertical_id");
                            _DashBoardDetailsParameters.vertical = dr.IsNull<string>("vertical");
                            _DashBoardDetailsParameters.Project_Type = dr.IsNull<string>("projecttype");
                            _DashBoardDetailsParameters.Payament_Terms = dr.IsNull<int>("paymentterms");
                            _DashBoardDetailsParameters.Capex = dr.IsNull<decimal>("capex");
                            _DashBoardDetailsParameters.Total_Cost = dr.IsNull<decimal>("TotalCost");
                            _DashBoardDetailsParameters.Total_Revenue = dr.IsNull<decimal>("TotalRevenue");
                            _DashBoardDetailsParameters.Gross_Margin = dr.IsNull<decimal>("GrossMargin");


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
    }
}
