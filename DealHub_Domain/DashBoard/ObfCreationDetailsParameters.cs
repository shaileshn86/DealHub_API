using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealHub_Domain.DashBoard
{
    public class ObfCreationDetailsParameters:CommonDetailsParameter
    {
        public string Result { get; set; }

     
    }

    public class SaveAttachementDetailsParameters:CommonDetailsParameter
    {
        public string status { get; set; }
        public string message { get; set; }
    }


    


    



}
