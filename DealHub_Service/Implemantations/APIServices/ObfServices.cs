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

        public static string GetMastersOBFCreation(string userid)
        {
            return OBF_Creation.GetMastersOBFCreation(userid);
        }

        public static List<SolutionCategory> get_master_solutions(string userid)
        {
            return OBF_Creation.get_master_solutions(userid);
        }

        public static List<SaveAttachementDetailsParameters> SaveServiceSolutionSector(SaveServiceSolutionParameters filter)
        {
            return OBF_Creation.SaveServiceSolutionSector(filter);
        }

        public static List<SaveAttachementDetailsParameters> submit_dh_headers(SubmitOBFParameters filter)
        {
            return OBF_Creation.submit_dh_headers(filter);
        }

        public static List<commanmessges> ApproveRejectObf(ApproveRejectOBFParameter filter)
        {
            return OBF_Creation.ApproveRejectObf(filter);
        }
    }
}
