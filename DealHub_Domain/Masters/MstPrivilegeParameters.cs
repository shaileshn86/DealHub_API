using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealHub_Domain.Masters
{
   public class MstPrivilegeParameters:Mstcommonparameters
    {
        public int _privilege_Id { get; set; }
        public string _privilege_name { get; set; }
    }
}
