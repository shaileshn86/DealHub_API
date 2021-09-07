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
using DealHub_Dal.ErrorLog;
using System.Configuration;

namespace DealHub_Dal.Masters
{
    public class MstRole:BaseDAL
    {
        public static string GetMstRole(Mstcommonparameters model)
        {
            try
            {

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    MySqlDataAdapter DA = new MySqlDataAdapter();
                    MySqlCommand cmd = new MySqlCommand("sp_get_mst_roles", conn);
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

        public static List<MstRoleDetailParameters> Update_Mst_Roles(MstRoleParameters model)
        {
            List<MstRoleDetailParameters> _commanmessges = new List<MstRoleDetailParameters>();
            try
            {
                int role_id = model._id;
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_update_mst_roles", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("_id", MySqlDbType.UInt32).Value = model._id;
                    cmd.Parameters.Add("_role_code", MySqlDbType.String).Value = model._role_code;
                    cmd.Parameters.Add("_role_name", MySqlDbType.String).Value = model._role_name;
                    cmd.Parameters.Add("_equivalent_cassh_role_name", MySqlDbType.String).Value = model._equivalent_cassh_role_name;
                    cmd.Parameters.Add("_active", MySqlDbType.String).Value = model._active;

                    cmd.Parameters.Add("_user_id", MySqlDbType.String).Value = model._user_id;
                    conn.Open();
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            MstRoleDetailParameters _Details = new MstRoleDetailParameters();
                            _Details.status = dr.IsNull<string>("status");
                            _Details.message = dr.IsNull<string>("message");

                            var updatedid = dr["role_id"];


                            role_id = Convert.ToInt32(updatedid);
                            _Details._role_id = (ulong)role_id;
                            //role_id = Convert.ToInt32(_Details._role_id);

                            //_Details._role_id = dr.IsNull<long>("role_id");
                            //role_id = Convert.ToInt32(_Details._role_id);

                            model._id = role_id;
                            _commanmessges.Add(_Details);
                        }
                    }

                    if(role_id > 0)
                    {
                        UpdateMapPrivilegeRole(model);
                    }

                    
                }



                return _commanmessges;
            }
            catch (Exception ex)
            {
                writelogobfcreation(ex.ToString());
                _commanmessges = new List<MstRoleDetailParameters>();

                MstRoleDetailParameters _Details = new MstRoleDetailParameters();
                _Details.status = "Failed";
                _Details.message = "Error in saving parameters";
                _commanmessges.Add(_Details);

                return _commanmessges;
            }
        }


        public static List<MstRoleDetailParameters> UpdateMapPrivilegeRole(MstRoleParameters model)
        {
            List<MstRoleDetailParameters> _commanmessges = new List<MstRoleDetailParameters>();
            try
            {
                string[] mappedprivilage = model._Previlege_Id.Split(',');
                for (int k = 0; k < mappedprivilage.Length; k++)
                {

                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        MySqlCommand cmd = new MySqlCommand("sp_update_map_privilege_role", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("_Role_Id", MySqlDbType.UInt32).Value = model._id;
                        cmd.Parameters.Add("_Previlege_Id", MySqlDbType.UInt32).Value = Convert.ToUInt32(mappedprivilage[k]);
                        cmd.Parameters.Add("_user_id", MySqlDbType.String).Value = model._user_id;
                        conn.Open();
                        using (IDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                MstRoleDetailParameters _Details = new MstRoleDetailParameters();
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
                writelogobfcreation(ex.ToString());
                _commanmessges = new List<MstRoleDetailParameters>();

                MstRoleDetailParameters _Details = new MstRoleDetailParameters();
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
