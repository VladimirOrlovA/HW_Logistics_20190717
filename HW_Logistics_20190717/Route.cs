using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_Logistics_20190717
{
    class Route
    {
        public int routeID { get; set; }
        public string routeStart { get; set; }
        public string routeEnd { get; set; }

        public Route(int routeID, string routeStart, string routeEnd)
        {
            this.routeID = routeID;
            this.routeStart = routeStart;
            this.routeEnd = routeEnd;
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

        public void InfoRoute()
        {
            Console.WriteLine("\n----------------- Информация о маршруте -----------------\n\n");
            Console.WriteLine("Номер маршрута ----------- " + routeID);
            Console.WriteLine("Начало маршрута ---------- " + routeStart);
            Console.WriteLine("Конец маршрута ----------- " + routeEnd);
            Console.WriteLine("\n---------------------------------------------------------\n\n");
        }
    }
}

