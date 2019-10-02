using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_Logistics_20190717
{
    class Transport
    {
        private static int transportCount = 0;
        public int transportID { get; }
        public string transportType { get; set; }
        public int bodyWeight { get; set; }
        public double bodyVolume { get; set; }
        public int currentWeight { get; set; }
        public double currentVolume { get; set; }

        public struct CurrentLoad
        {
            int capacity;
            double volume;

            public CurrentLoad(int capacity, double volume)
            {
                this.capacity = capacity;
                this.volume = volume;
            }

            public CurrentLoad GetcurrentLoad()
            {
                return this;
            }

            public void SetCurrentLoad(ref int capacity, double volume)
            {
                this.capacity += capacity;
                this.volume += volume;
            }
            public void CurrentLoadInfo()
            {
                Console.Write($"Масса:{capacity}, Объем:{volume}");
            }
        }

        public Transport() { }

        public Transport(string transportType, int bodyWeight, double bodyVolume)
        {
            this.transportID = ++transportCount;
            this.transportType = transportType;
            this.bodyWeight = bodyWeight;
            this.bodyVolume = bodyVolume;
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

        public void Loading(int addWeight, double addVolume) 
        {
            if ((bodyWeight != currentWeight + addWeight)&& (bodyVolume != currentVolume + addVolume))
                currentWeight += addWeight;
                currentVolume += addVolume;
        }

        public void Info()
        {
            Console.WriteLine("\n----------------- Информация о транспорте -----------------\n\n");
            Console.WriteLine("Номер транспорта --------- " + transportID);
            Console.WriteLine("Тип машины --------------- " + transportType);
            Console.WriteLine("Грузоподъемность --------- " + bodyWeight + " кг");
            Console.WriteLine("Объем грузового отсека --- " + bodyVolume + " куб.м");
            Console.WriteLine("\n\nТекущая загрузка --------- " + currentWeight + " кг" + " - " + currentVolume + " куб.м");
            Console.WriteLine("\n-----------------------------------------------------------\n\n");
        }

        // Вносит данные в таблицу БД
        public void InsertTable(IConnDataBaseSQL obj)
        {
        Console.WriteLine(@"Insert Data to table ""Transports"" about "
               + Convert.ToString(this.GetType()).Substring(22));

            StringBuilder sb = new StringBuilder();
            sb.Append("USE LogisticsOVA; ");
            sb.Append("INSERT INTO Transports (transportType, bodyWeight, " +
                "bodyVolume) VALUES ");
            sb.Append($"('{transportType}', '{bodyWeight}', " +
                $"'{bodyVolume}') ");

            string sqlQuery = sb.ToString();

            obj.SaveData(sqlQuery);
        }

        // Устнавливает счетчик на цифру кол-ва ранее созданных объектов
        public void SetCountObj(IConnDataBaseSQL obj)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("USE LogisticsOVA; ");
            sb.Append("SELECT COUNT(1) FROM Transports");
            string sqlQuery = sb.ToString();

            transportCount = obj.ReadCountRowInTable(sqlQuery);
            Console.WriteLine("\n-------------------------------------------------------------------");
            Console.WriteLine(@"Кол-во строк в tаблице ""Transports"" - " + transportCount);
            Console.WriteLine("\n-------------------------------------------------------------------");
        }
    }

}
