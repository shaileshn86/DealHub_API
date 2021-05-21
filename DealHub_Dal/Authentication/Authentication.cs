using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DealHub_Domain;
using DealHub_Domain.Authentication;
using MySql.Data.MySqlClient;
using System.Data;
using DealHub_Dal.Extensions;
using System.Net.Mail;

namespace DealHub_Dal.Authentication
{
    public class Authentication : BaseDAL
    {
        public static List<AuthenticationDetailParameters> AutheticateUser(AuthenticationParameters filter)
        {
            List<AuthenticationDetailParameters> authuser = new List<AuthenticationDetailParameters>();
            try
            {
                //sp_auth_user
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_auth_user", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@_user_code", MySqlDbType.String).Value = filter._user_code;
                    cmd.Parameters.Add("@_password", MySqlDbType.String).Value = filter._password;
                    conn.Open();
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            AuthenticationDetailParameters _AuthenticationDetailParameters = new AuthenticationDetailParameters();
                            string status = dr.IsNull<string>("status");
                            _AuthenticationDetailParameters.status = status;
                            if (status != "success")
                            {

                                _AuthenticationDetailParameters.user_code = "";


                            }
                            else
                            {
                                _AuthenticationDetailParameters.user_id = dr.IsNull<uint>("id");
                                _AuthenticationDetailParameters.user_code = dr.IsNull<string>("user_code");
                                _AuthenticationDetailParameters.password = dr.IsNull<string>("password");
                                _AuthenticationDetailParameters.privilege_name = dr.IsNull<string>("privilege_name");

                            }
                            authuser.Add(_AuthenticationDetailParameters);

                        }
                    }
                }
                return authuser;
            }
            catch (Exception e)
            {
                AuthenticationDetailParameters _AuthenticationDetailParameters = new AuthenticationDetailParameters();
                string status = "failed with exception in code";
                _AuthenticationDetailParameters.status = status;
                authuser.Add(_AuthenticationDetailParameters);
                return authuser;

            }
        }

        public static int UpdateToken(AuthenticationParameters filter)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_save_token", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@_user_code", MySqlDbType.String).Value = filter._user_code;
                    cmd.Parameters.Add("@_token", MySqlDbType.String).Value = filter._token;
                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    return result;
                }


            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public static string GetToken(AuthenticationParameters filter)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_GetToken", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@_user_code", MySqlDbType.String).Value = filter._user_code;
                    cmd.Parameters.Add("@_token", MySqlDbType.String).Value = filter._token;
                    conn.Open();
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            string status = dr.IsNull<string>("status");
                            return status;
                        }
                    }
                }

                return "No Result UnAuthorized";
            }
            catch (Exception e)
            {
                return "System Error";
            }
        }
        public static string ResetPassword(AuthenticationParameters filter)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_resetpassword", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@_user_code", MySqlDbType.String).Value = filter._user_code;
                    cmd.Parameters.Add("@_password", MySqlDbType.String).Value = filter._password;
                    conn.Open();
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            string status = dr.IsNull<string>("status");
                            return status;
                        }
                    }
                }

                return "No Result UnAuthorized";
            }
            catch (Exception e)
            {
                return "System Error";
            }
        }

        public static string sendmail(string usercode)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_get_userEmailID", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@_user_code", MySqlDbType.String).Value = usercode;
                    conn.Open();
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            string status = dr.IsNull<string>("status");
                            string EmailId = dr.IsNull<string>("email_id");
                            return EmailId;
                        }
                    }
                }
                return "No Result UnAuthorized";

            }
            catch (Exception)
            {
                return "System Error";
            }
        }

       


    }
}
