using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

    public class testforregularexpression
    {
        [RegularExpression(@"([a-zA-Z0-9@#$&;|,.?-_]+)", ErrorMessage = "not valid expression")]
        public string testmail { get; set; }
    }
}
