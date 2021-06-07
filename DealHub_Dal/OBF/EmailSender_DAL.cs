using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DealHub_Domain.DashBoard;
using MySql.Data.MySqlClient;
using System.Data;
using DealHub_Dal.Extensions;
using Newtonsoft.Json;

namespace DealHub_Dal.OBF
{
   public class EmailSender_DAL : BaseDAL
    {
        public static List<commanmessges> Email_Sending_Details(int _dh_header_id,int _is_shared)
        {
            List<commanmessges> _commanmessges = new List<commanmessges>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    MySqlDataAdapter DA = new MySqlDataAdapter();
                    MySqlCommand cmd = new MySqlCommand("sp_getOBFSummaryData", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@_dh_header_id", MySqlDbType.UInt32).Value = _dh_header_id;
                    cmd.Parameters.Add("@_is_shared", MySqlDbType.UInt32).Value = _is_shared;
                    DA.SelectCommand = cmd;
                    cmd.Connection = new MySqlConnection(connectionString);
                    DataSet ds = new DataSet();
                    DA.Fill(ds);

                    DataSet rds = ds.GetTableName();

                    if (rds.Tables["replaceemailcontent"] != null)
                    {
                        EmailSendingProperties ep = new EmailSendingProperties();
                        ep.SendTo = new List<EmailToCCParameters>();
                        ep.SendCC = new List<EmailToCCParameters>();
                        ep.Attachment = new List<EmailAttachmentParameters>();
                        if (rds.Tables["EmailBody"] != null)
                        {
                            foreach(DataRow Dr in rds.Tables["EmailBody"].Rows)
                            {
                                string email_subject = Dr["email_subject"].ToString();
                                string email_body = Dr["email_body"].ToString();
                                foreach (DataRow DrReplace in rds.Tables["replaceemailcontent"].Rows)
                                {
                                    foreach(DataColumn DC in rds.Tables["replaceemailcontent"].Columns)
                                    {

                                        email_subject = email_subject.Replace("#" + DC.ColumnName.ToString(), DrReplace[DC.ColumnName].ToString()) ;
                                        email_body = email_body.Replace("#" + DC.ColumnName.ToString(), DrReplace[DC.ColumnName].ToString());


                                    }

                                }

                                ep.subject = email_subject;
                                ep.body = email_body;
                                
                            }

                            foreach(DataRow ToRow in ds.Tables["ToEmail"].Rows)
                            {
                                EmailToCCParameters To = new EmailToCCParameters();
                                To.email_id = ToRow["ToEmail"].ToString();
                                ep.SendTo.Add(To);
                            }

                            foreach (DataRow ToRow in ds.Tables["ToEmail"].Rows)
                            {
                                EmailToCCParameters To = new EmailToCCParameters();
                                To.email_id = ToRow["CCEmail"].ToString();
                                ep.SendCC.Add(To);
                            }

                            EmailSender ES = new EmailSender();
                            ES.sendEmail(ep);

                        }
                        else
                        {
                            commanmessges cm = new commanmessges();
                            cm.message = "failure in sending mail";
                            cm.status = "failure";
                            _commanmessges.Add(cm);
                        }
                    }
                    else
                    {
                        commanmessges cm = new commanmessges();
                        cm.message = "failure in sending mail";
                        cm.status = "failure";
                        _commanmessges.Add(cm);
                    }


                        //return JsonConvert.SerializeObject(rds, Formatting.Indented); ;
                        return _commanmessges;
                }



                
            }
            catch(Exception ex)
            {
                return null;
            }

        }

    }
}
