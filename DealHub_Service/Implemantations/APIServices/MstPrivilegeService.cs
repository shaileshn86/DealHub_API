using DealHub_Domain.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DealHub_Dal.Masters;
using DealHub_Domain.DashBoard;
namespace DealHub_Service.Implemantations.APIServices
{
    public class MstPrivilegeService
    {
        public static string GetMstPrivilege(Mstcommonparameters model)
        {
            return MstPrivilege.GetMstPrivilege(model);
        }

        public static List<commanmessges> Update_Mst_Privilege(MstPrivilegeParameters model)
        {
            return MstPrivilege.Update_Mst_Privilege(model);
        }
    }
}
