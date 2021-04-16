using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DealHub_Domain.DashBoard;
using MySql.Data.MySqlClient;
using System.Data;
using DealHub_Dal.Extensions;

namespace DealHub_Dal.DashBoard
{
    public class DashBoard:BaseDAL
    {
        public static List<DashBoardDetailsParameters> GetDashBoardData(DashBoardParameters filter)
        {
            List<DashBoardDetailsParameters> DashBoardData = new List<DashBoardDetailsParameters>();
            try
            {
                //sp_auth_user
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_GetDashBoardData", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@_user_code", MySqlDbType.String).Value = filter._user_code;
                    conn.Open();
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            DashBoardDetailsParameters _DashBoardDetailsParameters = new DashBoardDetailsParameters();

                            _DashBoardDetailsParameters.obf_id = dr.IsNull<uint>("obf_id");
                            _DashBoardDetailsParameters.project_name = dr.IsNull<string>("projectname");
                            _DashBoardDetailsParameters.code = dr.IsNull<string>("code");
                            _DashBoardDetailsParameters.opp_id = dr.IsNull<string>("oppid");
                            _DashBoardDetailsParameters.created_by = dr.IsNull<string>("createdby");
                            _DashBoardDetailsParameters.date_created = dr.IsNull<string>("datecreated");
                            _DashBoardDetailsParameters.vertical_id = dr.IsNull<uint>("vertical_id");
                            _DashBoardDetailsParameters.vertical = dr.IsNull<string>("vertical");
                            _DashBoardDetailsParameters.project_type = dr.IsNull<string>("projecttype");
                            _DashBoardDetailsParameters.project_terms = dr.IsNull<string>("projectterms");


                            DashBoardData.Add(_DashBoardDetailsParameters);

                        }
                    }
                }
                return DashBoardData;
            }
            catch (Exception e)
            {
                return null;

            }
        }
    }
}
