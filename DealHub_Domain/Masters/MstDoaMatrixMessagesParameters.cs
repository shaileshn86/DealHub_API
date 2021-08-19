using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealHub_Domain.Masters
{
    public class MstDoaMatrixMessagesParameters : Mstcommonparameters
    {
        public int _DOA_Matrix_Id { get; set; }
        public string _Message { get; set; }

        public string _MessageFor { get; set; }
        public string _Prefix {get; set; }
    }
}
