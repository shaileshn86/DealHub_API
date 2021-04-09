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
    }
}
