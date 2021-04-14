using DealHub_Domain.MenuBinding;
using MySql.Data.MySqlClient;
using System.Data;
using DealHub_Dal.Extensions;
using System.Collections.Generic;
using System;

namespace DealHub_Dal.MenuBinding
{
   public class MenuBinding:BaseDAL
    {
        public static List<MenuBindingDetailsParameter> GetMenus(MenuBindingParameter filter)
        {
            List<MenuBindingDetailsParameter> menus = new List<MenuBindingDetailsParameter>();
            try
            {
                //sp_auth_user
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_menu_getmenus", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@_user_code", MySqlDbType.String).Value = filter._user_code;
                    
                    conn.Open();
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            MenuBindingDetailsParameter _MenuBindingDetailsParameter = new MenuBindingDetailsParameter();
                            
                            _MenuBindingDetailsParameter.id = dr.IsNull<int>("id");
                            
                            _MenuBindingDetailsParameter.name = dr.IsNull<string>("name");
                            //_MenuBindingDetailsParameter.iconClass = dr.IsNull<string>("iconClass");
                            //if (dr.IsNull<int>("active")==0)
                            //{
                            //    _MenuBindingDetailsParameter.active = false;
                            //}
                            //else
                            //{
                            //    _MenuBindingDetailsParameter.active = true;
                            //}
                         
                            _MenuBindingDetailsParameter.url= dr.IsNull<string>("url");

                            menus.Add(_MenuBindingDetailsParameter);

                        }
                    }
                }
                return menus;
            }
            catch(Exception e)
            {
                return null;

            }
        }
    }
}
