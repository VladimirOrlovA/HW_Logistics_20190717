using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_Logistics_20190717
{
    internal class Customer : Person
    {
        private static int customerCount = 0;
        public int customerID { get; set; }
        public int contractID { get; set; }

        public Customer() { }

        public Customer(string lastName, string firstName, string middleName, DateTime birthday, long inn) //, int orderID)
            : base(lastName, firstName, middleName, birthday, inn)
        {
            customerID = ++customerCount;
            //this.orderID = orderID;
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
            sb.Append("INSERT INTO Customers (lastName, firstName, middleName, birthday, inn, employmentDate, position, solary) VALUES ");
            //sb.Append($"('{lastName}', '{firstName}', '{middleName}', '{birthday.Year}-{birthday.Month}-{birthday.Day}', '{inn}'," +
            //    $" '{employmentDate.Year}-{employmentDate.Month}-{employmentDate.Day}', '{position}', '{solary}') ");
            string sqlQuery = sb.ToString();

            obj.SaveData(sqlQuery);
        }

        //устнавливает счетчик на цифру кол-ва ранее созданных объектов
        public void SetCountObj(IConnDataBaseSQL obj)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("USE LogisticsOVA; ");
            sb.Append("SELECT COUNT(1) FROM Customers");
            string sqlQuery = sb.ToString();

            customerCount = obj.ReadCountRowInTable(sqlQuery);
            Console.WriteLine("\n-------------------------------------------------------------------");
            Console.WriteLine(@"Кол-во строк в tаблице ""Customers"" - " + customerCount);
            Console.WriteLine("\n-------------------------------------------------------------------");
        }
    }

}
