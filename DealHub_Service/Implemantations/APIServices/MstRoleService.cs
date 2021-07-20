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
    public class MstRoleService
    {
        public static string GetMstRole(Mstcommonparameters model)
        {
            return MstRole.GetMstRole(model);
        }

        public static List<MstRoleDetailParameters> Update_Mst_Roles(MstRoleParameters model)
        {
            return MstRole.Update_Mst_Roles(model);
        }
    }
}
