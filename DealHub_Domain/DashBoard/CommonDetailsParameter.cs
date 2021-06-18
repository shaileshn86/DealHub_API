using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealHub_Domain.DashBoard
{
   public class CommonDetailsParameter
    {

        public uint dh_header_id { get; set; }

        public uint dh_id { get; set; }

        
    }

    public class EntityMainParameter
    {

        public int _dh_header_id { get; set; }

        public int _is_shared { get; set; }


    }

}
