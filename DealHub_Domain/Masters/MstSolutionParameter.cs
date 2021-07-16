using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealHub_Domain.Masters
{
    public class MstSolutionParameter:Mstcommonparameters
    {
        public int _Solution_Id { get; set; }

        public string _Solution_Name { get; set; }

        public int _Solutioncategory_Id { get; set; }

        public string _Active { get; set; }

        public int _function_id { get; set; }

        public int _domain_id { get; set; }
    }
}
