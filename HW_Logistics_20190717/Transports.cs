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

        // Выводит информацию в консоль по содержимому каждого объекта листа
        public void Info()
        {
            for (int i = 0; i != transportsList.Length; i++)
                transportsList[i].Info();

            //foreach (var transport in transportsList)
            //    worker.Info();
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
            sb.Append(" bodyVolume INT, ");
            sb.Append(" carrierTransportID INT DEFAULT NULL ");
            sb.Append("); ");
            string sqlQuery = sb.ToString();

            obj.SaveData(sqlQuery);

            //throw new NotImplementedException();
        }

        // Вставляет данные в таблицу БД
        public void InsertTable(IConnDataBaseSQL obj)
        {
            Console.WriteLine(@"Insert Data to table ""Transports"" about "
                    + Convert.ToString(this.GetType()).Substring(22));

            StringBuilder sb = new StringBuilder();
            sb.Append("USE LogisticsOVA; ");
            sb.Append("INSERT INTO Transports (transportType, carryingСapacity, " +
                "bodyVolume, carrierTransportID ) VALUES ");

            // объявляем переменную счетчика для подсчета кол-ва итерации, чтобы в запросе на последний
            // ввод строки в таблицу не ставить "," (обеспечение правильности синтаксиса запроса SQL)
            int count = 0;
            string sqlQuery = null;
            foreach (Transport i in transportsList)
            {
                count++;
                sb.Append($"('{i.transportType}', '{i.carryingСapacity}', " +
                    $"'{i.bodyVolume}, '{i.carrierTransportID}') ");
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
