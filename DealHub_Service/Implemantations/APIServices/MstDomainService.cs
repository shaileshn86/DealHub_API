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
    public class MstDomainService
    {
        public static string GetMstDomains(Mstcommonparameters model)
        {
            return MstDomain. GetMstDomains(model);
        }

        public static List<commanmessges> Update_Mst_Domains(MstDomainParameters model)
        {
            return MstDomain.Update_Mst_Domains(model);
        }

    }
}
