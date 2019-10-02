using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_Logistics_20190717
{
    class BestCarrier
    {
        // поиск перевозчика с саммым коротким маршрутом от точки A до точки B
        public void SearchBestWay(Carriers carriers, Routes routes, Order order)
        {
            int pointA = routes.GetCityNumber(order.from);
            int pointB = routes.GetCityNumber(order.to);

            int ABdistance = 0;
            int minDistance = 10000;
            int carrierIDwithBestWay = -1;
            // смотрим перевозчиков
            foreach (Carrier c in carriers.carriersList)
            {
                // смотрим маршрутный лист перевозчика - идем по связке путей
                for (int i = 0; i < c.carrierRoutesIdList.Length; i++)
                {
                    string strID = c.carrierRoutesIdList[i];
                    int firstNode = Convert.ToInt32(strID.Substring(0, strID.IndexOf('-')));
                    if (pointA == firstNode)
                    {
                        int secondNode = Convert.ToInt32(strID.Substring(strID.IndexOf('-') + 1));
                        ABdistance += routes.arrRoutes[firstNode, secondNode].routeDistance;
                        pointA = secondNode;
                        if (secondNode == pointB) break;
                    }
                }
                Console.WriteLine($"Перевозчик {c.carrierID} - Расстояние между пунктами {ABdistance}км");

                if (ABdistance < minDistance)
                {
                    minDistance = ABdistance;
                    carrierIDwithBestWay = c.carrierID;
                }

                // для просмотра маршрута у следующего перевозчика приводим вводные данные в исходное состояние 
                ABdistance = 0;
                pointA = routes.GetCityNumber(order.from);
                pointB = routes.GetCityNumber(order.to);
            }

            Console.WriteLine($"Самый коротки путь от A до B у Перевозчика {carrierIDwithBestWay} - Расстояние между пунктами {minDistance}км");
        }

        // поиск перевозчика с самой дешевой стоимсотью доставки от точки A до точки B
        public void SearchBestPrice(Carriers carriers, Routes routes, Order order)
        {

        }
    }
}
