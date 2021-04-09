using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealHub_Domain.Helpers
{
    public class ReponseMessage
    {
        public Dictionary<string, object> Record { get; set; }
        public ReponseMessage()
        {


        }


        public ReponseMessage(string MsgNo, string MsgType, string Message, object Validation = null)
        {
            Record = new Dictionary<string, object>();
            // Record.Add(KeyName, Code);
            Record.Add("MSG_SNO", MsgNo);
            Record.Add("MSG_TYP", MsgType);
            Record.Add("MESSAGE", Message);
            if (Validation != null)
                Record.Add("Validation", Validation);
        }
    }
    public class ReponseMessageCollection
    {
        public List<ReponseMessage> Records { get; set; }

    }
}
