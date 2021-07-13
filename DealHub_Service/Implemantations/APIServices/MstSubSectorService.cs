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
    public class MstSubSectorService
    {
        public static string GetMstSubsector(Mstcommonparameters model)
        {
          return  MstSubSector.GetMstSubsector(model);
        }

        public static List<commanmessges> Update_Mst_Subsector(MstSubSectorsParameters model)
        {
            return MstSubSector.Update_Mst_Subsector(model);
        }
    }
}
