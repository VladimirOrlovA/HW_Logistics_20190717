using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_Logistics_20190717
{
    class DijkstraAlgorithm
    {
        int size = 6;
        public Route[,] linkMatrix = new Route[6, 6]; // матрица связей

        public List<CityLink> cityLinks = new List<CityLink>();

        public void Algorithm()
        {
            int size = 6;
            int[,] linkMatrix = new int[size, size]; // матрица связей
            int[] distance = new int[size]; // минимальное расстояние
            int[] nodes = new int[size]; // посещенные вершины
            int tempDistance, minIndex, minDistance;
            int beginIndex = 0;

            // Инициализация матрицы связей
            //for (int i = 0; i < size; i++)
            //{
            //    linkMatrix[i, i] = 0;
            //    for (int j = i + 1; j < size; j++)
            //    {
            //        Console.Write($"Введите расстояние {i + 1} - {j + 1} : ");
            //        tempDistance = Convert.ToInt32(Console.ReadLine());
            //        linkMatrix[i, j] = tempDistance;
            //        linkMatrix[j, i] = tempDistance;
            //    }
            //}

            for (int i = 0; i < size; i++)
                linkMatrix[i, i] = 0;
            linkMatrix[0, 1] = 7;
            linkMatrix[0, 2] = 9;
            linkMatrix[0, 3] = 0;
            linkMatrix[0, 4] = 0;
            linkMatrix[0, 5] = 14;
            linkMatrix[1, 2] = 10;
            linkMatrix[1, 3] = 15;
            linkMatrix[1, 4] = 0;
            linkMatrix[1, 5] = 0;
            linkMatrix[2, 3] = 11;
            linkMatrix[2, 4] = 0;
            linkMatrix[2, 5] = 2;
            linkMatrix[3, 4] = 6;
            linkMatrix[3, 5] = 0;
            linkMatrix[4, 5] = 9;

            linkMatrix[1, 0] = 7;
            linkMatrix[2, 0] = 9;
            linkMatrix[3, 0] = 0;
            linkMatrix[4, 0] = 0;
            linkMatrix[5, 0] = 14;
            linkMatrix[2, 1] = 10;
            linkMatrix[3, 1] = 15;
            linkMatrix[4, 1] = 0;
            linkMatrix[5, 1] = 0;
            linkMatrix[3, 2] = 11;
            linkMatrix[4, 2] = 0;
            linkMatrix[5, 2] = 2;
            linkMatrix[4, 3] = 6;
            linkMatrix[5, 3] = 0;
            linkMatrix[5, 4] = 9;

            Console.WriteLine();

            // Вывод матрицы связей
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                    Console.Write(linkMatrix[i, j] + "\t");
                Console.WriteLine();
            }
            Console.WriteLine();

            //Инициализация вершин и расстояний
            for (int i = 0; i < size; i++)
            {
                distance[i] = 10000; // устанавливаем максимальный вес вершины
                nodes[i] = 1; // помечаем все вершины 1, признак того что они не просмотрены
            }
            distance[beginIndex] = 0;

            // Шаг алгоритма
            do
            {
                minIndex = 10000;
                minDistance = 10000;
                for (int i = 0; i < size; i++)
                { // Если вершину ещё не обошли и вес меньше minDistance
                    if ((nodes[i] == 1) && (distance[i] < minDistance))
                    { // Переприсваиваем значения
                        minDistance = distance[i];
                        minIndex = i;
                    }
                }
                // Добавляем найденный минимальный вес
                // к текущему весу вершины
                // и сравниваем с текущим минимальным весом вершины
                if (minIndex != 10000)
                {
                    for (int i = 0; i < size; i++)
                    {
                        // проверяем есть ли связь у вершины
                        if (linkMatrix[minIndex, i] > 0)
                        {
                            tempDistance = minDistance + linkMatrix[minIndex, i];
                            if (tempDistance < distance[i])
                            {
                                distance[i] = tempDistance;
                            }
                        }
                    }
                    nodes[minIndex] = 0; // помечаем вершину как просмотренную
                }
            } while (minIndex < 10000);


            // Вывод кратчайших расстояний до вершин
            Console.Write("\nКратчайшие расстояния до вершин: ");
            for (int i = 0; i < size; i++)
                Console.Write(distance[i] + " ");
            Console.WriteLine();

            // Восстановление пути
            int[] checkNodes = new int[size]; // массив посещенных вершин
            int end = 4; // индекс конечной вершины = 5 - 1
            checkNodes[0] = end + 1; // начальный элемент - конечная вершина
            int k = 1; // индекс предыдущей вершины
            int weight = distance[end]; // вес конечной вершины

            while (end != beginIndex) // пока не дошли до начальной вершины
            {
                for (int i = 0; i < size; i++) // просматриваем все вершины
                    if (linkMatrix[end, i] != 0)   // если связь есть
                    {
                        int tempWeight = weight - linkMatrix[end, i]; // определяем вес пути из предыдущей вершины
                        if (tempWeight == distance[i]) // если вес совпал с рассчитанным
                        {                 // значит из этой вершины и был переход
                            weight = tempWeight; // сохраняем новый вес
                            end = i;       // сохраняем предыдущую вершину
                            checkNodes[k] = i + 1; // и записываем ее в массив
                            k++;
                        }
                    }
            }


            // Вывод пути (начальная вершина оказалась в конце массива из k элементов)
            Console.WriteLine("Вывод кратчайшего пути : ");
            for (int i = k - 1; i >= 0; i--)
                Console.Write(checkNodes[i] + " - ");
            Console.WriteLine();
        }

        public void AD(Routes routes, Carriers carriers) //(int pointA, int pointB)
        {
            Console.WriteLine(routes.arrRoutes.GetLength(1));
            int size = routes.arrRoutes.GetLength(1); // устанавливаем размер матрицы связей
            int[,] linkMatrix = new int[size, size]; // матрица связей
            int[] distance = new int[size]; // минимальное расстояние
            int[] nodes = new int[size]; // посещенные вершины
            int tempDistance, minIndex, minDistance;
            int beginIndex = 0;

            /* Считываем все routeID (3-15 это имя, в имени 3 - номер города 15 - номер для связи) 
             * у всех имеющихся перевозчиков,
             * чтобы заполнить матрицу связей для поиска оптимального маршрута
             */
            List<string> routesID = new List<string>();
            foreach (Carrier i in carriers.carriersList)
                for (int r = 0; r != i.carrierRoutesIdList.Length; r++)
                {
                    routesID.Add(i.carrierRoutesIdList[r]);
                    Console.WriteLine(i.carrierRoutesIdList[r]);
                }

            /* routeID содержит запись 3-15, где 3 вершина  15 это признак второй вершины с которой 
             * есть связь например у вершины 3-15 есть связь с вершиной 15-18 */

            //Инициализация матрицы связей
            //Записываем в матрицу имеющиеся маршруты перевозчиков
            for (int i = 0; i < size; i++)
            {
                linkMatrix[i, i] = 0;
                for (int j = i + 1; j < size; j++)
                {
                    foreach (string strRouteId in routesID)
                    {
                        int firstInd = Convert.ToInt32(strRouteId.Substring(strRouteId.IndexOf('-')));
                        int secondInd = Convert.ToInt32(strRouteId.Substring(0, strRouteId.IndexOf('-')));
                        if (true)
                        {
                            linkMatrix[i, j] = routes.arrRoutes[i, j].routeDistance;
                            linkMatrix[j, i] = routes.arrRoutes[j, i].routeDistance;
                        }
                        //if (strRouteId == routes.arrRoutes[i, j].routeID)
                        //{
                        //    linkMatrix[i, j] = routes.arrRoutes[i, j].routeDistance;
                        //    linkMatrix[j, i] = routes.arrRoutes[j, i].routeDistance;
                        //}

                    }

                }
            }
            Console.WriteLine();

            // Вывод матрицы связей
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                    Console.Write(linkMatrix[i, j] + "\t");
                Console.WriteLine();
            }
            Console.WriteLine();

            //Инициализация вершин и расстояний
            for (int i = 0; i < size; i++)
            {
                distance[i] = 10000; // устанавливаем максимальный вес вершины
                nodes[i] = 1; // помечаем все вершины 1, признак того что они не просмотрены
            }
            distance[beginIndex] = 0;

            // Шаг алгоритма
            do
            {
                minIndex = 10000;
                minDistance = 10000;
                for (int i = 0; i < size; i++)
                { // Если вершину ещё не обошли и вес меньше minDistance
                    if ((nodes[i] == 1) && (distance[i] < minDistance))
                    { // Переприсваиваем значения
                        minDistance = distance[i];
                        minIndex = i;
                    }
                }
                // Добавляем найденный минимальный вес
                // к текущему весу вершины
                // и сравниваем с текущим минимальным весом вершины
                if (minIndex != 10000)
                {
                    for (int i = 0; i < size; i++)
                    {
                        // проверяем есть ли связь у вершины
                        if (linkMatrix[minIndex, i] > 0)
                        {
                            tempDistance = minDistance + linkMatrix[minIndex, i];
                            if (tempDistance < distance[i])
                            {
                                distance[i] = tempDistance;
                            }
                        }
                    }
                    nodes[minIndex] = 0;
                }
            } while (minIndex < 10000);


            // Вывод кратчайших расстояний до вершин
            Console.Write("\nКратчайшие расстояния до вершин: ");
            for (int i = 0; i < size; i++)
                Console.Write(distance[i] + " ");
            Console.WriteLine();

            // Восстановление пути
            int[] checkNodes = new int[size]; // массив посещенных вершин
            int end = 4; // индекс конечной вершины = 5 - 1
            checkNodes[0] = end + 1; // начальный элемент - конечная вершина
            int k = 1; // индекс предыдущей вершины
            int weight = distance[end]; // вес конечной вершины

            while (end != beginIndex) // пока не дошли до начальной вершины
            {
                for (int i = 0; i < size; i++) // просматриваем все вершины
                    if (linkMatrix[end, i] != 0)   // если связь есть
                    {
                        int tempWeight = weight - linkMatrix[end, i]; // определяем вес пути из предыдущей вершины
                        if (tempWeight == distance[i]) // если вес совпал с рассчитанным
                        {                 // значит из этой вершины и был переход
                            weight = tempWeight; // сохраняем новый вес
                            end = i;       // сохраняем предыдущую вершину
                            checkNodes[k] = i + 1; // и записываем ее в массив
                            k++;
                        }
                    }
            }


            // Вывод пути (начальная вершина оказалась в конце массива из k элементов)
            Console.WriteLine("Вывод кратчайшего пути : ");
            for (int i = k - 1; i >= 0; i--)
                Console.Write(checkNodes[i] + " - ");
            Console.WriteLine();
        }

        public void ADrev1(Routes routes, Carriers carriers) //(int pointA, int pointB)
        {
            /*смотрим все маршруты у всех перевозчиков и собираем их в один массив
             * для последующего определения связанных вершин графа и внесения их в матрицу связей*/
            GetСityLinksList(carriers);
            //Console.WriteLine($"Кол-во всех маршрутов перевозчиков = {cityLinks.Count}");

            Console.WriteLine(routes.arrRoutes.GetLength(1));
            int size = routes.arrRoutes.GetLength(1); // устанавливаем размер матрицы связей
            int[,] linkMatrix = new int[size, size]; // матрица связей
            int[] distance = new int[size]; // минимальное расстояние
            int[] nodes = new int[size]; // посещенные вершины
            int tempDistance, minIndex, minDistance;
            int beginIndex = 3;
         

            //Инициализация матрицы связей
            //Записываем в матрицу имеющиеся маршруты перевозчиков
            for (int i = 0; i != cityLinks.Count; i++)
                for (int j = 0; j != cityLinks.Count; j++)
                {
                    if (cityLinks[i].nextNode == cityLinks[j].node)
                    {
                        linkMatrix[cityLinks[i].node, cityLinks[j].node] =
                            routes.arrRoutes[cityLinks[i].node, cityLinks[j].node].routeDistance;

                        linkMatrix[cityLinks[j].node, cityLinks[i].node] =
                            routes.arrRoutes[cityLinks[i].node, cityLinks[j].node].routeDistance;
                    }
                }

            // Вывод матрицы связей
            PrintLinkMatrix();

            Console.WriteLine("\n============================= матрица связей =============================\n");
            Console.WriteLine();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                    Console.Write(linkMatrix[i, j] + "\t");
                Console.WriteLine();
            }
            Console.WriteLine();

            //Инициализация вершин и расстояний
            for (int i = 0; i < size; i++)
            {
                distance[i] = 10000; // устанавливаем максимальный вес вершины
                nodes[i] = 1; // помечаем все вершины 1, признак того что они не просмотрены
            }
            distance[beginIndex] = 0;

            // Шаг алгоритма
            do
            {
                minIndex = 10000;
                minDistance = 10000;
                for (int i = 0; i < size; i++)
                { // Если вершину ещё не обошли и вес меньше minDistance
                    if ((nodes[i] == 1) && (distance[i] < minDistance))
                    { // Переприсваиваем значения
                        minDistance = distance[i];
                        minIndex = i;
                    }
                }
                // Добавляем найденный минимальный вес
                // к текущему весу вершины
                // и сравниваем с текущим минимальным весом вершины
                if (minIndex != 10000)
                {
                    for (int i = 0; i < size; i++)
                    {
                        // проверяем есть ли связь у вершины
                        if (linkMatrix[minIndex, i] > 0)
                        {
                            tempDistance = minDistance + linkMatrix[minIndex, i];
                            if (tempDistance < distance[i])
                            {
                                distance[i] = tempDistance;
                            }
                        }
                    }
                    nodes[minIndex] = 0;
                }
            } while (minIndex < 10000);


            // Вывод кратчайших расстояний до вершин
            Console.Write($"\nКратчайшие расстояния от вершины {beginIndex} до вершин: \n");
            for (int i = 0; i < size; i++)
            {
                if(distance[i]!=10000)
                Console.WriteLine($"{i} - {distance[i]}км ");
            }
                
            Console.WriteLine();

            // Восстановление пути
            int[] checkNodes = new int[size]; // массив посещенных вершин
            int end = 6; // 10 костонай - индекс конечной вершины = 10 - 1   //// было значени 4  //индекс конечной вершины = 5 - 1
            checkNodes[0] = end; // начальный элемент - конечная вершина
            int k = 1; // индекс предыдущей вершины
            int weight = distance[end]; // вес конечной вершины

            while (end != beginIndex) // пока не дошли до начальной вершины
            {
                for (int i = 0; i < size; i++) // просматриваем все вершины
                    if (linkMatrix[end, i] != 0)   // если связь есть
                    {
                        int tempWeight = weight - linkMatrix[end, i]; // определяем вес пути из предыдущей вершины
                        if (tempWeight == distance[i]) // если вес совпал с рассчитанным
                        {                 // значит из этой вершины и был переход
                            weight = tempWeight; // сохраняем новый вес
                            end = i;       // сохраняем предыдущую вершину
                            checkNodes[k] = i; // и записываем ее в массив
                            k++;
                        }
                    }
            }


            // Вывод пути (начальная вершина оказалась в конце массива из k элементов)
            Console.WriteLine("Вывод кратчайшего пути : ");
            for (int i = k - 1; i >= 0; i--)
                Console.Write(checkNodes[i] + " - ");
            Console.WriteLine();
        }

        // Заполнение матрицы связей
        void GetСityLinksList(Carriers carriers)
        {
            /* Считываем все routeID (3-15 это номер маршрута, в номере 3 - номер города 15 - 
             * номер города, для связи) у всех имеющихся перевозчиков, чтобы заполнить матрицу 
             * связей для поиска оптимального маршрута
             */

            /* routeID содержит запись 3-15, где 3 вершина  15 это признак второй вершины с которой 
             * есть связь, например у вершины 3-15 есть связь с вершиной 15-18 */

            foreach (Carrier i in carriers.carriersList)
                for (int r = 0; r != i.carrierRoutesIdList.Length; r++)
                {
                    string str = i.carrierRoutesIdList[r];
                    int node = Convert.ToInt32(str.Substring(0, str.IndexOf('-')));
                    int nextNode = Convert.ToInt32(str.Substring(str.IndexOf('-') + 1));
                    CityLink tmp = new CityLink(node, nextNode);
                    cityLinks.Add(tmp);

                    Console.WriteLine($"Город {tmp.node} связан с городом {tmp.nextNode}");
                    //Console.WriteLine(i.carrierRoutesIdList[r]);
                }
        }

        // Структура для метода - Заполнение матрицы связей
        public struct CityLink
        {
            public int node;
            public int nextNode;

            public CityLink(int node, int nextNode)
            {
                this.node = node;
                this.nextNode = nextNode;
            }
        }


        // Вывод матрицы связей
        void PrintLinkMatrix()
        {
            // Вывод матрицы связей
            Console.WriteLine("\n============================= матрица связей =============================\n");
            Console.WriteLine();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                    Console.Write(linkMatrix[i, j] + "\t");
                Console.WriteLine();
            }
            Console.WriteLine();
        }

    }
}
