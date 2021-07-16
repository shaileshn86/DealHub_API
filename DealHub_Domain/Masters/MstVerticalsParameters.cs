using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DealHub_Domain.DashBoard;

namespace DealHub_Domain.Masters
{
    public class MstVerticalsParameters:Mstcommonparameters
    {
        public int _vertical_id { get; set; }
        public string _vertical_code { get; set; }

        public string _vertical_name { get; set; }

        public int _function_id { get; set; }
        public string _active { get; set; }
        public string _Sector_Id { get; set; }//comma seperated value expected
    }

    public class MstVerticalsDetailParameters : commanmessges
    {
        public ulong _vertical_id { get; set; }
       
    }


}
