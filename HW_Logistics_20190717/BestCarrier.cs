using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_Logistics_20190717
{
    class BestCarrier
    {
        public void BestWayandPrice(Carriers carriers, Routes routes, Order order, Transports transports)
        {
            order.Info();
            SearchBestWay(carriers, routes, order);
            SearchBestPrice(carriers, routes, order, transports);
        }
        // поиск перевозчика с саммым коротким маршрутом от точки A до точки B
        public void SearchBestWay(Carriers carriers, Routes routes, Order order)
        {
            int pointA = routes.GetCityNumber(order.from);
            int pointB = routes.GetCityNumber(order.to);

            int ABdistance = 0;
            int minDistance = 10000;
            int carrierIDwithBestWay = -1;
            int carriersCount = 0;
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

                        if (secondNode == pointB)
                        {
                            c.ABfound = true;
                            carriersCount++;
                            c.ABdistanceForOrder = ABdistance;
                            break;
                        }

                    }
                }

                if (ABdistance < minDistance)
                {
                    minDistance = ABdistance;
                    carrierIDwithBestWay = c.carrierID;
                }
        
                if(c.ABfound)
                Console.WriteLine($"Перевозчик {c.carrierID} - Расстояние между пунктами {c.ABdistanceForOrder}км");

                // для просмотра маршрута у следующего перевозчика приводим вводные данные в исходное состояние 
                ABdistance = 0;
                pointA = routes.GetCityNumber(order.from);
                pointB = routes.GetCityNumber(order.to);
            }
            Console.WriteLine();
            Console.WriteLine($"Найдено по запросу - {carriersCount}");
            Console.WriteLine($"Самый короткий путь от {order.from} до {order.to} у Перевозчика {carrierIDwithBestWay} - Расстояние между пунктами {minDistance}км");
            Console.WriteLine("\n\n");
        }

        // поиск перевозчика с самой дешевой стоимостью доставки от точки A до точки B
        public void SearchBestPrice(Carriers carriers, Routes routes, Order order, Transports transports)
        {
            double orderCost = 0;
            double minOrderCost = 10000;
            int carrierIDwithBestCost = -1;

            foreach (Carrier c in carriers.carriersList)
            {
                foreach (int transportID in c.carrierTransportsIdList)
                    foreach (Transport transport in transports.transportsList)
                    {
                        if(transportID == transport.transportID)
                        orderCost = transport.GetСoefficientCost() * c.ABdistanceForOrder/100 * order.volume;
                    }

                if (c.ABfound)
                Console.WriteLine($"Перевозчик {c.carrierID} - стоимость транспортировки {orderCost:#.##}тг");
                if (c.ABfound && orderCost < minOrderCost)
                {
                    minOrderCost=orderCost;
                    c.ABcostForOrder = orderCost;
                    carrierIDwithBestCost = c.carrierID;
                }
                // для получения с/с перевозки заказа у следующего перевозчика приводим вводные данные в исходное состояние 
                orderCost = 0;
            }

            Console.WriteLine();
            Console.WriteLine($"Самая низкая стоимсоть от {order.from} до {order.to} у Перевозчика {carrierIDwithBestCost} - стоимость транспортировки {minOrderCost:#.##}тг");
            Console.WriteLine("\n\n");
        }
    }
}
