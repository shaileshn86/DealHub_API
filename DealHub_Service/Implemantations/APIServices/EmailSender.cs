using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using DealHub_Domain.DashBoard;

namespace DealHub_Service.Implemantations.APIServices
{
   public class EmailSendingService
    {

        public static void EmailSendTest()
        {
            try
            {
                EmailSendingProperties EP = new EmailSendingProperties();
                EP.SendTo = new List<EmailToCCParameters>();
                EP.Attachment = new List<EmailAttachmentParameters>();
                EmailToCCParameters To = new EmailToCCParameters();
                To.email_id = "naik.shailesh@mahindra.com";
                EP.SendTo.Add(To);
                EP.SendCC = new List<EmailToCCParameters>();
                EmailToCCParameters CC = new EmailToCCParameters();
                CC.email_id = "BHABAL.NIKESH@mahindra.com";
                EP.SendCC.Add(CC);
                EP.subject = "test mail from system";
                EP.body = "Mail send for test do not reply";
                EmailSender ES = new EmailSender();
                ES.sendEmail(EP);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


    }
    
    
    

}
