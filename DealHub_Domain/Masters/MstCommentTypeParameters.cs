using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealHub_Domain.Masters
{
    public class MstCommentTypeParameters:Mstcommonparameters
    {
        public int _comment_type_id { get; set; }

        public string _comment_type { get; set; }
    }
}
