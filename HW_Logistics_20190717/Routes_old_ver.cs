using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_Logistics_20190717
{
    class Routes
    {
        public Route[] routesList = new Route[0];
        public int routesID { get; }
        public Routes() { }

        public void addRoute(Route route)
        {
            Route[] tmp = new Route[routesList.Length + 1];
            Array.Copy(routesList, tmp, routesList.Length);
            Array.Resize(ref routesList, (routesList.Length + 1));
            Array.Copy(tmp, routesList, routesList.Length);
            routesList[routesList.Length - 1] = route;
        }

        // вывод информации в консоль по содержимому каждого объекта листа
        public void Info()
        {
            Console.WriteLine("\nИмеющиеся маршруты:");

            foreach (Route i in routesList)
                i.Info();
        }

        // вывод списка маршрутов из таблицы Routes БД SQL
        public void InfoFromSQLtable()
        {
            Console.WriteLine("\nИмеющиеся маршруты в базе данных:");
            ConnDataBaseSQL db = new ConnDataBaseSQL();
            ViewTable(db);
        }

        // вывод списка маршрутов из таблицы Routes БД SQL по заданному routeID
        public void InfoFromSQLtableOnRouteID(int routeID)
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
            sb.Append(" routeID INT IDENTITY(1,1) NOT NULL PRIMARY KEY, ");
            sb.Append(" routeStart NVARCHAR(50), ");
            sb.Append(" routeEnd NVARCHAR(50), ");
            sb.Append(" routeLength INT DEFAULT NULL ");
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
            sb.Append("INSERT INTO Routes (routeStart, routeEnd, routeLength) VALUES ");

            // объявляем переменную счетчика для подсчета кол-ва итерации, чтобы в запросе на последний
            // ввод строки в таблицу не ставить "," (обеспечение правильности синтаксиса запроса SQL)
            int count = 0;
            string sqlQuery = null;
            foreach (Route i in routesList)
            {
                count++;
                sb.Append($"('{i.routeStart}', '{i.routeEnd}', '{i.routeLength}') ");
                if (routesList.Length != count) sb.Append(", ");
            }

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

        public void ViewTableOnRouteID(IConnDataBaseSQL obj, int routeID)
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
