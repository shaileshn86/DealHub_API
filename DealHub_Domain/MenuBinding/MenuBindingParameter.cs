using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealHub_Domain.MenuBinding
{
    public class MenuBindingParameter:CommonParameters
    {
        [Required]
        public string _user_code { get; set; }
    }
}
