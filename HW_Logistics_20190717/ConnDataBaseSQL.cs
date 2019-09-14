using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace HW_Logistics_20190717
{
    class ConnDataBaseSQL : IConnDataBaseSQL
    {
        public static SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        // создаем строку с данными для подключения к БД SQL
        public ConnDataBaseSQL()
        {
            builder.DataSource = @"ASUS_P52F\SQLEXPRESS";
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
                Console.WriteLine("Connecting to SQL Server ...");
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
                        Console.WriteLine("Creating DataBase --- LogisticsOVA");
                        Console.WriteLine("Done. DataBase is created");
                        Console.WriteLine("\n-------------------------------------------------------------------\n");

                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + e.ToString().Substring(48, 43));
                Console.WriteLine("\n\n-------------------------------------------------------------------\n");
            }
        }

        // Вносим данные в БД
        public void SaveData(string sqlQuery)
        {
            if (sqlQuery == null)
            {
                Console.WriteLine("Объект не содержит данных \n");
                return;
            }
            try
            {
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        Console.Write("\nResult: ");
                        Console.Write(command.ExecuteNonQuery());
                        Console.WriteLine(" records");
                        Console.WriteLine("Done.");
                        Console.WriteLine("\n-------------------------------------------------------------------\n\n");
                    }
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.ToString().Substring(0, e.ToString().IndexOf("\n")));
                Console.WriteLine("\n-------------------------------------------------------------------\n\n");
            }
        }

        // Получаем данные из БД
        public List<string> ReadData(string sqlQuery)
        {
            //Console.WriteLine("-------------------------------------------------------------------");
            List<string> rowsStr = new List<string>();
            try
            {
                //Console.Write("Connecting to SQL Server ... \n");
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    //Console.WriteLine("Done.");
                    //Console.WriteLine("ServerVersion: {0}", connection.ServerVersion);
                    //Console.WriteLine("State: {0}", connection.State);

                    // READ table
                    //Console.WriteLine("Reading data from table... \n\n");

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            Console.WriteLine();

                            //считываем строки таблицы
                            while (reader.Read())
                            {
                                // создаем переменную для записи одной строки.
                                // обнуляем эту строку на каждой итерации цикла
                                string oneRowStr = null;
                                // считываем все имеющиеся поля каждой строки таблицы
                                for (int i = 0; i != reader.FieldCount; i++)
                                {
                                    // считали строку таблицы - записали в строку
                                    oneRowStr += reader.GetValue(i) + "; ";
                                }
                                rowsStr.Add(oneRowStr);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine("\n-------------------------------------------------------------------\n\n");
            }
            return rowsStr;
        }

        // Загружает данные из таблиц SQL в объекты программы
        public void LoadData(string sqlQuery)
        {
            Console.WriteLine("\n-------------------------------------------------------------------\n");

            Employees employees = new Employees();

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

                                //return employees;
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
                                Employee tmpEmployee = new Employee();
                                // записываем значение полей в сооответсвующее поле объекта 
                                tmpEmployee.employeeID = Convert.ToInt32(rowStr[0]);
                                tmpEmployee.LastName = rowStr[1];
                                tmpEmployee.FirstName = rowStr[2];
                                tmpEmployee.MiddleName = rowStr[3];
                                tmpEmployee.birthday = Convert.ToDateTime(rowStr[4]);
                                tmpEmployee.iin = Convert.ToInt64(rowStr[5]);
                                tmpEmployee.employmentDate = Convert.ToDateTime(rowStr[6]);
                                tmpEmployee.position = rowStr[7];
                                tmpEmployee.solary = Convert.ToInt32(rowStr[8]);
                                // записываем объект в коллекцию объектов - в лист
                                employees.AddEmployee(tmpEmployee);
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

        public int ReadCountRowInTable(string sqlQuery)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            reader.Read();
                            int count = Convert.ToInt32(reader.GetValue(0));
                            return count;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine("\n-------------------------------------------------------------------\n\n");
                return -1;
            }
        }

    }
}

