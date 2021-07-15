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
using DealHub_Dal.ErrorLog;
using System.Configuration;

namespace DealHub_Dal.Authentication
{
    public class Authentication : BaseDAL
    {
        static int attempts;
        public static List<AuthenticationDetailParameters> AutheticateUser(AuthenticationParameters filter)
        {
            List<AuthenticationDetailParameters> authuser = new List<AuthenticationDetailParameters>();
            try
            {
                filter._user_code = filter._user_code.ToLower();
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
                                _AuthenticationDetailParameters.role_name = dr.IsNull<string>("role_name");
                                _AuthenticationDetailParameters.UserName = dr.IsNull<string>("user_name");

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

        public static DeleteTokenResponse deleteToken(string usercode)
        {
            DeleteTokenResponse deleteResult = new DeleteTokenResponse();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SET SQL_SAFE_UPDATES = 0;update mst_users set token='' where user_code=@usercode;SET SQL_SAFE_UPDATES = 1;", conn);
                    cmd.Parameters.AddWithValue("@usercode", usercode);
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        deleteResult.result = "Success";
                    }
                    else
                    {
                        deleteResult.result = "Failure";
                    }
                }
                 return deleteResult;
            }
            catch (Exception ex)
            {
                deleteResult.result = "Failure";
                return deleteResult;
            }
        }
        public static List<AuthenticationDetailParameters> AutheticateUserwithattempts(AuthenticationParameters filter)
        {
            List<AuthenticationDetailParameters> authuser = new List<AuthenticationDetailParameters>();
            try
            {
                filter._user_code = filter._user_code.ToLower();
                attempts = Convert.ToInt32(filter._attempt.ToString());
                DataSet ds = new DataSet();
                DataSet ds1 = new DataSet();
                //sp_auth_user
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("select id,LoginAttempt from mst_users where user_code=@username or email_id=@username", conn);
                    cmd.Parameters.AddWithValue("@username", filter._user_code);
                    cmd.Parameters.AddWithValue("@password", filter._password);

                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(ds);
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            attempts = Convert.ToInt32(ds.Tables[0].Rows[0]["LoginAttempt"]);
                            if (attempts == 3)
                            {
                                AuthenticationDetailParameters _AuthenticationDetailParameters = new AuthenticationDetailParameters();
                                string status = "Your Account Already Locked";
                                _AuthenticationDetailParameters.status = status;
                                authuser.Add(_AuthenticationDetailParameters);
                               // return authuser;
                            }
                            else
                            {
                                cmd = new MySqlCommand("sp_auth_user_attempt", conn);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add("@_user_code", MySqlDbType.String).Value = filter._user_code;
                                cmd.Parameters.Add("@_password", MySqlDbType.String).Value = filter._password;
                                da = new MySqlDataAdapter(cmd);
                                da.Fill(ds1);


                                if (ds1 != null)
                                {
                                    if (ds1.Tables[0].Rows.Count > 0 && ds1.Tables[0].Rows[0]["LoginAttempt"].ToString() !="")
                                    {
                                        filter._attempt = ds1.Tables[0].Rows[0]["LoginAttempt"].ToString();
                                        if (Convert.ToInt32(filter._attempt.ToString()) != 3)
                                        {
                                            cmd = new MySqlCommand("SET SQL_SAFE_UPDATES = 0;update mst_users set LoginAttempt=0 where (user_code=@username or email_id =@username)  and password=@password;SET SQL_SAFE_UPDATES = 1;", conn);
                                            cmd.Parameters.AddWithValue("@username", filter._user_code);
                                            cmd.Parameters.AddWithValue("@password", filter._password);
                                            cmd.ExecuteNonQuery();
                                            AuthenticationDetailParameters _AuthenticationDetailParameters = new AuthenticationDetailParameters();
                                            string status = ds1.Tables[0].Rows[0]["status"].ToString();
                                            _AuthenticationDetailParameters.status = status;
                                            if (status != "success")
                                            {

                                                _AuthenticationDetailParameters.user_code = "";


                                            }
                                            else
                                            {
                                                _AuthenticationDetailParameters.user_id = Convert.ToUInt32(ds1.Tables[0].Rows[0]["id"].ToString());
                                                _AuthenticationDetailParameters.user_code = ds1.Tables[0].Rows[0]["user_code"].ToString();
                                                _AuthenticationDetailParameters.password = ds1.Tables[0].Rows[0]["password"].ToString();
                                                _AuthenticationDetailParameters.privilege_name = ds1.Tables[0].Rows[0]["privilege_name"].ToString();
                                                _AuthenticationDetailParameters.role_name = ds1.Tables[0].Rows[0]["role_name"].ToString();
                                                _AuthenticationDetailParameters.UserName = ds1.Tables[0].Rows[0]["user_name"].ToString();

                                            }
                                            authuser.Add(_AuthenticationDetailParameters);
                                          //  return authuser;

                                        }
                                        else
                                        {
                                            AuthenticationDetailParameters _AuthenticationDetailParameters = new AuthenticationDetailParameters();
                                            string status = "Your Account Already Locked...Contact Administrator";
                                            _AuthenticationDetailParameters.status = status;
                                            authuser.Add(_AuthenticationDetailParameters);
                                           // return authuser;
                                        }
                                    }
                                    else
                                    {

                                        AuthenticationDetailParameters _AuthenticationDetailParameters = new AuthenticationDetailParameters();

                                        string strquery = string.Empty;
                                        if (attempts > 2)
                                        {
                                            strquery = "SET SQL_SAFE_UPDATES = 0;update mst_users set islocked=1, LoginAttempt=@attempts where (user_code=@username or email_id =@username) and password=@password;SET SQL_SAFE_UPDATES = 1;";
                                            string status = "You Reached Maximum Attempts. Your account has been locked";
                                            _AuthenticationDetailParameters.status = status;
                                        }
                                        else
                                        {
                                            attempts = attempts + 1;
                                            filter._attempt = attempts.ToString();
                                           
                                            if (attempts == 3)
                                            {
                                                strquery = "SET SQL_SAFE_UPDATES = 0;update mst_users set islocked=1,LoginAttempt=@attempts where user_code=@username or email_id =@username;SET SQL_SAFE_UPDATES = 1;";
                                                string status = "Your Account Locked";
                                                _AuthenticationDetailParameters.status = status;
                                            }
                                            else
                                            {
                                                strquery = "SET SQL_SAFE_UPDATES = 0;update mst_users set LoginAttempt=@attempts where user_code=@username or email_id =@username;SET SQL_SAFE_UPDATES = 1;";
                                                // string status = "Your Email ID or Password is wrong, you have only " + (3 - attempts) + " attempts";
                                                string status = "Your Email ID or Password is wrong";
                                                _AuthenticationDetailParameters.status = status;
                                            }
                                        }
                                        cmd = new MySqlCommand(strquery, conn);
                                        cmd.Parameters.AddWithValue("@username", filter._user_code);
                                        cmd.Parameters.AddWithValue("@password", filter._password);
                                        cmd.Parameters.AddWithValue("@attempts", attempts);
                                        cmd.ExecuteNonQuery();
                                        authuser.Add(_AuthenticationDetailParameters);
                                        //return authuser;
                                    }
                                }
                            }
                        }
                        else
                        {
                            AuthenticationDetailParameters _AuthenticationDetailParameters = new AuthenticationDetailParameters();
                           // string status = "UserName does not exist";
                            string status = "Your Email ID or Password is wrong";
                            _AuthenticationDetailParameters.status = status;
                            authuser.Add(_AuthenticationDetailParameters);
                           // return authuser;
                        }
                    }
                    else
                    {
                        AuthenticationDetailParameters _AuthenticationDetailParameters = new AuthenticationDetailParameters();
                        //string status = "UserName does not exist";
                        string status = "Your Email ID or Password is wrong";
                        _AuthenticationDetailParameters.status = status;
                        authuser.Add(_AuthenticationDetailParameters);
                       // return authuser;
                    }
                }
                return authuser;
            }
            catch (Exception e)
            {
                WritetoLogFile W = new WritetoLogFile();
                W.LogEvent(ConfigurationManager.AppSettings["logfilepath"].ToString(), e.ToString(), true);
                AuthenticationDetailParameters _AuthenticationDetailParameters = new AuthenticationDetailParameters();
                string status = "something went wrong";
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

        public static string ResetPasswordDashboard(AuthenticationParameters filter)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_resetpasswordDashboard", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@_user_code", MySqlDbType.String).Value = filter._user_code;
                    cmd.Parameters.Add("@_password", MySqlDbType.String).Value = filter._password;
                    cmd.Parameters.Add("@_current_password", MySqlDbType.String).Value = filter._CurrentPassword;
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
                            if (status == "success")
                            { 
                                string EmailId = dr.IsNull<string>("email_id");
                            
                            return EmailId;
                            }
                            else
                                return "No Result UnAuthorized";
                        }
                    }
                }
                return "No Result UnAuthorized";

            }
            catch (Exception ex)
            {
                return "System Error";
            }
        }




    }
}
