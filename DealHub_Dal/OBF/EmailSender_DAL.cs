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
using System.Web;
using System.IO;

namespace DealHub_Dal.OBF
{
   public class EmailSender_DAL : BaseDAL
    {
        public static List<commanmessges> Email_Sending_Details(ApproveRejectOBFParameter filter)
        {
            List<commanmessges> _commanmessges = new List<commanmessges>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    MySqlDataAdapter DA = new MySqlDataAdapter();
                    MySqlCommand cmd = new MySqlCommand("sp_getEmail_Sending_Details_Actionwise", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@_dh_header_id", MySqlDbType.UInt32).Value = filter._dh_header_id;
                    cmd.Parameters.Add("@_user_id", MySqlDbType.String).Value = filter._created_by;
                    cmd.Parameters.Add("@isapproved", MySqlDbType.UInt32).Value = filter.isapproved;
                    cmd.Parameters.Add("@is_on_hold", MySqlDbType.UInt32).Value = filter.is_on_hold;
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

                            if (rds.Tables["ToEmail"] != null)
                            {
                                foreach (DataRow ToRow in ds.Tables["ToEmail"].Rows)
                                {
                                    EmailToCCParameters To = new EmailToCCParameters();
                                    To.email_id = ToRow["ToEmailId"].ToString();
                                    ep.SendTo.Add(To);
                                }
                            }


                            if (rds.Tables["CCEmail"] != null)
                            {
                                foreach (DataRow ToRow in ds.Tables["CCEmail"].Rows)
                                {
                                    EmailToCCParameters To = new EmailToCCParameters();
                                    To.email_id = ToRow["CcEmailId"].ToString();
                                    ep.SendCC.Add(To);
                                }
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

                        if (rds.Tables["PPLInit_ToEmail"] != null)
                        {
                            EmailSendingProperties ep1 = new EmailSendingProperties();
                            ep1.SendTo = new List<EmailToCCParameters>();
                            ep1.SendCC = new List<EmailToCCParameters>();
                            ep1.Attachment = new List<EmailAttachmentParameters>();
                            if (rds.Tables["PPLInit_EmailBody"] != null)
                            {
                                foreach (DataRow Dr in rds.Tables["PPLInit_EmailBody"].Rows)
                                {
                                    string email_subject = Dr["email_subject"].ToString();
                                    string email_body = Dr["email_body"].ToString();
                                    foreach (DataRow DrReplace in rds.Tables["replaceemailcontent"].Rows)
                                    {
                                        foreach (DataColumn DC in rds.Tables["replaceemailcontent"].Columns)
                                        {

                                            email_subject = email_subject.Replace("#" + DC.ColumnName.ToString(), DrReplace[DC.ColumnName].ToString());
                                            email_body = email_body.Replace("#" + DC.ColumnName.ToString(), DrReplace[DC.ColumnName].ToString());


                                        }

                                    }

                                    ep1.subject = email_subject;
                                    ep1.body = email_body;

                                }

                                if (rds.Tables["PPLInit_ToEmail"] != null)
                                {
                                    foreach (DataRow ToRow in ds.Tables["PPLInit_ToEmail"].Rows)
                                    {
                                        EmailToCCParameters To = new EmailToCCParameters();
                                        To.email_id = ToRow["ToEmailId"].ToString();
                                        ep1.SendTo.Add(To);
                                    }
                                }
                            }

                            EmailSender ES1 = new EmailSender();
                            ES1.sendEmail(ep1);
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
            catch 
            {
                throw;
            }

        }

        public static List<commanmessges> ShareEmail(ShareEmailParameters model)
        {
           
            try
            {
                List<commanmessges> _commanmessges = new List<commanmessges>();


                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    MySqlDataAdapter DA = new MySqlDataAdapter();
                    MySqlCommand cmd = new MySqlCommand("sp_getshareobfdetails", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@_dh_header_id", MySqlDbType.UInt32).Value = model._dh_header_id;
                    cmd.Parameters.Add("@_user_id", MySqlDbType.VarChar).Value = model._user_id;
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
                            foreach (DataRow Dr in rds.Tables["EmailBody"].Rows)
                            {
                                string email_subject = Dr["email_subject"].ToString();
                                string email_body = Dr["email_body"].ToString();
                                foreach (DataRow DrReplace in rds.Tables["replaceemailcontent"].Rows)
                                {
                                    foreach (DataColumn DC in rds.Tables["replaceemailcontent"].Columns)
                                    {

                                        email_subject = email_subject.Replace("#" + DC.ColumnName.ToString(), DrReplace[DC.ColumnName].ToString());
                                        email_body = email_body.Replace("#" + DC.ColumnName.ToString(), DrReplace[DC.ColumnName].ToString());


                                    }

                                }

                                ep.subject = email_subject;
                                ep.body = email_body;

                            }

                            string[] ToEmailId = model._ToEmailId.Split(',');

                            for (int cnt = 0; cnt < ToEmailId.Length; cnt++)
                            {
                                EmailToCCParameters ToParameters = new EmailToCCParameters();
                                ToParameters.email_id = ToEmailId[cnt];
                                ep.SendTo.Add(ToParameters);
                            }

                            if (rds.Tables["CCEmail"] != null)
                            {
                                foreach (DataRow ToRow in ds.Tables["CCEmail"].Rows)
                                {
                                    EmailToCCParameters CC = new EmailToCCParameters();
                                    CC.email_id = ToRow["CcEmailId"].ToString();
                                    ep.SendCC.Add(CC);
                                }
                            }

                            if (rds.Tables["AttachmentDetails"] != null)
                            {
                                //string filepath=  HttpContext.Current.Server.MapPath()

                                foreach (DataRow row in rds.Tables["AttachmentDetails"].Rows)
                                {
                                    string filepath = HttpContext.Current.Server.MapPath(row["filepath"].ToString());
                                    if (File.Exists(filepath))
                                    {
                                        EmailAttachmentParameters Attach = new EmailAttachmentParameters();
                                        Attach.file_path = filepath;
                                        ep.Attachment.Add(Attach);
                                    }
                                }

                            }

                            EmailSender ES1 = new EmailSender();
                            ES1.sendEmail(ep);

                            commanmessges cm = new commanmessges();
                            cm.message = "Success in sharing Email";
                            cm.status = "Success";
                            _commanmessges.Add(cm);


                        }
                        else
                        {
                            commanmessges cm = new commanmessges();
                            cm.message = "failure in sharing Email";
                            cm.status = "failure";
                            _commanmessges.Add(cm);
                        }
                    }
                    else
                    {
                        commanmessges cm = new commanmessges();
                        cm.message = "failure in sharing Email";
                        cm.status = "failure";
                        _commanmessges.Add(cm);

                    }

                }
                    return _commanmessges;
            }
            catch(Exception ex)
            {
                throw;
            }


        }

    }


