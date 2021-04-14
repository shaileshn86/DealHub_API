using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealHub_Domain
{
    public  class CommonParameters
    {
        [Required]
       public string token { get; set; }
    }
}
