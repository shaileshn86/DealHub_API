using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealHub_Domain.Masters
{
    public   class MstDomainParameters:Mstcommonparameters
    {
        public int _domain_id { get; set; }
        public string _domain_code { get; set; }
        public string _domain_name { get; set; }
        public int     _active { get; set; }

    }

    public class Mstcommonparameters
    {
        public string _user_id { get; set; }
}

 
}
