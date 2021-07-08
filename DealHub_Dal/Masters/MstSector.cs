using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DealHub_Dal.Extensions;
using DealHub_Domain.DashBoard;
using DealHub_Domain.Masters;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System.Data;

namespace DealHub_Dal.Masters
{
    public class MstSector:BaseDAL
    {
        public static string GetMstSector(Mstcommonparameters model)
        {
            try
            {

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    MySqlDataAdapter DA = new MySqlDataAdapter();
                    MySqlCommand cmd = new MySqlCommand("sp_get_mst_sector", conn);
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
                return "error";
            }

        }

        public static List<commanmessges> UpdateMstSector(MstSectorParameter model)
        {
            List<commanmessges> _commanmessges = new List<commanmessges>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_update_mst_sector", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("_Sector_Id", MySqlDbType.UInt32).Value = model._Sector_Id;
                    cmd.Parameters.Add("_Sector_Name", MySqlDbType.String).Value = model._Sector_Name;
                    cmd.Parameters.Add("_active", MySqlDbType.String).Value = model._active;
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
                _commanmessges = new List<commanmessges>();

                commanmessges _Details = new commanmessges();
                _Details.status = "Failed";
                _Details.message = "Error in saving parameters";
                _commanmessges.Add(_Details);

                return _commanmessges;
            }
        }
    }
}
