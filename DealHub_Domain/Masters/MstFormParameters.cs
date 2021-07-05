using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealHub_Domain.Masters
{
   public class MstFormParameters:Mstcommonparameters
    {
        public int _id { get; set; }
        public string _form_name { get; set; }

        public string _url { get; set; }

        public string _active { get; set; }

    }
}
