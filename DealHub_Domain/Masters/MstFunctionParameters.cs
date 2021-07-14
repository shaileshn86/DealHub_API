using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealHub_Domain.Masters
{
    public class MstFunctionParameters:Mstcommonparameters
    {
        public int _function_id { get; set; }
        public string _function_code { get; set; }
        public string _function_name { get; set; }
        public int _active { get; set; }
    }
}
