using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealHub_Domain.Masters
{
   public class MstSubSectorsParameters:Mstcommonparameters
    {
        public int _SubSector_Id { get; set; }
        public string _SubSector_Name { get; set; }

        public int _Sector_Id { get; set; }
    }
}
