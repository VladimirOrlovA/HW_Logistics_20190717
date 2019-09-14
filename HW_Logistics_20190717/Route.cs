using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_Logistics_20190717
{
    class Route
    {
        private static int routesCount = 0;
        public int routeID { get; }
        public string routeStart { get; set; }
        public string routeEnd { get; set; }
        public int routeLength { get; set; }

        public Route() { }

        public Route(string routeStart, string routeEnd, int routeLength)
        {
            this.routeID = ++routesCount;
            this.routeStart = routeStart;
            this.routeEnd = routeEnd;
            this.routeLength = routeLength;
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

        public void Info()
        {
            Console.WriteLine("\n----------------- Информация о маршруте -------------------\n\n");
            Console.WriteLine("Номер маршрута ----------- " + routeID);
            Console.WriteLine("Начало маршрута ---------- " + routeStart);
            Console.WriteLine("Конец маршрута ----------- " + routeEnd);
            Console.WriteLine("Расстояние --------------- " + routeLength + " км");
            Console.WriteLine("\n-----------------------------------------------------------\n\n");
        }

        // Вставляет данные в таблицу БД
        public void InsertTable(IConnDataBaseSQL obj)
        {
            Console.WriteLine(@"Insert Data to table ""Routes"" about "
               + Convert.ToString(this.GetType()).Substring(22));

            StringBuilder sb = new StringBuilder();
            sb.Append("USE LogisticsOVA; ");
            sb.Append("INSERT INTO Routes (routeStart, routeEnd, routeLength) VALUES ");
            sb.Append($"('{routeStart}', '{routeEnd}', '{routeLength}') ");
            string sqlQuery = sb.ToString();

            obj.SaveData(sqlQuery);
        }

        // Устнавливает счетчик на цифру кол-ва ранее созданных объектов
        public void SetCountObj(IConnDataBaseSQL obj)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("USE LogisticsOVA; ");
            sb.Append("SELECT COUNT(1) FROM Routes");
            string sqlQuery = sb.ToString();

            routesCount = obj.ReadCountRowInTable(sqlQuery);
            Console.WriteLine("\n-------------------------------------------------------------------");
            Console.WriteLine(@"Кол-во строк в tаблице ""Routes"" - " + routesCount);
            Console.WriteLine("\n-------------------------------------------------------------------");
        }
    }
}

