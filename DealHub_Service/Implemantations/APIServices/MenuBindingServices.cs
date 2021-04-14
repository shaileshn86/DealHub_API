using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DealHub_Dal.MenuBinding;
using DealHub_Domain.MenuBinding;

namespace DealHub_Service.Implemantations.APIServices
{
    public class MenuBindingServices
    {
        public static List<MenuBindingDetailsParameter> GetMenus(MenuBindingParameter filter)
        {
            return MenuBinding.GetMenus(filter);
        }
    }
}
