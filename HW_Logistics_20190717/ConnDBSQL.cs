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
                    sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine("Done. Table is created");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return;
            }
        }

        public void InsertTable()
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

                    Console.Write("\nCreating sample table with data, press any key to continue...\n");
                    Console.ReadKey(true);
                    StringBuilder sb = new StringBuilder();
                    sb.Append("USE Logistics; ");
                    sb.Append("INSERT INTO Person (lastName, firstName, middleName, birthday, inn) VALUES ");
                    sb.Append("('Orlov', 'Vladimir', 'Aleksandrovich', '1980-07-16', 20660716888), ");
                    sb.Append("('Нестеров', 'Павел', 'Николаевич', '1994-10-12', 2586556655); ");
                    sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        Console.Write("\nRecords Processed - ");
                        Console.WriteLine(command.ExecuteNonQuery());
                        Console.WriteLine("Done.");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return;
            }
        }

        public void ViewTable()
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
                                Console.WriteLine("{0} \t {1} \t {2} \t {3} \t {4} \t {5}",
                                    reader.GetInt32(0),
                                    reader.GetString(1),
                                    reader.GetString(2),
                                    reader.GetString(3),
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
