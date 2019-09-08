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
        int carrierID;
        Route[] routeList = new Route[0];

        public Carrier(string carrierName)
        {
            carrierID = ++carriersCount;
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

        public void addRoute(Route addRoute)
        {
            Route[] tmp = new Route[routeList.Length + 1];
            Array.Copy(routeList, tmp, routeList.Length);
            Array.Resize(ref routeList, (routeList.Length + 1));
            Array.Copy(tmp, routeList, routeList.Length);
            routeList[routeList.Length-1] = addRoute;
        }

        public void InfoRoutes()
        {
            Console.WriteLine("\nИмеющиеся маршруты:");

            foreach (Route i in routeList)
            {
                Console.WriteLine(i.routeID + " - " + i.routeStart + " - " + i.routeEnd);
            }
        }

        public override void Info()
        {
            Console.WriteLine("\n----------------- Информация о перевозчике -----------------\n\n");
            Console.WriteLine("Название перевозчика ----------- " + carriersCount);
            InfoRoutes();
            Console.WriteLine("\n---------------------------------------------------------\n\n");
        }

        // Вставляет данные в таблицу БД
        public void InsertTable(IConnDataBaseSQL obj)
        {
            Console.WriteLine(@"Insert Data to table ""Workers"" about "
               + Convert.ToString(this.GetType()).Substring(22));

            StringBuilder sb = new StringBuilder();
            sb.Append("USE LogisticsOVA; ");
            sb.Append("INSERT INTO Carriers (lastName, firstName, middleName, birthday, inn, employmentDate, position, solary) VALUES ");
            sb.Append($"('{lastName}', '{firstName}', '{middleName}', '{birthday.Year}-{birthday.Month}-{birthday.Day}', '{inn}'," +
                $" '{employmentDate.Year}-{employmentDate.Month}-{employmentDate.Day}', '{position}', '{solary}') ");
            string sqlQuery = sb.ToString();

            obj.SaveData(sqlQuery);
        }


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
