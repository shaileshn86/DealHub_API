using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealHub_Domain.Masters
{
   public class MstSectorParameter:Mstcommonparameters
    {
         public int _Sector_Id { get; set; }
         public string _Sector_Name { get; set; }
         public string _active { get; set; }
    }
}
