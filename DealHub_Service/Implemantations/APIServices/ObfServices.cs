using DealHub_Domain.DashBoard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DealHub_Dal.OBF;
namespace DealHub_Service.Implemantations.APIServices

{
   public class ObfServices
    {
        public static List<ObfCreationDetailsParameters> ObfCreation(ObfCreationParameters filter)
        {
            return OBF_Creation.ObfCreation(filter);
        }
    }
}
