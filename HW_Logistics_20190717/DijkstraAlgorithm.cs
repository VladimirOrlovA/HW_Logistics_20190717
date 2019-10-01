using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_Logistics_20190717
{
    class DijkstraAlgorithm
    {
        protected int size = 0;
        protected Route[,] linkMatrix = new Route[0, 0]; // матрица связей
        protected int[] distance = new int[0]; // минимальное расстояние
        protected int[] nodes = new int[0]; // посещенные вершины
        protected int tempDistance, minIndex, minDistance;
        protected int beginIndex = 0; // начало маршрута
        protected int endIndex = 0;   // конец маршрута

        public DijkstraAlgorithm(Routes routes, Carriers carriers)
        {
            size = routes.arrRoutes.GetLength(1);            // получаем размер матрицы связей - равен размеру матрицы маршрутов
            ResizeArray2D<Route>(ref linkMatrix, size, size);    // устанавливаем размер матрицы связей
            FillLinkMatrix(routes, carriers);                    // заполняем матрицу связей - имеющимеся маршрутами
            ResizeArray1D<int>(ref distance, size);
            ResizeArray1D<int>(ref nodes, size);
            FillNodesAndDistance();
        }

        // изменение размера двумерного массива
        void ResizeArray2D<T>(ref T[,] original, int newRowNum, int newColumnNum)
        {
            var newArray = new T[newRowNum, newColumnNum];
            int columnCount = original.GetLength(1);
            int columnCount2 = newRowNum;
            int columns = original.GetUpperBound(0);
            for (int co = 0; co <= columns; co++)
                Array.Copy(original, co * columnCount, newArray, co * columnCount2, columnCount);
            original = newArray;
        }

        // изменение размера одномерного массива
        void ResizeArray1D<T>(ref T[] original, int newColumnNum)
        {
            Array.Resize(ref original, newColumnNum);
        }

        // заполнение матрицы связей
        void FillLinkMatrix(Routes routes, Carriers carriers)
        {
            /* Считываем все routeID (3-15 это имя, в имени 3 - номер города 15 - номер для связи) 
             * у всех имеющихся перевозчиков,
             * чтобы заполнить матрицу связей для поиска оптимального маршрута
             */
            List<string> routesID = new List<string>();
            foreach (Carrier i in carriers.carriersList)
                for (int r = 0; r != i.carrierRoutesIdList.Length; r++)
                {
                    routesID.Add(i.carrierRoutesIdList[r]);
                    //Console.WriteLine(i.carrierRoutesIdList[r]);
                }

            
            //Инициализация матрицы связей
            //Записываем в матрицу имеющиеся маршруты перевозчиков
            foreach (string strRouteId in routesID)
                foreach (Route route in routes.arrRoutes)
                {
                    if (strRouteId == route.routeID)
                    {

                        int i = Convert.ToInt32(strRouteId.Substring(0, strRouteId.IndexOf('-')));
                        int j = Convert.ToInt32(strRouteId.Substring(strRouteId.IndexOf('-') + 1));

                        linkMatrix[i, j] = route;
                        linkMatrix[j, i] = route;
                    }
                }

            Console.WriteLine();
        }

        // инициализация вершин и расстояний
        void FillNodesAndDistance()
        {
            for (int i = 0; i < size; i++)
            {
                nodes[i] = 1; // помечаем все вершины 1, признак того что они не просмотрены
                distance[i] = 10000; // устанавливаем максимальный вес вершины
            }
        }

        public void PrintLinkMatrix()
        {
            // Вывод матрицы связей
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                    if (linkMatrix[i, j]!= null)
                        Console.Write($"{linkMatrix[i, j].routeDistance} \t");
                    else Console.Write(0 + "\t");
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void Algorithm(Order order, Routes routes)
        {
            beginIndex = routes.GetCityNumber(order.from);
            distance[beginIndex] = 0;

            endIndex = routes.GetCityNumber(order.to);

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
                        if (linkMatrix[minIndex, i]!=null && linkMatrix[minIndex, i].routeDistance > 0 )
                        {
                            tempDistance = minDistance + linkMatrix[minIndex, i].routeDistance;
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
                if (distance[i] != 10000)
                    Console.WriteLine($"{i} - {distance[i]}км ");
            }

            Console.WriteLine();

            // Восстановление пути
            int[] checkNodes = new int[size]; // массив посещенных вершин
            //endIndex = 10; // 10 костонай - индекс конечной вершины = 10 - 1   //// было значени 4  //индекс конечной вершины = 5 - 1
            checkNodes[0] = endIndex; // начальный элемент - конечная вершина
            int k = 1; // индекс предыдущей вершины
            int weight = distance[endIndex]; // вес конечной вершины

            while (endIndex != beginIndex) // пока не дошли до начальной вершины
            {
                for (int i = 0; i < size; i++) // просматриваем все вершины
                    if (linkMatrix[endIndex, i] != null && linkMatrix[endIndex, i].routeDistance != 0)   // если связь есть
                    {
                        int tempWeight = weight - linkMatrix[endIndex, i].routeDistance; // определяем вес пути из предыдущей вершины
                        if (tempWeight == distance[i]) // если вес совпал с рассчитанным
                        {                 // значит из этой вершины и был переход
                            weight = tempWeight; // сохраняем новый вес
                            endIndex = i;       // сохраняем предыдущую вершину
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

            // Вывод пути (начальная вершина оказалась в конце массива из k элементов)
            Console.WriteLine("Вывод кратчайшего пути : ");
            for (int i = k - 1; i >= 0; i--)
                Console.Write(routes.GetCityName(checkNodes[i]) + " - ");
            Console.WriteLine();
        }

    }
}
