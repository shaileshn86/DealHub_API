using MySql.Data.MySqlClient;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealHub_Dal
{
    public class BaseDAL
    {
        public static string ApiConnectionString = ConfigurationManager.ConnectionStrings["OBFConnectionString"].ConnectionString;
        public static string connectionString = ConfigurationManager.ConnectionStrings["OBFConnectionString"].ConnectionString;
        public static MySqlParameter CreateParameters(DbType dbType, object value, string name, ParameterDirection pDirection)
        {
            MySqlParameter mySqlParam = new MySqlParameter();
            mySqlParam.DbType = dbType;
            mySqlParam.Value = value;
            mySqlParam.ParameterName = name;
            mySqlParam.Direction = pDirection;
            return mySqlParam;
        }
    }
}
