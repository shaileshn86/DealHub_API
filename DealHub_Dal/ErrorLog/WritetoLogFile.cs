using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealHub_Dal.ErrorLog
{
    class WritetoLogFile
    {
        private string sLogFormat;
        private string sErrorTime;

        private static object _lock = new object();

        public WritetoLogFile()
        {
            //this variable used to create log filename format "
            //for example filename : ErrorLogYYYYMMDD
            string sYear = DateTime.Now.Year.ToString();
            string sMonth = DateTime.Now.Month.ToString();
            string sDay = DateTime.Now.Day.ToString();
            string sHour = DateTime.Now.Hour.ToString().PadLeft(2, '0');
            string sMin = DateTime.Now.Minute.ToString().PadLeft(2, '0');
            sErrorTime = sYear + sMonth + sDay + sHour + sMin;
        }

        public void LogEvent(string sPathName, string sErrMsg, bool IsNewPara)
        {
            try
            {
                sLogFormat = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " --> ";
                if (!Directory.Exists(sPathName + @"\ErrorLog")) Directory.CreateDirectory(sPathName + @"\ErrorLog");

                StreamWriter sw = new StreamWriter(sPathName + @"\ErrorLog\" + sErrorTime + ".txt", true);
                lock (_lock)
                {
                    if (IsNewPara) sw.WriteLine("***************************************");
                    sw.WriteLine(sLogFormat + sErrMsg);
                }
                sw.Flush();
                sw.Close();
            }
            catch(Exception ex)
            {
             
            }
            //sLogFormat used to create log files format :
            //yyyy-MM-dd HH:mm:ss -->  Log Message
           
        }
    }
}
