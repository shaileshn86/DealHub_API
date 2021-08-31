using DealHub_Dal.ErrorLog;
using DealHub_Dal.Extensions;
using DealHub_Domain.DashBoard;
using DealHub_Domain.Masters;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealHub_Dal.Masters
{
    public class MstFunctions:BaseDAL
    {
        public static string GetMstFunctions(Mstcommonparameters model)
        {
            try
            {

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    MySqlDataAdapter DA = new MySqlDataAdapter();
                    MySqlCommand cmd = new MySqlCommand("sp_get_mst_functions", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("_user_id", MySqlDbType.String).Value = model._user_id;
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
                writelogobfcreation(ex.ToString());
                return "error";
            }

        }

        public static List<commanmessges> UpdateMstFunctions(MstFunctionParameters model)
        {
            List<commanmessges> _commanmessges = new List<commanmessges>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_update_mst_functions", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("_function_id", MySqlDbType.UInt32).Value = model._function_id;
                    cmd.Parameters.Add("_function_code", MySqlDbType.String).Value = model._function_code;
                    cmd.Parameters.Add("_function_name", MySqlDbType.String).Value = model._function_name;
                    cmd.Parameters.Add("_active", MySqlDbType.UInt32).Value = model._active;
                    cmd.Parameters.Add("_user_id", MySqlDbType.String).Value = model._user_id;
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



                return _commanmessges;
            }
            catch (Exception ex)
            {
                writelogobfcreation(ex.ToString());
                _commanmessges = new List<commanmessges>();

                commanmessges _Details = new commanmessges();
                _Details.status = "Failed";
                _Details.message = "Error in saving parameters";
                _commanmessges.Add(_Details);

                return _commanmessges;
            }
        }

        public static void writelogobfcreation(string errordetails)
        {
            WritetoLogFile W = new WritetoLogFile();
            W.LogEvent(ConfigurationManager.AppSettings["logfilepath"].ToString(), errordetails, true);
        }
    }
}
