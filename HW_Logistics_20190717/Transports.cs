using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_Logistics_20190717
{
    class Transports
    {
        Transport[] transportsList = new Transport[0];
        // Добавление заказчика в список заказчиков

        public void AddTransport(Transport transport)
        {
            Transport[] tmp = new Transport[transportsList.Length + 1];
            Array.Copy(transportsList, tmp, transportsList.Length);
            Array.Resize(ref transportsList, (transportsList.Length + 1));
            Array.Copy(tmp, transportsList, transportsList.Length);
            transportsList[transportsList.Length - 1] = transport;
        }

        // вывод информации в консоль по содержимому каждого объекта листа
        public void Info()
        {
            Console.WriteLine("\nСписок транспортных средств:");
            
            foreach (var transport in transportsList)
                transport.Info();
        }

        // вывод списка транспорта из таблицы Transports БД SQL
        public void InfoFromSQLtable()
        {
            Console.WriteLine("\nСписок транспортных средств в базе данных:");
            ConnDataBaseSQL db = new ConnDataBaseSQL();
            ViewTable(db);
        }

        // вывод списка маршрутов из таблицы Transports БД SQL по заданному transportID
        public void InfoFromSQLtableOnRouteID(int transportID)
        {
            ConnDataBaseSQL db = new ConnDataBaseSQL();
            ViewTableOnTransportID(db, transportID);
        }

        // вывод списка транспорта из таблицы Transports БД SQL по заданному transportID
        public void ViewTableOnTransportID(IConnDataBaseSQL obj, int transportID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("USE LogisticsOVA; ");
            sb.Append("SELECT * FROM Transports ");
            sb.Append("WHERE transportID=" + transportID);
            string sqlQuery = sb.ToString();

            List<string> rowsStr = obj.ReadData(sqlQuery);
            foreach (string i in rowsStr)
                Console.WriteLine(i);
            //throw new NotImplementedException();
        }

        // Создает таблицу в БД
        public void CreateTable(IConnDataBaseSQL obj)
        {
            Console.WriteLine(@"Creating Table --- ""Transports""");

            StringBuilder sb = new StringBuilder();
            sb.Append("USE LogisticsOVA; ");
            sb.Append("CREATE TABLE Transports (");
            sb.Append(" transportID INT IDENTITY(1,1) NOT NULL PRIMARY KEY, ");
            sb.Append(" transportType NVARCHAR(50), ");
            sb.Append(" carryingСapacity INT, ");
            sb.Append(" bodyVolume INT ");
            sb.Append("); ");
            string sqlQuery = sb.ToString();

            obj.SaveData(sqlQuery);

            //throw new NotImplementedException();
        }

        // Вносит данные в таблицу БД
        public void InsertTable(IConnDataBaseSQL obj)
        {
            Console.WriteLine(@"Insert Data to table ""Transports"" about "
                    + Convert.ToString(this.GetType()).Substring(22));

            StringBuilder sb = new StringBuilder();
            sb.Append("USE LogisticsOVA; ");
            sb.Append("INSERT INTO Transports (transportType, carryingСapacity, " +
                "bodyVolume) VALUES ");

            // объявляем переменную счетчика для подсчета кол-ва итерации, чтобы в запросе на последний
            // ввод строки в таблицу не ставить "," (обеспечение правильности синтаксиса запроса SQL)
            int count = 0;
            string sqlQuery = null;
            foreach (Transport i in transportsList)
            {
                count++;
                sb.Append($"('{i.transportType}', '{i.bodyWeight}', " +
                    $"'{i.bodyVolume}') ");
                if (transportsList.Length != count) sb.Append(", ");
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
            sb.Append("SELECT * FROM Transports ");
            string sqlQuery = sb.ToString();
            obj.ReadData(sqlQuery);
            //throw new NotImplementedException();
        }


    }
}
