using DealHub_Dal.Extensions;
using DealHub_Domain.DashBoard;
using DealHub_Domain.Masters;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealHub_Dal.Masters
{
    public class MstVerticals:BaseDAL
    {
        public static string GetMstVerticals(Mstcommonparameters model)
        {
            try
            {

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    MySqlDataAdapter DA = new MySqlDataAdapter();
                    MySqlCommand cmd = new MySqlCommand("sp_get_mst_verticals", conn);
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

        public static List<MstVerticalsDetailParameters> Update_Mst_Verticals(MstVerticalsParameters model)
        {
            List<MstVerticalsDetailParameters> _commanmessges = new List<MstVerticalsDetailParameters>();
            try
            {
                int verticalid = model._vertical_id;
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_update_mst_verticals", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("_vertical_id", MySqlDbType.UInt32).Value = model._vertical_id;
                    cmd.Parameters.Add("_vertical_code", MySqlDbType.String).Value = model._vertical_code;
                    cmd.Parameters.Add("_vertical_name", MySqlDbType.String).Value = model._vertical_name;
                    cmd.Parameters.Add("_function_id", MySqlDbType.UInt32).Value = model._function_id;
                    cmd.Parameters.Add("_active", MySqlDbType.String).Value = model._active;

                    cmd.Parameters.Add("_user_id", MySqlDbType.String).Value = model._user_id;
                    conn.Open();
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            MstVerticalsDetailParameters _Details = new MstVerticalsDetailParameters();
                            _Details.status = dr.IsNull<string>("status");
                            _Details.message = dr.IsNull<string>("message");
                          //  _Details._vertical_id = dr.IsNull<long>("_vertical_id");
                            var updatedid = dr["_vertical_id"];


                            verticalid = Convert.ToInt32(updatedid);
                            _Details._vertical_id = (ulong)verticalid;
                            verticalid = Convert.ToInt32( _Details._vertical_id);

                             model._vertical_id = verticalid;
                            _commanmessges.Add(_Details);
                        }
                    }

                    UpdateMapVerticalSector(model);
                }



                return _commanmessges;
            }
            catch (Exception ex)
            {
                _commanmessges = new List<MstVerticalsDetailParameters>();

                MstVerticalsDetailParameters _Details = new MstVerticalsDetailParameters();
                _Details.status = "Failed";
                _Details.message = "Error in saving parameters";
                _commanmessges.Add(_Details);

                return _commanmessges;
            }
        }


        public static List<MstVerticalsDetailParameters> UpdateMapVerticalSector(MstVerticalsParameters model)
        {
            List<MstVerticalsDetailParameters> _commanmessges = new List<MstVerticalsDetailParameters>();
            try
            {
                string[] mappedsectors = model._Sector_Id.Split(',');
                for (int k=0;k<mappedsectors.Length;k++)
                {

                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        MySqlCommand cmd = new MySqlCommand("sp_update_map_vertical_sector", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("_vertical_id", MySqlDbType.UInt32).Value = model._vertical_id;
                        cmd.Parameters.Add("_Sector_Id", MySqlDbType.UInt32).Value =Convert.ToUInt32(mappedsectors[k]);
                        cmd.Parameters.Add("_user_id", MySqlDbType.String).Value = model._user_id;
                        conn.Open();
                        using (IDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                MstVerticalsDetailParameters _Details = new MstVerticalsDetailParameters();
                                _Details.status = dr.IsNull<string>("status");
                                _Details.message = dr.IsNull<string>("message");
                               // _Details._vertical_id = dr.IsNull<ulong>("_vertical_id");
                               
                                _commanmessges.Add(_Details);
                            }
                        }
                    }

                }
                



                return _commanmessges;
            }
            catch (Exception ex)
            {
                _commanmessges = new List<MstVerticalsDetailParameters>();

                MstVerticalsDetailParameters _Details = new MstVerticalsDetailParameters();
                _Details.status = "Failed";
                _Details.message = "Error in saving parameters";
                _commanmessges.Add(_Details);

                return _commanmessges;
            }
        }

    }
}
