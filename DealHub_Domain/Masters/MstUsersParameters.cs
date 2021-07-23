using DealHub_Domain.DashBoard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealHub_Domain.Masters
{
    public class MstUsersParameters : Mstcommonparameters
    {
        public int _id { get; set; }
        public string _user_code { get; set; }

        public string _first_name { get; set; }

        public string _last_name { get; set; }

        public string _mobile_no { get; set; }

        public string _email_id { get; set; }

        public int _role_id { get; set; }

        public int _is_cassh_user { get; set; }

        public string _active { get; set; }

        public int _islocked { get; set; }

        public string _mappedverticals { get; set; }

        public string _mappedbranches { get; set; }

        public string _password { get; set; }
    }

    public class MappedUserDetailParameters:Mstcommonparameters
    {
        public int _mapped_User_Id { get; set; }

    }

    public class MappUserBranch:MappedUserDetailParameters
    {
        public int _Branch_Id { get; set; }
    }

    public class MappUserVertical : MappedUserDetailParameters
    {
        public int _Vertical_Id { get; set; }
    }

    public class MstUserDetailParameters : commanmessges
    {
        public ulong _updateduser_id { get; set; }

    }
}
