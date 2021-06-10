using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealHub_Domain.DashBoard
{
    public class DashBoardParameters
    {
        public string _user_code { get; set; }
    }


    public class systemnotificationparameters
    {
        public int _dh_system_notification_id { get; set; }
        public int _IsRead { get; set; }

        public int _IsSoftDelete { get; set; }
    }
}
