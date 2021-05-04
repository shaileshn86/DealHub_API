using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DealHub_Domain.DashBoard;
using MySql.Data.MySqlClient;
using System.Data;
using DealHub_Dal.Extensions;

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
                            _DashBoardDetailsParameters.ApprovalStatus = dr.IsNull<string>("ApprovalStatus");
                            _DashBoardDetailsParameters.CurrentStatus = dr.IsNull<string>("CurrentStatus");
                            _DashBoardDetailsParameters.DetailedOBF = dr.IsNull<string>("DetailedOBF");
                            _DashBoardDetailsParameters.FinalAgg = dr.IsNull<string>("FinalAgg");
                            _DashBoardDetailsParameters.ProjectName = dr.IsNull<string>("ProjectName");
                            _DashBoardDetailsParameters.Code = dr.IsNull<string>("code");
                            _DashBoardDetailsParameters.Opp_Id = dr.IsNull<string>("oppid");
                            _DashBoardDetailsParameters.Created_On = dr.IsNull<string>("datecreated");
                            _DashBoardDetailsParameters.Created_By = dr.IsNull<string>("createdby");
                            
                           // _DashBoardDetailsParameters.vertical_id = dr.IsNull<uint>("vertical_id");
                            _DashBoardDetailsParameters.vertical = dr.IsNull<string>("vertical");
                            _DashBoardDetailsParameters.Project_Type = dr.IsNull<string>("projecttype");
                            _DashBoardDetailsParameters.Payament_Terms = dr.IsNull<string>("paymentterms");
                            _DashBoardDetailsParameters.Capex = dr.IsNull<decimal>("capex");
                            _DashBoardDetailsParameters.Total_Cost = dr.IsNull<decimal>("TotalCost");
                            _DashBoardDetailsParameters.Total_Revenue = dr.IsNull<string>("TotalRevenue");
                            _DashBoardDetailsParameters.Gross_Margin = dr.IsNull<string>("GrossMargin");


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
                    MySqlCommand cmd = new MySqlCommand("GetDashBoardCount", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@_user_code", MySqlDbType.String).Value = filter._user_code;
                    conn.Open();
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            DashBoardDetailsCountParameters _DashBoardDetailsCountParameters = new DashBoardDetailsCountParameters();

                            //_DashBoardDetailsParameters.obf_id = dr.IsNull<uint>("obf_id");
                            _DashBoardDetailsCountParameters.count = dr.IsNull<long>("count");
                            _DashBoardDetailsCountParameters.process_id = dr.IsNull<int>("process_id");
                            _DashBoardDetailsCountParameters.process_code = dr.IsNull<string>("process_code");
                          


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
    }
}
