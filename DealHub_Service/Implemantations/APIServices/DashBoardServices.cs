using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DealHub_Dal.DashBoard;
using DealHub_Domain.DashBoard;

namespace DealHub_Service.Implemantations.APIServices
{
    public  class DashBoardServices
    {
        public static List<DashBoardDetailsParameters> GetDashBoardData(DashBoardParameters filter)
        {
            return DashBoard.GetDashBoardData(filter);
        }

        public static List<DashBoardDetailsCountParameters> GetDashBoardDataCount(DashBoardParameters filter)
        {
            return DashBoard.GetDashBoardDataCount(filter);
        }
    }
}
