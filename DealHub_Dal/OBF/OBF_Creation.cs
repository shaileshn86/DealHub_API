using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DealHub_Domain.DashBoard;
using MySql.Data.MySqlClient;
using System.Data;
using DealHub_Dal.Extensions;

namespace DealHub_Dal.OBF
{
    public class OBF_Creation: BaseDAL
    {
        public static List<ObfCreationDetailsParameters> ObfCreation(ObfCreationParameters filter)
        {
            List<ObfCreationDetailsParameters> _ObfCreationData = new List<ObfCreationDetailsParameters>();
            try
            {
                //sp_auth_user
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_manage_obfheaders", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    


                    conn.Open();
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ObfCreationDetailsParameters _ObfCreationDetailsParameters = new ObfCreationDetailsParameters();

                            //_DashBoardDetailsParameters.obf_id = dr.IsNull<uint>("obf_id");
                            _ObfCreationDetailsParameters.Result = dr.IsNull<string>("Result");
                           


                            _ObfCreationData.Add(_ObfCreationDetailsParameters);

                        }
                    }
                }
                return _ObfCreationData;
            }
            catch (Exception e)
            {
                return null;

            }
        }
    }
}
