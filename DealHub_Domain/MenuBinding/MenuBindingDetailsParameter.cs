using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealHub_Domain.MenuBinding
{
     public class MenuBindingDetailsParameter
    {
        public int ? id { get; set; }
        public string name { get; set; }

        public string iconClass { get; set; }
        public Boolean active { get; set; }

        public string url { get; set; }

        //public string iconClass { get; set; }
    }
}
