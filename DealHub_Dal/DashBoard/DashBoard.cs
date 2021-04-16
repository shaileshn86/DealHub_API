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

                            //_DashBoardDetailsParameters.obf_id = dr.IsNull<uint>("obf_id");
                            _DashBoardDetailsParameters.Project_Name = dr.IsNull<string>("projectname");
                            _DashBoardDetailsParameters.Code = dr.IsNull<string>("code");
                            _DashBoardDetailsParameters.Opp_Id = dr.IsNull<string>("oppid");
                            _DashBoardDetailsParameters.Created_On = dr.IsNull<string>("datecreated");
                            _DashBoardDetailsParameters.Created_By = dr.IsNull<string>("createdby");
                            
                           // _DashBoardDetailsParameters.vertical_id = dr.IsNull<uint>("vertical_id");
                            _DashBoardDetailsParameters.vertical = dr.IsNull<string>("vertical");
                            _DashBoardDetailsParameters.Project_Type = dr.IsNull<string>("projecttype");
                            _DashBoardDetailsParameters.Project_Terms = dr.IsNull<string>("projectterms");


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
