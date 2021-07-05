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
    public class MstFunctionService
    {
        public static string GetMstFunctions(Mstcommonparameters model)
        {
            return MstFunctions.GetMstFunctions(model);
        }

        public static List<commanmessges> UpdateMstFunctions(MstFunctionParameters model)
        {
            return MstFunctions.UpdateMstFunctions(model);
        }
    }
}
