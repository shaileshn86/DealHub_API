using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealHub_Domain.Migration
{
   public class MigrationParameters
    {
       public string _user_code { get; set; }
       public string _batch_no { get; set; }
    }

    public class TruncateMigrationDataParameters
    {
        public string _user_code { get; set; }
        public string _batch_no { get; set; }

        public int _TotalRecords { get; set; }

        public string _FileName { get; set; }

    }
}
