using DealHub_Dal.ErrorLog;
using DealHub_Dal.Extensions;
using DealHub_Dal.OBF;
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
    public class MstUsers:BaseDAL
    {
        public static string GetMstUsers(Mstcommonparameters model)
        {
            try
            {

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    MySqlDataAdapter DA = new MySqlDataAdapter();
                    MySqlCommand cmd = new MySqlCommand("sp_get_mst_users", conn);
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

        public static List<MstUserDetailParameters> Update_Mst_Users(MstUsersParameters model)
        {
            List<MstUserDetailParameters> _commanmessges = new List<MstUserDetailParameters>();
            try
            {
                int _mapped_User_Id = model._id;
                bool sendmail = false;
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_update_mst_users", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("_id", MySqlDbType.UInt32).Value = model._id;
                    cmd.Parameters.Add("_user_code", MySqlDbType.String).Value = model._user_code;
                    cmd.Parameters.Add("_first_name", MySqlDbType.String).Value = model._first_name;
                    cmd.Parameters.Add("_last_name", MySqlDbType.String).Value = model._last_name;
                    cmd.Parameters.Add("_password", MySqlDbType.String).Value = model._password;
                    cmd.Parameters.Add("_mobile_no", MySqlDbType.String).Value = model._mobile_no;
                    cmd.Parameters.Add("_email_id", MySqlDbType.String).Value = model._email_id;
                    cmd.Parameters.Add("_role_id", MySqlDbType.UInt32).Value = model._role_id;
                    cmd.Parameters.Add("_is_cassh_user", MySqlDbType.UInt32).Value = model._is_cassh_user;
                    cmd.Parameters.Add("_active", MySqlDbType.String).Value = model._active;
                    cmd.Parameters.Add("_islocked", MySqlDbType.UInt32).Value = model._islocked;
                    cmd.Parameters.Add("_user_id", MySqlDbType.String).Value = model._user_id;
                    conn.Open();
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            MstUserDetailParameters _Details = new MstUserDetailParameters();
                            _Details.status = dr.IsNull<string>("status");
                            _Details.message = dr.IsNull<string>("message");
                            var updatedid = dr["user_id"];
                            
                            if (model._id ==0)
                            {
                                sendmail = true;  
                            }
                            
                             
                            _mapped_User_Id = Convert.ToInt32(updatedid);
                            _Details._updateduser_id =(ulong)_mapped_User_Id;
                            model._id = _mapped_User_Id;
                            _commanmessges.Add(_Details);
                        }
                    }

                    if (_mapped_User_Id > 0)// if sp returns -1 dont update mapped vertical and branches
                    {
                        UpdateMapUsersVertical(model);
                        UpdateMapUsersBranch(model);
                        try
                        {
                            if (sendmail)
                            {
                                EmailSendModelUserCreation model1 = new EmailSendModelUserCreation();
                                model1._user_code = model._user_code;
                                model1._encpassword = model._encpassword;

                                EmailSender_DAL.UserCreationMail(model1);
                            }
                           
                        }
                        catch(Exception ex)
                        {

                        }
                    }
                    
                }



                return _commanmessges;
            }
            catch (Exception ex)
            {
                writelogobfcreation(ex.ToString());
                _commanmessges = new List<MstUserDetailParameters>();

                MstUserDetailParameters _Details = new MstUserDetailParameters();
                _Details.status = "Failed";
                _Details.message = "Error in saving parameters";
                _commanmessges.Add(_Details);

                return _commanmessges;
            }
        }


        public static List<MstUserDetailParameters> Update_Mst_Users_Dashboard(MstUpdateUsersParameters model)
        {
            List<MstUserDetailParameters> _commanmessges = new List<MstUserDetailParameters>();
            try
            {
                int _mapped_User_Id = model._id;
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_update_mst_users_dashboard", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("_id", MySqlDbType.UInt32).Value = model._id;
                    cmd.Parameters.Add("_is_cassh_user", MySqlDbType.UInt32).Value = model._is_cassh_user;
                    cmd.Parameters.Add("_active", MySqlDbType.String).Value = model._active;
                    cmd.Parameters.Add("_islocked", MySqlDbType.UInt32).Value = model._islocked;
                    cmd.Parameters.Add("_user_id", MySqlDbType.String).Value = model._user_id;
                    conn.Open();
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            MstUserDetailParameters _Details = new MstUserDetailParameters();
                            _Details.status = dr.IsNull<string>("status");
                            _Details.message = dr.IsNull<string>("message");
                            //  _Details._updateduser_id = dr.IsNull<ulong>("user_id");
                            var updatedid = dr["user_id"];
                            _mapped_User_Id = Convert.ToInt32(updatedid);
                            _Details._updateduser_id = (ulong)_mapped_User_Id;
                           // _mapped_User_Id = Convert.ToInt32(_Details._updateduser_id);
                            model._id = _mapped_User_Id;
                            _commanmessges.Add(_Details);
                        }
                    }

                   
                }



                return _commanmessges;
            }
            catch (Exception ex)
            {
                writelogobfcreation(ex.ToString());
                _commanmessges = new List<MstUserDetailParameters>();

                MstUserDetailParameters _Details = new MstUserDetailParameters();
                _Details.status = "Failed";
                _Details.message = "Error in saving parameters";
                _commanmessges.Add(_Details);

                return _commanmessges;
            }
        }

        public static List<MstUserDetailParameters> UpdateMapUsersVertical(MstUsersParameters model)
        {
            List<MstUserDetailParameters> _commanmessges = new List<MstUserDetailParameters>();
            try
            {
                string[] mappedverticals = model._mappedverticals.Split(',');
                for (int k = 0; k < mappedverticals.Length; k++)
                {

                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        MySqlCommand cmd = new MySqlCommand("sp_update_map_users_vertical", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("_mapped_User_Id", MySqlDbType.UInt32).Value = model._id;
                        cmd.Parameters.Add("_Vertical_Id", MySqlDbType.UInt32).Value = Convert.ToUInt32(mappedverticals[k]);
                        cmd.Parameters.Add("_user_id", MySqlDbType.String).Value = model._user_id;
                        conn.Open();
                        using (IDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                MstUserDetailParameters _Details = new MstUserDetailParameters();
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
                _commanmessges = new List<MstUserDetailParameters>();

                MstUserDetailParameters _Details = new MstUserDetailParameters();
                _Details.status = "Failed";
                _Details.message = "Error in saving parameters";
                _commanmessges.Add(_Details);

                return _commanmessges;
            }
        }


        public static List<MstUserDetailParameters> UpdateMapUsersBranch(MstUsersParameters model)
        {
            List<MstUserDetailParameters> _commanmessges = new List<MstUserDetailParameters>();
            try
            {
                string[] mappedbranches = model._mappedbranches.Split(',');
                for (int k = 0; k < mappedbranches.Length; k++)
                {

                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        MySqlCommand cmd = new MySqlCommand("sp_update_map_users_branch", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("_mapped_User_Id", MySqlDbType.UInt32).Value = model._id;
                        cmd.Parameters.Add("_Branch_Id", MySqlDbType.UInt32).Value = Convert.ToUInt32(mappedbranches[k]);
                        cmd.Parameters.Add("_user_id", MySqlDbType.String).Value = model._user_id;
                        conn.Open();
                        using (IDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                MstUserDetailParameters _Details = new MstUserDetailParameters();
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
                _commanmessges = new List<MstUserDetailParameters>();

                MstUserDetailParameters _Details = new MstUserDetailParameters();
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
