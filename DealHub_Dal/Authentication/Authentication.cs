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

namespace DealHub_Dal.Authentication
{
    public  class Authentication:BaseDAL
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
                            string status= dr.IsNull<string>("status");
                            _AuthenticationDetailParameters.status = status;
                            if (status!= "success")
                            {

                                _AuthenticationDetailParameters.user_code = "";
                               
                                
                            }
                            else
                            {
                                _AuthenticationDetailParameters.user_code = dr.IsNull<string>("user_code");
                                _AuthenticationDetailParameters.password = dr.IsNull<string>("password");

                            }
                            authuser.Add(_AuthenticationDetailParameters);

                        }
                    }
                }
                    return authuser;
            }
            catch
            {
                AuthenticationDetailParameters _AuthenticationDetailParameters = new AuthenticationDetailParameters();
                string status ="failed with exception in code";
                _AuthenticationDetailParameters.status = status;
                authuser.Add(_AuthenticationDetailParameters);
                return authuser;

            }
        }


        

    }
}
