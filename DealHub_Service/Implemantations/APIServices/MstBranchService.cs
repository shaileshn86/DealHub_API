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
   public class MstBranchService
    {
        public static string GetMstBranch(Mstcommonparameters model)
        {
            return MstBranch.GetMstBranch(model);
        }

        public static List<commanmessges> Update_Mst_Branch(MstBranchParameters model)
        {
            return MstBranch.Update_Mst_Branch(model);
        }
    }
}
