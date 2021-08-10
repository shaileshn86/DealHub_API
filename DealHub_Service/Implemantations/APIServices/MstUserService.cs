using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DealHub_Dal.Masters;
using DealHub_Domain.Masters;
using DealHub_Domain.DashBoard;

namespace DealHub_Service.Implemantations.APIServices
{
    public class MstUserService
    {
        public static string GetMstUsers(Mstcommonparameters model)
        {
            return MstUsers.GetMstUsers(model);
        }

        public static List<MstUserDetailParameters> Update_Mst_Users(MstUsersParameters model)
        {
            return MstUsers.Update_Mst_Users(model);
        }

        public static List<MstUserDetailParameters> Update_Mst_Users_Dashboard(MstUpdateUsersParameters model)
        {
            return MstUsers.Update_Mst_Users_Dashboard(model);
        }
    }
}
