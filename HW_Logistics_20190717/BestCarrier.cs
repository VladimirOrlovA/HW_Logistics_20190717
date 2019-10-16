using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_Logistics_20190717
{
    class BestCarrier
    {
        // срок доставки 100км пути 1 день
        int kmPerDay = 100;

        // расстояние между городами по заданному маршруту
        int ABdistance = 0;

        // номер перевозчика с саммым коротким маршрутом от точки A до точки B
        int carrierIDwithBestWay = -1;

        // номер перевозчика с наилучшей стоимостью
        int carrierIDwithBestCost = -1;

        double orderCost = 0;
        int minDistance = 100000;
        double minCost = 100000;

        int bestCarrierInd = -1;

        public void BestWayandPrice(Carriers carriers, Routes routes, Order order, Transports transports)
        {
            order.Info();
            SearchBestWay(carriers, routes, order);
            SearchBestPrice(carriers, routes, order, transports);



            minDistance = 100000;
            minCost = 100000;

            for (int i = 0; i < carriers.carriersList.Capacity; i++)
                if (carriers.carriersList[i].foundRouteAB)
                {
                    if (carriers.carriersList[i].distanceABforOrder < minDistance && carriers.carriersList[i].costABforOrder < minCost)
                    {
                        minDistance = carriers.carriersList[i].distanceABforOrder;
                        minCost = carriers.carriersList[i].costABforOrder;
                        bestCarrierInd = i;
                    }

                    Console.WriteLine(carriers.carriersList[i]);
                }

            Console.WriteLine("\nЛучший вариант - мин срок и мин стоимость\n");

            Console.WriteLine(carriers.carriersList[bestCarrierInd]);

        }


        // поиск перевозчика с саммым коротким маршрутом от точки A до точки B
        public void SearchBestWay(Carriers carriers, Routes routes, Order order)
        {
            int pointA = routes.GetCityNumber(order.from);
            int pointB = routes.GetCityNumber(order.to);

            // подсчет найденных перевозчиков 
            int carriersCount = 1;

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
                            c.foundRouteAB = true;
                            //carriersCount++;
                            c.distanceABforOrder = ABdistance;
                            break;
                        }

                    }
                }

                if (ABdistance < minDistance)
                {
                    minDistance = ABdistance;
                    carrierIDwithBestWay = c.carrierID;
                }



                c.distanceABforOrder = ABdistance;

                if (c.foundRouteAB)
                {
                    Console.WriteLine($" {carriersCount++}) - Перевозчик № {c.carrierID} - " +
                      $"Расстояние между пунктами {c.distanceABforOrder}км {c.dayABforOrder = c.distanceABforOrder / kmPerDay}дн ");
                }


                // для просмотра маршрута у следующего перевозчика приводим вводные данные в исходное состояние 
                ABdistance = 0;
                pointA = routes.GetCityNumber(order.from);
                pointB = routes.GetCityNumber(order.to);
            }
            Console.WriteLine();
            //Console.WriteLine($"Найдено по запросу - {carriersCount - 1}");
            Console.WriteLine($"Самый короткий путь из {order.from} в {order.to} у Перевозчика № {carrierIDwithBestWay} - расстояние {minDistance}км");

            Console.WriteLine();
            Console.WriteLine($"Будет доставлено за  {minDistance / kmPerDay} дней");

            Console.WriteLine("\n\n");
        }


        // поиск перевозчика с самой дешевой стоимостью доставки от точки A до точки B
        public void SearchBestPrice(Carriers carriers, Routes routes, Order order, Transports transports)
        {
            // подсчет найденных перевозчиков 
            int carriersCount = 1;

            foreach (Carrier c in carriers.carriersList)
            {
                foreach (int transportID in c.carrierTransportsIdList)
                    foreach (Transport transport in transports.transportsList)
                    {
                        if (transportID == transport.transportID)
                            orderCost = transport.GetСoefficientCost() * c.distanceABforOrder / 100 * order.volume;
                    }

                if (c.foundRouteAB)
                    Console.WriteLine($" {carriersCount++}) Перевозчик № {c.carrierID} - стоимость транспортировки {orderCost:#.##}тг");
                if (c.foundRouteAB && orderCost < minCost)
                {
                    minCost = orderCost;
                    carrierIDwithBestCost = c.carrierID;
                }
                c.costABforOrder = orderCost;

                // для получения с/с перевозки заказа у следующего перевозчика приводим вводные данные в исходное состояние 
                orderCost = 0;
            }

            Console.WriteLine();
            Console.WriteLine($"Самая низкая стоимость посылки из {order.from} в {order.to} у Перевозчика № {carrierIDwithBestCost} - цена {minCost:#.##}тг");
            Console.WriteLine("\n\n");
        }
    }
}
