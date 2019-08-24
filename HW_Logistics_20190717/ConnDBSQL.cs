using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Common;

namespace HW_Logistics_20190717
{
    class ConnDBSQL
    {
        public static SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        public ConnDBSQL()
        {
            builder.DataSource = "ASUS_P52F\\SQLEXPRESS";
            builder.UserID = "OVA";
            builder.Password = "123";
            builder.InitialCatalog = "master";
        }

        public void CreateTable()
        {
            try
            {
                Console.Write("Connecting to SQL Server ... \n");
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    Console.WriteLine("Done.");
                    Console.WriteLine("ServerVersion: {0}", connection.ServerVersion);
                    Console.WriteLine("State: {0}", connection.State);

                    String sql = null;

                    // Create a Table and insert some sample data
                    Console.Write("Creating sample table with data, press any key to continue...");
                    Console.ReadKey(true);
                    StringBuilder sb = new StringBuilder();
                    sb.Append("USE Logistics; ");
                    sb.Append("CREATE TABLE Person ( ");
                    sb.Append(" Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY, ");
                    sb.Append(" lastName NVARCHAR(50), ");
                    sb.Append(" firstName NVARCHAR(50), ");
                    sb.Append(" middleName NVARCHAR(50), ");
                    sb.Append(" birthday DATE, ");
                    sb.Append(" inn DECIMAL ");
                    sb.Append("); ");
                    sb.Append("INSERT INTO Person (lastName, firstName, middleName, birthday, inn) VALUES ");
                    sb.Append("('Orlov', 'Vladimir', 'Aleksandrovich', '1980-07-16', 20660716888); ");
                    sql = sb.ToString();


                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        // Выполняет инструкцию по соединению и возвращает количество затронутых строк. 
                        // Для операторов UPDATE, INSERT и DELETE возвращаемое значение представляет собой 
                        // количество строк, на которые влияет команда.
                        command.ExecuteNonQuery();
                        Console.WriteLine("\n\n");
                        Console.WriteLine("ServerVersion: {0}", connection.ServerVersion);
                        Console.WriteLine("State: {0}", connection.State);
                        Console.WriteLine("\n");
                        Console.WriteLine("Done. Table Person is created");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void ReadTable()
        {
            try
            {
                Console.Write("Connecting to SQL Server ... \n");
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    Console.WriteLine("Done.");
                    Console.WriteLine("ServerVersion: {0}", connection.ServerVersion);
                    Console.WriteLine("State: {0}", connection.State);

                    String sql = null;

                    // READ demo
                    Console.WriteLine("Reading data from table, press any key to continue...");
                    Console.ReadKey(true);
                    StringBuilder sb = new StringBuilder();
                    sb.Append("USE Logistics; ");
                    sb.Append("SELECT * FROM Person;");
                    sql = sb.ToString();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            Console.WriteLine();
                            while (reader.Read())
                            {
                                Console.WriteLine("{0} {1} {2} {3} {4} {5}",
                                    reader.GetValue(0),
                                    reader.GetValue(1),
                                    reader.GetValue(2),
                                    reader.GetValue(3),
                                    reader.GetValue(4),
                                    reader.GetValue(5));
                            }
                        }
                    }
                } 
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return;
            }
        }
    }
}
