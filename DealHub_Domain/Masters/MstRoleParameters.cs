using DealHub_Domain.DashBoard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealHub_Domain.Masters
{
    public class MstRoleParameters:Mstcommonparameters
    {
        public int _id { get; set; }

        public string _role_code { get; set; }

        public string _role_name { get; set; }

        public string _equivalent_cassh_role_name { get; set; }

        public string _active { get; set; }
        public string _Previlege_Id { get; set; }//comma seperated value expected
    }

    public class MstRoleDetailParameters : commanmessges
    {
        public long _role_id { get; set; }

    }
}