    public class SystemNotificationDAL:BaseDAL
    {
        public static string Get_System_Notification(string _user_code)
        {
            try
            {

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    MySqlDataAdapter DA = new MySqlDataAdapter();
                    MySqlCommand cmd = new MySqlCommand("sp_get_system_notification", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@_user_code", MySqlDbType.String).Value = _user_code;
                   
                    DA.SelectCommand = cmd;
                    cmd.Connection = new MySqlConnection(connectionString);
                    DataSet ds = new DataSet();
                    DA.Fill(ds);

                    DataSet rds = ds.GetTableName();

                    return JsonConvert.SerializeObject(rds, Formatting.Indented); ;

                }

            }
            catch  
            {
                throw;
            }

        }

        public static List<commanmessges> Update_System_Notification(List<systemnotificationparameters> filters)
        {
            List<commanmessges> _commanmessges = new List<commanmessges>();
            try
            {
                foreach(systemnotificationparameters filter in filters)
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        MySqlCommand cmd = new MySqlCommand("sp_update_system_notification", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@_dh_system_notification_id", MySqlDbType.UInt32).Value = filter._dh_system_notification_id;
                        cmd.Parameters.Add("@_IsRead", MySqlDbType.UInt32).Value = filter._IsRead;
                        cmd.Parameters.Add("@_IsSoftDelete", MySqlDbType.UInt32).Value = filter._IsSoftDelete;

                        conn.Open();
                        using (IDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                commanmessges _Details = new commanmessges();
                                _Details.status = dr.IsNull<string>("status");
                                _Details.message = dr.IsNull<string>("message");

                                _commanmessges.Add(_Details);
                            }
                        }
                    }
                }
                

               

                return _commanmessges;
            }
            catch (Exception ex)
            {
                _commanmessges = new List<commanmessges>();

                commanmessges _Details = new commanmessges();
                _Details.status = "Failed";
                _Details.message = "Error in saving parameters";
                _commanmessges.Add(_Details);

                return _commanmessges;
            }

        }

    }
}
