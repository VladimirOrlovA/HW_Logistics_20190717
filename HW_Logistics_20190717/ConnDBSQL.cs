﻿using System;
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

        // Делает создание таблицы в БД получив строку (запроса в БД для создания таблицы) непосредственно из метода класса
        public void CreateTableThroughClass(string strSqlQuery)
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

                    Console.Write("Creating table...");

                    using (SqlCommand command = new SqlCommand(strSqlQuery, connection))
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

        // Делает создание таблицы в БД получив через интерфейс строку запроса в БД для создания таблицы
        public void CreateTable(IWorkWithSQL obj)
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
                    Console.Write("Creating table - ");
                    Console.WriteLine(Convert.ToString(obj.GetType()).Substring(22));

                    using (SqlCommand command = new SqlCommand(obj.CreateTableQuery(), connection))
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

        // Делает запись в БД получив строку (запроса в БД для вставки в таблицу) непосредственно из метода класса
        public void InsertTableThroughClass(string strQuery)
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
                    Console.Write("\nInserting data to table...\n");
                    using (SqlCommand command = new SqlCommand(strQuery, connection))
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

        // Делает запись в БД получив через интерфейс строку запроса в БД для внесения записи в таблицу
        public void InsertTable(IWorkWithSQL obj)
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
                    Console.Write("\nInserting data to table...\n");

                    using (SqlCommand command = new SqlCommand(obj.InsertTableQuery(), connection))
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

        // Выводит все записи из таблицы БД получив строку (запроса в БД для чтения таблицы) непосредственно из метода класса
        public void ViewTableThroughClass(string strSqlQuery)
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

                    using (SqlCommand command = new SqlCommand(strSqlQuery, connection))
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return;
            }
            Console.WriteLine("-------------------------------------------------------------------");
        }

        // Выводит все записи из таблицы БД получив через интерфейс строку запроса в БД для чтения записей из таблицы
        public void ViewTable(IWorkWithSQL obj)
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

                    using (SqlCommand command = new SqlCommand(obj.ViewTableQuery(), connection))
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return;
            }
            Console.WriteLine("-------------------------------------------------------------------");
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

        public void ReadTable(IWorkWithSQL obj)
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

                    using (SqlCommand command = new SqlCommand(obj.ViewTableQuery(), connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            Console.WriteLine();

                            //считываем строки таблицы
                            while (reader.Read())
                            {
                                // считываем поля строки
                                string[] rowStr = new string[reader.FieldCount];
                                for (int i = 0; i != reader.FieldCount; i++)
                                {
                                    rowStr[i] = Convert.ToString(reader.GetValue(i));
                                }
                                Worker tmpWorker = new Worker();

                                tmpWorker.workerID = Convert.ToInt32(rowStr[0]);
                                tmpWorker.LastName = rowStr[1];
                                tmpWorker.FirstName = rowStr[2];
                                tmpWorker.MiddleName = rowStr[3];
                                tmpWorker.birthday = Convert.ToDateTime(rowStr[4]);
                                tmpWorker.inn = Convert.ToInt64(rowStr[5]);
                                tmpWorker.employmentDate = Convert.ToDateTime(rowStr[6]);
                                tmpWorker.position = rowStr[7];
                                tmpWorker.solary = Convert.ToInt32(rowStr[8]);

                                Workers wrks = new Workers();
                                wrks.AddWorker(tmpWorker);
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
            Console.WriteLine("-------------------------------------------------------------------");
        }


    }
}
