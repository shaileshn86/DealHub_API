using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealHub_Domain.Masters
{
    public class MstSolutionCategoryParameters:Mstcommonparameters
    {
        public int _solutioncategory_Id { get; set; }
        public string _solutioncategory_name { get; set; }
        public string _active { get; set; }
    }
}
