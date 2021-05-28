using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DealHub_Dal.DashBoard;
using DealHub_Domain.DashBoard;
using System.Data;

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
        public static string GetOBFSummaryDetails(int dh_id)
        {
            return DashBoard.GetOBFSummaryDetails(dh_id);
        }
        public static List<timelinehistroy> GetDetailTimelineHistory(int dh_id,int dh_header_id)
        {
            return DashBoard.GetDetailTimelineHistory(dh_id, dh_header_id);
        }

        public static string GetOBFSummaryDetails_version(int dh_id,int dh_header_id)
        {
            return DashBoard.GetOBFSummaryDetails_version(dh_id,dh_header_id);
        }

    }
}
