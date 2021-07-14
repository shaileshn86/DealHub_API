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
    public class MstFormServices
    {
        public static List<commanmessges> Update_Mst_Forms(MstFormParameters model)
        {
            return MstForms.Update_Mst_Forms(model);
        }

        public static string GetMstForms(Mstcommonparameters model)
        {
            return MstForms.GetMstForms(model);
        }
    }
}
