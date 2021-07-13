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
    public class MstSolutionsService
    {
        public static string GetMstSolution(Mstcommonparameters model)
        {
            return MstSolutions.GetMstSolution(model);
        }

        public static List<commanmessges> Update_Mst_solution(MstSolutionParameter model)
        {
            return MstSolutions.Update_Mst_solution(model);
        }
    }
}
