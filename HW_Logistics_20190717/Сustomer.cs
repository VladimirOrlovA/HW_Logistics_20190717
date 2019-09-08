using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_Logistics_20190717
{
    internal class Customer : Person
    {
        private static int customersCount = 0;
        public int customerID { get; }
        public int contractID { get; set; }
        public DateTime registrationDate = DateTime.Today;

        public Customer() { }

        public Customer(string lastName, string firstName, string middleName, DateTime birthday, long inn) //, int orderID)
            : base(lastName, firstName, middleName, birthday, inn)
        {
            customerID = ++customersCount;
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
            Console.WriteLine("\n----------------- Информация о заказчике -----------------\n\n");
            Console.WriteLine("Номер клиента ----------- " + customerID);
            Console.WriteLine("Дата регистрации -------- " + registrationDate);
            InfoPerson();
            Console.WriteLine("Номер договора ---------- " + contractID);
            Console.WriteLine("\n---------------------------------------------------------\n\n");
        }

        // Вставляет данные в таблицу БД
        public void InsertTable(IConnDataBaseSQL obj)
        {
            Console.WriteLine(@"Insert Data to table ""Customers"" about "
               + Convert.ToString(this.GetType()).Substring(22));

            StringBuilder sb = new StringBuilder();
            sb.Append("USE LogisticsOVA; ");
            sb.Append("INSERT INTO Customers (lastName, firstName, middleName, " +
                "birthday, inn, contractID) VALUES ");
            sb.Append($"('{lastName}', '{firstName}', '{middleName}', " +
                $"'{birthday.Year}-{birthday.Month}-{birthday.Day}', '{inn}', '{contractID}') ");

            string sqlQuery = sb.ToString();
            obj.SaveData(sqlQuery);
        }

        // Устнавливает счетчик на цифру кол-ва ранее созданных объектов
        public void SetCountObj(IConnDataBaseSQL obj)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("USE LogisticsOVA; ");
            sb.Append("SELECT COUNT(1) FROM Customers");
            string sqlQuery = sb.ToString();

            customersCount = obj.ReadCountRowInTable(sqlQuery);
            Console.WriteLine("\n-------------------------------------------------------------------");
            Console.WriteLine(@"Кол-во строк в tаблице ""Customers"" - " + customersCount);
            Console.WriteLine("\n-------------------------------------------------------------------");
        }
    }

}
