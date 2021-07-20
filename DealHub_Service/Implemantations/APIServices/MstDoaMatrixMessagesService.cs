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
    public class MstDoaMatrixMessagesService
    {
        public static string GetMstDoaMatrixMessages(Mstcommonparameters model)
        {
            return MstDoaMatrixMessages.GetMstDoaMatrixMessages(model);
        }

        public static List<commanmessges> Update_Mst_Doa_Matrix_Messages(MstDoaMatrixMessagesParameters model)
        {
            return MstDoaMatrixMessages.Update_Mst_Doa_Matrix_Messages(model);
        }
    }
}
