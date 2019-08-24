using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using Dapper;
using System.Data;

namespace HW_Logistics_20190717
{
    class DBconnDAPPER
    {
        private static readonly string conn_str = "Server = ASUS_P52F\\SQLEXPRESS; Database = InternetShop; user Id = OVA; Password=123";

        private string connStr = "Server = ASUS_P52F\\SQLEXPRESS; Database = InternetShop; user Id = OVA; Password=123";

        public DBconnDAPPER() { }

        public DBconnDAPPER(string connStr)
        {
            this.connStr = connStr;
        }

        public static void Dapper_sel()
        {
            Console.WriteLine("DAPPER");
            using (IDbConnection db = new SqlConnection(conn_str))
            {
                List<ProductCategory> res = db.Query<ProductCategory>("SELECT id, category_name FROM ProductCategory", commandType: CommandType.Text).ToList();
                foreach (var item in res)
                {
                    Console.WriteLine($"{item.id} - {item.category_name}");
                }
            }
        }

        class ProductCategory
        {
            public int id { get; set; }
            public string category_name { get; set; }
        }
    }
}
