using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_Logistics_20190717
{
    class Сarrier
    {
        string carrierName;
        Route[] routeList = new Route[0];

        public Сarrier(string carrierName)
        {
            this.carrierName = carrierName;
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

            //for (int i=0; i<routeList.Length; i++)
            //{
            //    tmp[i] = routeList[i];
            //}

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
                Console.WriteLine("Из города " + i.routeStart + " в город " + i.routeEnd);
            }
        }

        public void InfoCarrier()
        {
            Console.WriteLine("\n----------------- Информация о перевозчике -----------------\n\n");
            Console.WriteLine("Название перевозчика ----------- " + carrierName);
            InfoRoutes();
            //Console.WriteLine("Начало маршрута ---------- " + routeStart);
            //Console.WriteLine("Конец маршрута ----------- " + routeEnd);
            Console.WriteLine("\n---------------------------------------------------------\n\n");
        }

    }
}
