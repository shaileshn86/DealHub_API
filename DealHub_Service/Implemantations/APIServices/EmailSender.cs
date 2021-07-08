using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using DealHub_Domain.DashBoard;
using DealHub_Dal.OBF;
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


        public static List<commanmessges> Email_Sending_Details(int _dh_header_id, int _is_shared)
        {
            return EmailSender_DAL.Email_Sending_Details(_dh_header_id,_is_shared);
        }

        public static List<commanmessges> ShareEmail(ShareEmailParameters model)
        {
            return EmailSender_DAL.ShareEmail(model);
        }





    }

    public class SystemNotificationService
    {
        public static string Get_System_Notification(string _user_code)
        {
            return SystemNotificationDAL.Get_System_Notification(_user_code);
        }

        public static List<commanmessges> Update_System_Notification(List<systemnotificationparameters> filters)
        {
            return SystemNotificationDAL.Update_System_Notification(filters);
        }
    }
    
    
    

}
