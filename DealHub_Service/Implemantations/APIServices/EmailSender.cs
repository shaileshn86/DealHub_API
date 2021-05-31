using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DealHub_Service.Implemantations.APIServices
{
    class EmailSender
    {
        

        private string FromEmail = ConfigurationManager.AppSettings["FromEmail"].ToString();

        private string EmailPassword = ConfigurationManager.AppSettings["EmailPassword"].ToString();

        private bool EnableSsl = bool.Parse( ConfigurationManager.AppSettings["enablessl"].ToString());

        private int portno = int.Parse(ConfigurationManager.AppSettings["port"].ToString());

        private string HostName = ConfigurationManager.AppSettings["hostname"].ToString();

        public void sendEmail(EmailSendingProperties EP)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(FromEmail);
                    foreach(EmailToCCParameters To in EP.SendTo)
                    {
                        mail.To.Add(To.email_id);
                    }

                    foreach (EmailToCCParameters CC in EP.SendCC)
                    {
                        mail.CC.Add(CC.email_id);
                    }

                    mail.Subject = EP.subject;

                    mail.Body = EP.body;

                    foreach(EmailAttachmentParameters attachment in EP.Attachment)
                    {
                        System.Net.Mail.Attachment attach;
                        attach = new System.Net.Mail.Attachment(attachment.file_path);
                        mail.Attachments.Add(attach);

                    }

                    using (SmtpClient SmtpServer = new SmtpClient(HostName, portno))
                    {
                        SmtpServer.UseDefaultCredentials = false; //Need to overwrite this
                        SmtpServer.Credentials = new System.Net.NetworkCredential(FromEmail, EmailPassword);
                        SmtpServer.EnableSsl = true;
                        SmtpServer.Send(mail);
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }



    }

    class EmailSendingProperties
    {
        public List<EmailToCCParameters> SendTo { get; set; }

        public List<EmailToCCParameters> SendCC { get; set; }

        public List <EmailAttachmentParameters> Attachment { get; set; }

        public string subject { get; set; }

        public string body { get; set; }


    }

    class EmailToCCParameters
    {
        public string email_id { get; set; }
    }

    class EmailAttachmentParameters
    {
        public string file_path { get; set; }
    }
    
    
    

}
