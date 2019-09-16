using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace HW_Logistics_20190717
{
    class LoadArrayRoutes
    {
        public static int columnNum = 1;
        public static int rowNum = 0;
        public string[] fileStr = new string[0];

        public int[,] arrRoutes = new int[columnNum, rowNum];

        void ResizeArray<T>(ref T[,] original, int newColumnNum, int newRowNum)
        {
            var newArray = new T[newColumnNum, newRowNum];
            int columnCount = original.GetLength(1);
            int columnCount2 = newRowNum;
            int columns = original.GetUpperBound(0);
            for (int co = 0; co <= columns; co++)
                Array.Copy(original, co * columnCount, newArray, co * columnCount2, columnCount);
            original = newArray;
        }

        public void FillArray()
        {
            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader(@"C:\Users\Orlov\source\repos\HW_Logistics_20190717\Carriers routes\DistancesBetweenCitiesOfKazakhstan.csv"))
                {
                    string line;
                    // Read and display lines from the file until the end of 
                    // the file is reached.
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] tmp = new string[fileStr.Length + 1];
                        Array.Copy(fileStr, tmp, fileStr.Length);
                        Array.Resize(ref fileStr, (fileStr.Length + 1));
                        Array.Copy(tmp, fileStr, fileStr.Length);
                        fileStr[fileStr.Length - 1] = line;
                    }
                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            foreach (string i in fileStr)
                Console.WriteLine(i);

            // ===== перепишем данные дистанций между городами из строк в массив =====

            // получим размер массива и запишем его новые размеры
            for (int i = 0; i < fileStr[0].Length; i++)
                if (fileStr[0][i] == ',')
                    columnNum++;

            rowNum = fileStr.Length;
            ResizeArray<int>(ref arrRoutes, columnNum, rowNum);

            for (int i = 1; i < rowNum; i++)
                for (int j = 1; j < columnNum; j++)
                {
                    arrRoutes[i, j]= fileStr[i][j];
                }
        }

        public void PrintArray()
        {
            for (int i = 0; i < rowNum; i++)
            {
                for (int j = 0; j < columnNum; j++)
                {
                    Console.Write(arrRoutes[i, j] + " ");
                }
                Console.WriteLine();
            }

        }
    }
}
