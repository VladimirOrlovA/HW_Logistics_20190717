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
        public string[] carrierRoutesIdList = new string[0];
        public int[] carrierTransportsIdList = new int[0];
        public bool foundRouteAB { get; set; }
        public int distanceABforOrder { get; set; }
        public double costABforOrder { get; set; }
        public double dayABforOrder { get; set; }

        public Carrier() { }

        public Carrier(string lastName, string firstName, string middleName, DateTime birthday, long iin)
            : base(lastName, firstName, middleName, birthday, iin)
        {
            carrierID = ++carriersCount;
        }

        // Создание маршрутного листа перевозчику по запросу routeID в БД
        public void AddRouteToСarrierRouteListByRouteID(IConnDataBaseSQL obj, string routeID)
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
                int countMatch = 0;
                foreach (string i in rowsStr)
                    if (i.Substring(0, i.IndexOf(';')) == routeID)
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

        // Создание маршрутного листа перевозчику по запросу города A в город B в БД
        public void AddRouteToСarrierRouteListByCityNamesAB(IConnDataBaseSQL obj, string routeStart, string routeEnd)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("USE LogisticsOVA; ");
            sb.Append("SELECT * FROM [Routes]");
            sb.Append($"WHERE routeStart like '{routeStart}' and routeEnd like '{routeEnd}' ");
            string sqlQuery = sb.ToString();
            List<string> rowsStr = obj.ReadData(sqlQuery);

            //проверяем есть ли в списках маршрутов маршрут с указанными городами routeStart и routeEnd,
            //который мы хотим добавить перевозчику
            try
            {
                if (rowsStr.Count == 0)
                    throw new Exception("Ошибка: маршрут с заданым номером не существует.");
                if (rowsStr.Count > 1)
                    throw new Exception("Ошибка: Найдено более одной записи с таким маршрутом.\n" +
                        "Проверьте корректность записей в таблице маршрутов");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return;
            }

            string routeID = rowsStr[0].Substring(0, rowsStr[0].IndexOf(';'));

            /* каждый отдельный маршрут должен быть связан с предыдущим и последующим 
             * routeEnd(1) == routeStart(2) и routeEnd(2) == routeStart(3) 
             * проверяем цепь маршрутов
             * !!! реализовать после..., по мере необходимости !!!
             * пока просто вносим маршруты соблюдая описанную выше логику - цепь маршрутов
             */

            string[] tmp = new string[carrierRoutesIdList.Length + 1];
            Array.Copy(carrierRoutesIdList, tmp, carrierRoutesIdList.Length);
            Array.Resize(ref carrierRoutesIdList, (carrierRoutesIdList.Length + 1));
            Array.Copy(tmp, carrierRoutesIdList, carrierRoutesIdList.Length);
            carrierRoutesIdList[carrierRoutesIdList.Length - 1] = routeID;

            //Console.WriteLine($"маршрут {routeStart} - {routeEnd} добавлен, номер маршрута : {routeID}");
        }

        // Добавление в маршрутный лист перевозчика записи о routeID из XML файла
        public void AddRouteFromXML(string routeID)
        {
            string[] tmp = new string[carrierRoutesIdList.Length + 1];
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

            int[] tmp = new int[carrierTransportsIdList.Length + 1];
            Array.Copy(carrierTransportsIdList, tmp, carrierTransportsIdList.Length);
            Array.Resize(ref carrierTransportsIdList, (carrierTransportsIdList.Length + 1));
            Array.Copy(tmp, carrierTransportsIdList, carrierTransportsIdList.Length);
            carrierTransportsIdList[carrierTransportsIdList.Length - 1] = transportID;

            //Console.WriteLine($"транспорт за номером {transportID} добавлен");
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
            return $"Перевозчик № {carrierID} \t расстояние {distanceABforOrder}км \t цена {costABforOrder:#.##} \t доставка {dayABforOrder}дн";
        }

        public override void Info()
        {
            Console.WriteLine("\n----------------- Информация о перевозчике ----------------\n\n");
            Console.WriteLine("Номер перевозчика ------- " + carrierID);
            InfoPerson();
            Console.Write("Номера маршрутов -------- ");

            foreach (string i in carrierRoutesIdList)
                Console.Write(i + " ");
            Console.WriteLine();

            Routes routes = new Routes();
            ConnDataBaseSQL db = new ConnDataBaseSQL();

            // выводим список маршрутов - маршрутный лист
            foreach (string i in carrierRoutesIdList)
                Console.WriteLine(routes.ViewTableOnRouteID(db, i));

            // выводим общую протяженность маршрутного листа
            int routesLength = 0;
            foreach (string i in carrierRoutesIdList)
            {
                string tmp = null;
                tmp = routes.ViewTableOnRouteID(db, i);
                tmp = tmp.Substring(tmp.IndexOf(';') + 1);
                tmp = tmp.Substring(tmp.IndexOf(';') + 1);
                tmp = tmp.Substring(tmp.IndexOf(';') + 2);
                tmp = tmp.Substring(0, tmp.IndexOf(';'));
                routesLength += Convert.ToInt32(tmp);
            }
            Console.Write($"Протяженность маршрутного листа: { routesLength} км");

            Console.WriteLine("\n");

            // выводим список транспорта
            Console.Write("Номера машин ------------ ");

            foreach (int i in carrierTransportsIdList)
                Console.Write(i + " ");
            Console.WriteLine("\n");
            
            // выводим список машин
            Transports transports = new Transports();
            foreach (int i in carrierTransportsIdList)
                transports.InfoFromSQLtableOnRouteID(i);

            Console.WriteLine("\n-----------------------------------------------------------\n\n");
        }

        // Вносит данные в таблицу БД
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
