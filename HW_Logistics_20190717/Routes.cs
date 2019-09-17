using System;
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

        public Route[,] arrRoutes = new Route[rowNum, columnNum];

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

        // заполнение массива из файла 
        public void FillArray()
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

            // ===== перепишем данные дистанций между городами из строк в массив =====

            // получим размер массива в файле csv и запишем его новые размеры
            rowNum = fileStr.Length;

            for (int i = 0; i < fileStr[0].Length; i++)
                if (fileStr[0][i] == ',')
                    columnNum++;

            Console.WriteLine($"Строк - {rowNum}");
            Console.WriteLine($"Колонок - {columnNum}");


            // изменяем размер массива для приема данных с файла
            ResizeArray<Route>(ref arrRoutes, rowNum, columnNum);

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

                    arrRoutes[i, j] = route;

                    valueStr = null;
                }
                rowStr = null;
            }
        }

        public void PrintArray()
        {
            for (int i = 0; i < rowNum; i++)
            {
                for (int j = 0; j < columnNum; j++)
                {
                    Console.Write(arrRoutes[i, j].routeID + " = " + arrRoutes[i, j].routeDistance + "км \n");
                }
            }

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
            sb.Append("INSERT INTO Routes (routeID, routeDistance) VALUES ");

            // объявляем переменную счетчика для подсчета кол-ва итерации, чтобы в запросе на последний
            // ввод строки в таблицу не ставить "," (обеспечение правильности синтаксиса запроса SQL)
            int count = 0;
            string sqlQuery = null;

            // === вариант через цикл foreach ===
            foreach (Route i in arrRoutes)
            {
                count++;
                sb.Append($"('{i.routeID}', '{i.routeDistance}') ");
                if (arrRoutes.Length != count) sb.Append(", ");
            }

            // === вариант через цикл for ===
            //for (int i = 0; i < rowNum; i++)
            //{
            //    for (int j = 0; j < columnNum; j++)
            //    {
            //        count++;
            //        sb.Append($"('{arrRoutes[i, j].routeID}', '{arrRoutes[i, j].routeDistance}') ");
            //        if (arrRoutes.Length != count) sb.Append(", ");
            //    }
            //}

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
            string sqlQuery = sb.ToString();

            // получаем массив из строк считанный из таблицы и выводим в консоль
            List<string> rowsStr = obj.ReadData(sqlQuery);
            foreach (string i in rowsStr)
                Console.WriteLine(i);
            //throw new NotImplementedException();
        }

        public void ViewTableOnRouteID(IConnDataBaseSQL obj, string routeID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("USE LogisticsOVA; ");
            sb.Append("SELECT * FROM Routes ");
            sb.Append("WHERE routeID=" + routeID);
            string sqlQuery = sb.ToString();

            List<string> rowsStr = obj.ReadData(sqlQuery);
            foreach (string i in rowsStr)
                Console.WriteLine(i);
            //throw new NotImplementedException();
        }

    }
}

