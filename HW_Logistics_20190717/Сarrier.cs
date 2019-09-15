using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_Logistics_20190717
{
    class Carrier : Person
    {
        private static int carriersCount = 0;
        public int carrierID { get; }
        public int[] carrierRoutesIdList = new int[0];
        public int[] carrierTransportsIdList = new int[0];

        public Carrier() { }

        public Carrier(string lastName, string firstName, string middleName, DateTime birthday, long iin)
            : base(lastName, firstName, middleName, birthday, iin)
        {
            carrierID = ++carriersCount;
        }

        // Создание маршрутного листа перевозчику
        public void AddRouteToСarrierRouteList(IConnDataBaseSQL obj, int routeID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("USE LogisticsOVA; ");
            sb.Append("SELECT routeID FROM [Routes]");
            string sqlQuery = sb.ToString();
            List<string> rowsStr = obj.ReadData(sqlQuery);

            //проверяем есть ли в списках маршрутов маршрут под номером,
            //который мы хотим добавить перевозчику
            try
            {
                //foreach (string i in rowsStr)
                //    Console.WriteLine((Convert.ToInt32(i.Substring(0, i.IndexOf(';')))));

                int countMatch = 0;
                foreach (string i in rowsStr)
                    if ((Convert.ToInt32(i.Substring(0, i.IndexOf(';')))) == routeID)
                        countMatch++;
                if (countMatch == 0)
                    throw new Exception("Ошибка: маршрут с заданым номером не существует.");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return;
            }

            Console.WriteLine($"маршрут за номером {routeID} добавлен");

            int[] tmp = new int[carrierRoutesIdList.Length + 1];
            Array.Copy(carrierRoutesIdList, tmp, carrierRoutesIdList.Length);
            Array.Resize(ref carrierRoutesIdList, (carrierRoutesIdList.Length + 1));
            Array.Copy(tmp, carrierRoutesIdList, carrierRoutesIdList.Length);
            carrierRoutesIdList[carrierRoutesIdList.Length - 1] = routeID;
        }

        // Создание списка транспортных средств перевозчика
        public void AddTransportToTransportsList(IConnDataBaseSQL obj, int transportID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("USE LogisticsOVA; ");
            sb.Append("SELECT transportID FROM [Transports]");
            string sqlQuery = sb.ToString();
            List<string> rowsStr = obj.ReadData(sqlQuery);

            //проверяем есть ли в списках транспортных средств транспорт под номером,
            //который мы хотим добавить перевозчику
            try
            {
                //foreach (string i in rowsStr)
                //    Console.WriteLine((Convert.ToInt32(i.Substring(0, i.IndexOf(';')))));

                int countMatch = 0;
                foreach (string i in rowsStr)
                    if ((Convert.ToInt32(i.Substring(0, i.IndexOf(';')))) == transportID)
                        countMatch++;
                if (countMatch == 0)
                    throw new Exception("Ошибка: транспорта с заданым номером не существует.");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return;
            }

            Console.WriteLine($"транспорт за номером {transportID} добавлен");

            int[] tmp = new int[carrierTransportsIdList.Length + 1];
            Array.Copy(carrierTransportsIdList, tmp, carrierTransportsIdList.Length);
            Array.Resize(ref carrierTransportsIdList, (carrierTransportsIdList.Length + 1));
            Array.Copy(tmp, carrierTransportsIdList, carrierTransportsIdList.Length);
            carrierTransportsIdList[carrierTransportsIdList.Length - 1] = transportID;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override void Info()
        {
            Console.WriteLine("\n----------------- Информация о перевозчике ----------------\n\n");
            Console.WriteLine("Номер перевозчика ------- " + carrierID);
            InfoPerson();
            Console.Write("Номера маршрутов -------- ");

            foreach (int i in carrierRoutesIdList)
                Console.Write(i + " ");
            Console.WriteLine();

            Console.Write("Номера машин ------------ ");

            foreach (int i in carrierTransportsIdList)
                Console.Write(i + " ");
            Console.WriteLine("\n");



            Routes routes = new Routes();
            foreach (int i in carrierRoutesIdList)
                routes.InfoFromSQLtableOnRouteID(i);


            Transports transports = new Transports();
            foreach (int i in carrierTransportsIdList)
                transports.InfoFromSQLtableOnRouteID(i);

            Console.WriteLine("\n-----------------------------------------------------------\n\n");
        }

        // Вставляет данные в таблицу БД
        public void InsertTable(IConnDataBaseSQL obj)
        {
            Console.WriteLine(@"Insert Data to table ""Carriers"" about "
               + Convert.ToString(this.GetType()).Substring(22));

            StringBuilder sb = new StringBuilder();
            sb.Append("USE LogisticsOVA; ");
            sb.Append("INSERT INTO Carriers (lastName, firstName, middleName, birthday, iin, " +
                "carrierRouteID, carrierTransportsIdList) VALUES ");
            sb.Append($"('{lastName}', '{firstName}', '{middleName}', '{birthday.Year}-{birthday.Month}-{birthday.Day}', '{iin}'," +
                $" '{carrierRoutesIdList}', '{carrierTransportsIdList}') ");
            string sqlQuery = sb.ToString();

            obj.SaveData(sqlQuery);
        }

        // Устнавливает счетчик на цифру кол-ва ранее созданных объектов
        public void SetCountObj(IConnDataBaseSQL obj)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("USE LogisticsOVA; ");
            sb.Append("SELECT COUNT(1) FROM Carriers");
            string sqlQuery = sb.ToString();

            carriersCount = obj.ReadCountRowInTable(sqlQuery);
            Console.WriteLine("\n-------------------------------------------------------------------");
            Console.WriteLine(@"Кол-во строк в tаблице ""Carriers"" - " + carriersCount);
            Console.WriteLine("\n-------------------------------------------------------------------");
        }

    }
}
