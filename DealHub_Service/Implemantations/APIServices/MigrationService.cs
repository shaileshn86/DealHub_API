using DealHub_Domain.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DealHub_Dal.Migration;
using DealHub_Domain.Migration;
using System.Data;
using DealHub_Domain.DashBoard;

namespace DealHub_Service.Implemantations.APIServices
{
    public class MigrationService
    {
        public static DataSet truncatemigrationdata(TruncateMigrationDataParameters model)
        {
            return MigrationDAL.truncatemigrationdata(model);
        }

        public static string UpdateMigrationData(DataSet rds)
        {
            return MigrationDAL.UpdateMigrationData(rds);
        }

        public static List<commanmessges> ValidateMigratedData(MigrationParameters model)
        {
            return MigrationDAL.ValidateMigratedData(model);
        }

        public static List<commanmessges> insertmigratedData(MigrationParameters model)
        {
            return MigrationDAL.insertmigratedData(model);
        }
    }
}
