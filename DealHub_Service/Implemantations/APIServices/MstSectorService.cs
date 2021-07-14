using DealHub_Domain.DashBoard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DealHub_Dal.Masters;
using DealHub_Domain.Masters;
namespace DealHub_Service.Implemantations.APIServices
{
   public class MstSectorService
    {
        public static string GetMstSector(Mstcommonparameters model)
        {
            return MstSector.GetMstSector(model);
        }

        public static List<commanmessges> UpdateMstSector(MstSectorParameter model)
        {
            return MstSector.UpdateMstSector(model);
        }
    }
}
