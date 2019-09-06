using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace HW_Logistics_20190717
{
    class ConnDataBaseSQL: IConnDataBaseSQL
    {
        
        public static SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        // создаем строку с данными для подключения к БД SQL
        public ConnDataBaseSQL()
        {
            builder.DataSource = "ASUS_P52F\\SQLEXPRESS";
            builder.UserID = "OVA";
            builder.Password = "123";
            builder.InitialCatalog = "master";
        }

        // Создаем базу данных для программы
        public void CreareDataBase()
        {
            Console.WriteLine("\n-------------------------------------------------------------------\n\n");
            try
            {
                Console.Write("Connecting to SQL Server ... \n");
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    Console.WriteLine("Done.");
                    Console.WriteLine("ServerVersion: {0}", connection.ServerVersion);
                    Console.WriteLine("State: {0}", connection.State);
                    Console.WriteLine("\n-------------------------------------------------------------------\n");

                    //StringBuilder sb = new StringBuilder();
                    //sb.Append("IF  NOT EXISTS (SELECT 1 FROM sys.databases WHERE name = N'LogisticsOVA') ");
                    //sb.Append("BEGIN ");
                    //sb.Append("CREATE DATABASE [LogisticsOVA] ");
                    //sb.Append("END; ");

                    string strSqlQuery = "CREATE DATABASE [LogisticsOVA]";
                    using (SqlCommand command = new SqlCommand(strSqlQuery, connection))
                    {
                        command.ExecuteNonQuery();
                        Console.Write("Creating DataBase --- LogisticsOVA");
                        Console.WriteLine("Done. DataBase is created");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + e.ToString().Substring(48, 43));
                Console.WriteLine("\n\n-------------------------------------------------------------------\n");
            }
        }

        // Создаем таблицу
        public void CreateTable(string sqlQuery)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    //Console.WriteLine(Convert.ToString(obj.GetType()).Substring(22));

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.ExecuteNonQuery();
                        Console.Write("Creating table...  ");
                        Console.WriteLine("Done. Table is created");
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write(e.ToString().Substring(48, 46));
                //Console.Write(Convert.ToString(obj.GetType()).Substring(22));
                Console.WriteLine("\n\n-------------------------------------------------------------------\n");
            }
        }

        // Вставляем данные в таблицу
        public void InsertTable(string sqlQuery)
        {
            if (sqlQuery == null)
            {
                Console.WriteLine("Объект не содержит данных \n");
                return;
            }
            try
            {
                Console.Write("Connecting to SQL Server ... \n");
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    Console.Write("\nInserting data to table...\n");

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        Console.Write("\nRecords Processed - ");
                        Console.WriteLine(command.ExecuteNonQuery());
                        Console.WriteLine("Done.");
                        Console.WriteLine("\n-------------------------------------------------------------------\n\n");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine("\n-------------------------------------------------------------------\n\n");
            }
        }


        // Выводит все записи из таблицы БД
        public void ViewTable(string sqlQuery)
        {
            Console.WriteLine("-------------------------------------------------------------------");
            try
            {
                Console.Write("Connecting to SQL Server ... \n");
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    Console.WriteLine("Done.");
                    Console.WriteLine("ServerVersion: {0}", connection.ServerVersion);
                    Console.WriteLine("State: {0}", connection.State);

                    // READ table
                    Console.WriteLine("Reading data from table... \n\n");

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            Console.WriteLine();

                            //считываем строки таблицы
                            while (reader.Read())
                            {
                                // считываем поля строки
                                string rowStr = null;
                                for (int i = 0; i != reader.FieldCount; i++)
                                {
                                    rowStr += reader.GetValue(i) + " || ";
                                }
                                Console.WriteLine(rowStr);
                            }
                        }
                    }
                }
                Console.WriteLine("\n-------------------------------------------------------------------\n\n");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine("\n-------------------------------------------------------------------\n\n");
            }
        }

        // метод из примера
        public void ViewTableDEMO()
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
                            Console.WriteLine(reader.FieldCount);
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

        // Загружает данные из таблиц SQL в объекты программы
        public void LoadData(string sqlQuery)
        {
            Console.WriteLine("\n-------------------------------------------------------------------\n");

            Workers wrks = new Workers();

            try
            {
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();

                    //Console.WriteLine("Load data from table " + Convert.ToString(obj.GetType()).Substring(22) + "... \n");

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // проверяем есть ли что считывать из таблицы
                            if (!reader.HasRows)
                            {
                                Console.WriteLine("Table is empty \n");
                                Console.WriteLine("\n-------------------------------------------------------------------\n");

                                //return wrks;
                            }

                            // создаем массив из строк куда запишем данные из полей строки
                            string[] rowStr = new string[reader.FieldCount];

                            //считываем строки таблицы
                            while (reader.Read())
                            {
                                // считываем поля строки
                                for (int i = 0; i != reader.FieldCount; i++)
                                {
                                    rowStr[i] = Convert.ToString(reader.GetValue(i));
                                }

                                // создаем новый объект на каждой итерации
                                Worker tmpWorker = new Worker();
                                // записываем значение полей в сооответсвующее поле объекта 
                                tmpWorker.workerID = Convert.ToInt32(rowStr[0]);
                                tmpWorker.LastName = rowStr[1];
                                tmpWorker.FirstName = rowStr[2];
                                tmpWorker.MiddleName = rowStr[3];
                                tmpWorker.birthday = Convert.ToDateTime(rowStr[4]);
                                tmpWorker.inn = Convert.ToInt64(rowStr[5]);
                                tmpWorker.employmentDate = Convert.ToDateTime(rowStr[6]);
                                tmpWorker.position = rowStr[7];
                                tmpWorker.solary = Convert.ToInt32(rowStr[8]);
                                // записываем объект в коллекцию объектов - в лист
                                wrks.AddWorker(tmpWorker);
                            }
                        }
                    }
                }
                //return wrks;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                //return wrks = null;
            }
        }

    }
}

