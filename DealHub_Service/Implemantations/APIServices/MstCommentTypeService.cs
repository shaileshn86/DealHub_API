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
    public class MstCommentTypeService
    {
        public static string GetMstCommentType(Mstcommonparameters model)
        {
            return MstCommentType.GetMstCommentType(model);
        }

        public static List<commanmessges> Update_Mst_CommentType(MstCommentTypeParameters model)
        {
            return MstCommentType.Update_Mst_CommentType(model);
        }
    }
}
