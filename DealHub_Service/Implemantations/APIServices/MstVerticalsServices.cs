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
   public class MstVerticalsServices
    {
        public static string GetMstVerticals(Mstcommonparameters model)
        {
            return MstVerticals.GetMstVerticals(model);
        }

        public static List<MstVerticalsDetailParameters> Update_Mst_Verticals(MstVerticalsParameters model)
        {
            return MstVerticals.Update_Mst_Verticals(model);
        }
    }
}
