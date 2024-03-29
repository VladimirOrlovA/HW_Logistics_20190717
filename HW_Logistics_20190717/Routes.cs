﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HW_Logistics_20190717
{
    class Routes
    {
        public static int rowNum = 1;
        public static int columnNum = 1;
        public string[] fileStr = new string[0];

        public Route[,] arrRoutes = new Route[0, 0];
        string[] arrayCityName = new string[0];

        // изменение размера двумерного массива
        void ResizeArray<T>(ref T[,] original, int newRowNum, int newColumnNum)
        {
            var newArray = new T[newRowNum, newColumnNum];
            int columnCount = original.GetLength(1);
            int columnCount2 = newRowNum;
            int columns = original.GetUpperBound(0);
            for (int co = 0; co <= columns; co++)
                Array.Copy(original, co * columnCount, newArray, co * columnCount2, columnCount);
            original = newArray;
        }

        // изменение размера одномерного массива
        void ResizeArray1D<T>(ref T[] original, int newColumnNum)
        {
            Array.Resize(ref original, newColumnNum);
        }

        // заполнение строкового массива из файла 
        void ReadFileToArrString()
        {
            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader(@"C:\Users\Orlov\source\repos\HW_Logistics_20190717\Carriers routes\DistancesBetweenCitiesOfKazakhstan.csv"))
                {
                    string line;
                    // Read and display lines from the file until the end of 
                    // the file is reached.
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] tmp = new string[fileStr.Length + 1];
                        Array.Copy(fileStr, tmp, fileStr.Length);
                        Array.Resize(ref fileStr, (fileStr.Length + 1));
                        Array.Copy(tmp, fileStr, fileStr.Length);
                        fileStr[fileStr.Length - 1] = line;
                    }
                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            //foreach (string i in fileStr)
            //    Console.WriteLine(i);
        }

        // заполняем массив маршрутов
        public void FillArrayRoutes()
        {
            // заполненяем строковый массив строками из файла
            ReadFileToArrString();

            // ===== переписываем данные дистанций между городами из строк в массив =====

            // последнюю строку не учитываем, т.к. в ней внесены названия городов
            rowNum = fileStr.Length - 1;

            for (int i = 0; i < fileStr[0].Length; i++)
                if (fileStr[0][i] == ',')
                    columnNum++;

            //Console.WriteLine($"Строк - {rowNum}");
            //Console.WriteLine($"Колонок - {columnNum}");

            // изменяем размер массива для приема данных с файла
            ResizeArray<Route>(ref arrRoutes, rowNum, columnNum);

            // получаем массив из названий городов
            string[] arrayCityName = GetArrayCityName();

            // запись данных в массив
            string rowStr = null;

            for (int i = 0; i < rowNum; i++)
            {
                rowStr = fileStr[i];

                int startIndex = 0;
                string valueStr = null;
                for (int j = 0; j < columnNum; j++)
                {
                    rowStr = rowStr.Substring(startIndex);
                    startIndex = 0;
                    if (rowStr.IndexOf(',') > 0)
                    {
                        valueStr = rowStr.Substring(startIndex, rowStr.IndexOf(','));
                        startIndex = rowStr.IndexOf(',') + 1;
                    }
                    else valueStr = rowStr;

                    Route route = new Route();
                    route.routeDistance = Convert.ToInt32(valueStr);
                    route.routeID = Convert.ToString(i) + "-" + Convert.ToString(j);

                    // заполнить routeStart и routeEnd
                    route.routeStart = arrayCityName[i];
                    route.routeEnd = arrayCityName[j];

                    arrRoutes[i, j] = route;
                    //route.Info();
                    valueStr = null;
                }
                rowStr = null;
            }
        }

        // выводим массив маршрутов в консоль
        public void PrintArrayRoutes()
        {
            for (int i = 0; i < rowNum; i++)
            {
                for (int j = 0; j < columnNum; j++)
                {
                    Console.Write(arrRoutes[i, j].routeID + " = " + arrRoutes[i, j].routeDistance + "км " + "\t");
                }
                Console.WriteLine();
            }

        }

        // заполняем массив названий городов
        void FillArrayCityName()
        {
            ResizeArray1D<string>(ref arrayCityName, columnNum);
            string CityNameStr = fileStr[rowNum];
            int startIndex = 0;
            string valueStr = null;
            for (int i = 0; i < columnNum; i++)
            {
                CityNameStr = CityNameStr.Substring(startIndex);
                startIndex = 0;
                if (CityNameStr.IndexOf(',') > 0)
                {
                    valueStr = CityNameStr.Substring(startIndex, CityNameStr.IndexOf(','));
                    startIndex = CityNameStr.IndexOf(',') + 1;
                }
                else valueStr = CityNameStr;

                arrayCityName[i] = valueStr;
                //Console.WriteLine(valueStr);
            }
        }

        // возвращаем массив названий городов
        public string[] GetArrayCityName()
        {
            FillArrayCityName();
            return arrayCityName;
        }

        // возвращаем строку названия города из массива городов по номеру города
        public string GetCityName(int cityNumber)
        {
            FillArrayCityName();
            string cityName = arrayCityName[cityNumber];
            return cityName;
        }

        // возвращаем номер города из массива городов по названию города
        public int GetCityNumber(string cityName)
        {
            int cityNumber = -1;
            FillArrayCityName();

            for (int i = 0; i < arrayCityName.Length; i++)
                if (cityName == arrayCityName[i])
                {
                    cityNumber = i;
                    break;
                }

            if (cityNumber == -1)
            {
                cityNumber = 0;
                Console.WriteLine("\nВведенного города нет в системе. По умолчанию установлен '0'");
            }

            return cityNumber;
        }
        // вывод списка маршрутов из таблицы Routes БД SQL
        public void InfoFromSQLtable()
        {
            Console.WriteLine("\nИмеющиеся маршруты в базе данных:");
            ConnDataBaseSQL db = new ConnDataBaseSQL();
            ViewTable(db);
        }

        // вывод списка маршрутов из таблицы Routes БД SQL по заданному routeID
        public void InfoFromSQLtableOnRouteID(string routeID)
        {
            ConnDataBaseSQL db = new ConnDataBaseSQL();
            ViewTableOnRouteID(db, routeID);
        }

        // Создает таблицу в БД
        public void CreateTable(IConnDataBaseSQL obj)
        {
            Console.WriteLine(@"Creating Table --- ""Routes""");

            StringBuilder sb = new StringBuilder();
            sb.Append("USE LogisticsOVA; ");
            sb.Append("CREATE TABLE Routes (");
            sb.Append(" routeID NVARCHAR(10) NOT NULL PRIMARY KEY, ");
            sb.Append(" routeStart NVARCHAR(25), ");
            sb.Append(" routeEnd NVARCHAR(25), ");
            sb.Append(" routeDistance INT DEFAULT NULL ");
            sb.Append("); ");
            string sqlQuery = sb.ToString();

            obj.SaveData(sqlQuery);

            //throw new NotImplementedException();
        }

        // Вносит данные в таблицу БД
        public void InsertTable(IConnDataBaseSQL obj)
        {
            Console.WriteLine(@"Insert Data to table ""Routes"" about "
                    + Convert.ToString(this.GetType()).Substring(22));

            StringBuilder sb = new StringBuilder();
            sb.Append("USE LogisticsOVA; ");
            sb.Append("INSERT INTO Routes (routeID, routeStart, routeEnd, routeDistance) VALUES ");

            // объявляем переменную счетчика для подсчета кол-ва итерации, чтобы в запросе на последний
            // ввод строки в таблицу не ставить "," (обеспечение правильности синтаксиса запроса SQL)
            int count = 0;
            string sqlQuery = null;

            // === вариант через цикл foreach ===
            foreach (Route i in arrRoutes)
            {
                count++;
                sb.Append($"('{i.routeID}', '{i.routeStart}', '{i.routeEnd}', '{i.routeDistance}') ");
                if (arrRoutes.Length != count) sb.Append(", ");
            }

            // === вариант через цикл for ===
            //for (int i = 0; i < rowNum; i++)
            //    {
            //        for (int j = 0; j < columnNum; j++)
            //        {
            //            count++;
            //            sb.Append($"('{arrRoutes[i, j].routeID}', '{arrRoutes[i, j].routeStart}', '{arrRoutes[i, j].routeEnd}', '{arrRoutes[i, j].routeDistance}') ");
            //            if (arrRoutes.Length != count) sb.Append(", ");
            //        }
            //    }

            sqlQuery = sb.ToString();
            obj.SaveData(sqlQuery);
            //throw new NotImplementedException();
        }

        // Выполняет вывод таблицы в консоль
        public void ViewTable(IConnDataBaseSQL obj)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("USE LogisticsOVA; ");
            sb.Append("SELECT * FROM Routes ");
            sb.Append("ORDER BY routeStart ");
            string sqlQuery = sb.ToString();

            // получаем массив из строк считанный из таблицы и выводим в консоль
            List<string> rowsStr = obj.ReadData(sqlQuery);

            for (int i = 0; i != rowsStr.Count; i++)
            {
                Console.Write(rowsStr[i] + "\t\t\t\t");
                if (i % 3 == 0) Console.WriteLine();
            }
            //throw new NotImplementedException();
        }

        // вывод в строку данных маршрута из таблицы Routes БД SQL по заданному routeID
        public string ViewTableOnRouteID(IConnDataBaseSQL obj, string routeID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("USE LogisticsOVA; ");
            sb.Append("SELECT * FROM Routes ");
            sb.Append($"WHERE routeID like '{routeID}'");
            string sqlQuery = sb.ToString();
            List<string> rowsStr = obj.ReadData(sqlQuery);
            return rowsStr[0];
            //throw new NotImplementedException();
        }

    }
}

