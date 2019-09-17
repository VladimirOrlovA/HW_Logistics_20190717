using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_Logistics_20190717
{
    class Route
    {
        public string routeID { get; set; }
        public string routeStart { get; set; }
        public string routeEnd { get; set; }
        public int routeDistance { get; set; }

        public Route() { }

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
            Console.WriteLine("Расстояние --------------- " + routeDistance + " км");
            Console.WriteLine("\n-----------------------------------------------------------\n\n");
        }

        // Вставляет данные в таблицу БД
        public void InsertTable(IConnDataBaseSQL obj)
        {
            Console.WriteLine(@"Insert Data to table ""Routes"" about "
               + Convert.ToString(this.GetType()).Substring(22));

            StringBuilder sb = new StringBuilder();
            sb.Append("USE LogisticsOVA; ");
            sb.Append("INSERT INTO Routes (routeID, routeDistance) VALUES ");
            sb.Append($"('{routeID}', '{routeDistance}') ");
            string sqlQuery = sb.ToString();

            obj.SaveData(sqlQuery);
        }
    }
}

