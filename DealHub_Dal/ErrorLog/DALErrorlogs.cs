using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using DealHub_Domain.Entity.Logs;

namespace DealHub_Dal.ErrorLog
{
    public class DALErrorlogs:BaseDAL
    {
        public static string Add(Errorlogs errorlogs)
        {

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    MySqlCommand cmd = new MySqlCommand("SP_Add_Errorlogs", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.Parameters.Add("ErrorLogId", MySqlDbType.Int32).Value = errorlogs.ErrorLogId;
                    cmd.Parameters.Add("Message", MySqlDbType.VarChar).Value = errorlogs.Message;
                    cmd.Parameters.Add("SourceStackTrace", MySqlDbType.VarChar).Value = errorlogs.SourceStackTrace;
                    cmd.Parameters.Add("Parameters", MySqlDbType.VarChar).Value = errorlogs.Parameters;
                    cmd.Parameters.Add("ActionName", MySqlDbType.VarChar).Value = errorlogs.ActionName;
                    cmd.Parameters.Add("PageName", MySqlDbType.VarChar).Value = errorlogs.PageName;
                    cmd.Parameters.Add("URL", MySqlDbType.VarChar).Value = errorlogs.URL;
                    cmd.Parameters.Add("AppId", MySqlDbType.VarChar).Value = errorlogs.AppId;
                    cmd.Parameters.Add("IPAddress", MySqlDbType.VarChar).Value = errorlogs.IpAddress;
                    // cmd.Parameters.Add("CreatedDate", MySqlDbType.DateTime).Value = errorlogs.CreatedDate;
                    cmd.Parameters.Add("CreatedBy", MySqlDbType.VarChar).Value = errorlogs.CreatedBy;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                }

            }
            catch
            {
                throw;
            }
            return "success";
        }
    }
}
