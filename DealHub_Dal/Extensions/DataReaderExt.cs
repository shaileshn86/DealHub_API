using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealHub_Dal.Extensions
{
   public static class DataReaderExt
    {
        public static string IsNullCheck(this IDataReader reader, string Name, string defaultValue = "")
        {
            if (reader[Name] != DBNull.Value)
                return reader[Name].ToString();
            else
                return defaultValue;
        }

        public static T IsNull<T>(this IDataReader reader, string Name)
        {
            if (reader[Name] != DBNull.Value)
                return (T)reader[Name];
            else
                return default(T);
        }


        public static T IsNullColumn<T>(this DataRow row, string Name)
        {
            if (row[Name] != DBNull.Value)
                return (T)row[Name];
            else
                return default(T);
        }

        public static DataSet GetTableName (this DataSet ds)
        {
           try
            {
                for (int i=0; i< ds.Tables.Count;i++)
                {
                    if (ds.Tables[i].Columns.Contains("tablename") )
                    {
                        try
                        {
                            ds.Tables[i].TableName = ds.Tables[i].Rows[0]["tablename"].ToString();
                        }
                        catch(Exception ex)
                        {

                        }
                        
                    }
                }
            }
            catch(Exception ex)
            {

            }

            return ds;
        }
    }
}
