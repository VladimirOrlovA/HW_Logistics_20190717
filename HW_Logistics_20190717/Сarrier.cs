﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_Logistics_20190717
{
    class Carrier : Person
    {
        private static int carriersCount = 0;
        public int carrierID { get; }
        public int carrierRouteID { get; set; }
        public int carrierTransportID { get; set; }

        public Carrier() { }

        public Carrier(string lastName, string firstName, string middleName, DateTime birthday, long iin)
            : base(lastName, firstName, middleName, birthday, iin)
        {
            carrierID = ++carriersCount;
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

        public override void Info()
        {
            Console.WriteLine("\n----------------- Информация о перевозчике ----------------\n\n");
            Console.WriteLine("Номер перевозчика ------- " + carrierID);
            InfoPerson();
            Console.WriteLine("Номер маршрута ---------- " + carrierRouteID);
            //InfoRoutes(carrierID);
            Console.WriteLine("\n-----------------------------------------------------------\n\n");
        }

        // Вставляет данные в таблицу БД
        public void InsertTable(IConnDataBaseSQL obj)
        {
            Console.WriteLine(@"Insert Data to table ""Carriers"" about "
               + Convert.ToString(this.GetType()).Substring(22));

            StringBuilder sb = new StringBuilder();
            sb.Append("USE LogisticsOVA; ");
            sb.Append("INSERT INTO Carriers (lastName, firstName, middleName, birthday, iin, " +
                "carrierRouteID, carrierTransportID) VALUES ");
            sb.Append($"('{lastName}', '{firstName}', '{middleName}', '{birthday.Year}-{birthday.Month}-{birthday.Day}', '{iin}'," +
                $" '{carrierRouteID}', '{carrierTransportID}') ");
            string sqlQuery = sb.ToString();

            obj.SaveData(sqlQuery);
        }

        // Устнавливает счетчик на цифру кол-ва ранее созданных объектов
        public void SetCountObj(IConnDataBaseSQL obj)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("USE LogisticsOVA; ");
            sb.Append("SELECT COUNT(1) FROM Carriers");
            string sqlQuery = sb.ToString();

            carriersCount = obj.ReadCountRowInTable(sqlQuery);
            Console.WriteLine("\n-------------------------------------------------------------------");
            Console.WriteLine(@"Кол-во строк в tаблице ""Carriers"" - " + carriersCount);
            Console.WriteLine("\n-------------------------------------------------------------------");
        }

    }
}
