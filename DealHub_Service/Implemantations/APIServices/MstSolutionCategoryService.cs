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
    public class MstSolutionCategoryService
    {
        public static string GetMstSolutionCategory(Mstcommonparameters model)
        {
            return MstSolutionCategory.GetMstSolutionCategory(model);
        }

        public static List<commanmessges> UpdateMstSolutionCategory(MstSolutionCategoryParameters model)
        {
            return MstSolutionCategory.UpdateMstSolutionCategory(model);
        }
    }
}
