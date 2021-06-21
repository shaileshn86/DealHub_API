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

        public static List<ObfCreationDetailsParameters> editcustomercodeandio(ObfCreationParameters filter)
        {
            return OBF_Creation.editcustomercodeandio(filter);
        }

        public static string GetMastersOBFCreation(GetObfMasterParameters model)
        {
            return OBF_Creation.GetMastersOBFCreation(model);
        }

        public static List<SolutionCategory> get_master_solutions(GetObfMasterParameters model)
        {
            return OBF_Creation.get_master_solutions(model);
        }

        public static EditObfParameters get_edit_obf(editobfarguement model)
        {
            return OBF_Creation.getEditObf(model);
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
        public static List<SaveAttachementDetailsParameters> SaveAttachment(List<SaveAttachmentParameter> filter)
        {
            return OBF_Creation.SaveAttachments_OBFSummary(filter);
        }
        public static string GetOBFSummaryDataVersionWise(GetOBFSummaryDataVersionWiseParameters model)
        {
            return OBF_Creation.GetOBFSummaryDataVersionWise(model);
        }
        public static string GetAttachmentDocument(GetOBFSummaryDataVersionWiseParameters model)
        {
            return OBF_Creation.GetAttachmentDocument(model);
        }


    }
}
