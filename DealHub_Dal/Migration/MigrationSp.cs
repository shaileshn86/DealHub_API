using DealHub_Dal.Extensions;
using DealHub_Domain.DashBoard;
using DealHub_Domain.Migration;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealHub_Dal.Migration
{
    public class MigrationDAL:BaseDAL
    {
        public static DataSet truncatemigrationdata(TruncateMigrationDataParameters model)

        {
            try
            {

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    MySqlDataAdapter DA = new MySqlDataAdapter();
                    MySqlCommand cmd = new MySqlCommand("sp_truncate_migrationdata", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("_user_code", MySqlDbType.String).Value = model._user_code;
                    cmd.Parameters.Add("_batch_no", MySqlDbType.String).Value = model._batch_no;
                    cmd.Parameters.Add("_TotalRecords", MySqlDbType.Int32).Value = model._TotalRecords;
                    cmd.Parameters.Add("_FileName", MySqlDbType.String).Value = model._FileName;


                    DA.SelectCommand = cmd;
                    cmd.Connection = new MySqlConnection(connectionString);
                    DataSet ds = new DataSet();
                    DA.Fill(ds);

                    DataSet rds = ds.GetTableName();

                    return rds ;

                }

            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public static string UpdateMigrationData(DataSet rds)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlTransaction tran = conn.BeginTransaction(System.Data.IsolationLevel.Serializable))
                    {
                        using (MySqlCommand cmd = new MySqlCommand())
                        {
                            cmd.Connection = conn;
                            cmd.Transaction = tran;
                            cmd.CommandText = "SELECT * FROM stage_obf_migration";
                            using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                            {
                                da.UpdateBatchSize = 1000;
                                using (MySqlCommandBuilder cb = new MySqlCommandBuilder(da))
                                {
                                    da.Update(rds.Tables[0]);
                                    tran.Commit();
                                }
                            }
                        }
                    }
                    conn.Close();
                   
                }
                
                
                return "Data Upload Sucess";
            }
            catch
            {
                throw;
            }
            finally
            {
                
            }
        }

        public static List<commanmessges> ValidateMigratedData(MigrationParameters model)
        {
            List<commanmessges> _commanmessges = new List<commanmessges>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_validate_migratedData", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("_user_code", MySqlDbType.String).Value = model._user_code;
                    cmd.Parameters.Add("_batch_no", MySqlDbType.String).Value = model._batch_no;

                    
                    conn.Open();
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            commanmessges _Details = new commanmessges();
                            _Details.status = dr.IsNull<string>("status");
                            _Details.message = dr.IsNull<string>("message");

                            _commanmessges.Add(_Details);
                        }
                    }
                }



                return _commanmessges;
            }
            catch (Exception ex)
            {
                _commanmessges = new List<commanmessges>();

                commanmessges _Details = new commanmessges();
                _Details.status = "Failed";
                _Details.message = "Error in saving parameters";
                _commanmessges.Add(_Details);

                return _commanmessges;
            }
        }

        public static List<commanmessges> insertmigratedData(MigrationParameters model)
        {
            List<commanmessges> _commanmessges = new List<commanmessges>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_insert_migratedData", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("_user_code", MySqlDbType.String).Value = model._user_code;
                    cmd.Parameters.Add("_batch_no", MySqlDbType.String).Value = model._batch_no;


                    conn.Open();
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            commanmessges _Details = new commanmessges();
                            _Details.status = dr.IsNull<string>("status");
                            _Details.message = dr.IsNull<string>("message");

                            _commanmessges.Add(_Details);
                        }
                    }
                }



                return _commanmessges;
            }
            catch (Exception ex)
            {
                _commanmessges = new List<commanmessges>();

                commanmessges _Details = new commanmessges();
                _Details.status = "Failed";
                _Details.message = "Error in saving parameters";
                _commanmessges.Add(_Details);

                return _commanmessges;
            }
        }

    }
}
