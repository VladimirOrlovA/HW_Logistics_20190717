using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_Logistics_20190717
{
    internal class Сustomer : Person, IWorkWithSQL
    {
        public int customerID { get; set; }
        public int orderID { get; set; }

        public Сustomer(string lastName, string firstName, string middleName, DateTime birthday, long inn, int customerID) //, int orderID)
            : base(lastName, firstName, middleName, birthday, inn)
        {
            this.customerID = customerID;
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

        public void InfoCustomer()
        {
            Console.WriteLine("\n----------------- Информация о заказчике -----------------\n\n");
            Console.WriteLine("Номер клиента ----------- " + customerID);
            InfoPerson();
            Console.WriteLine("\n---------------------------------------------------------\n\n");
        }

        // Формирует строку запроса в БД для создания таблицы
        public override string CreateTableQuery()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("USE Logistics; ");
            sb.Append("CREATE TABLE Customers (");
            sb.Append(" Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY, ");
            sb.Append(" customerID INT, ");
            //sb.Append(" orderID INT FOREIGN KEY (orderID) REFERENCES Orders (id), ");
            sb.Append(" orderID INT, ");
            sb.Append("); ");
            string sql = sb.ToString();
            return sql;
        }

        // Формирует строку запроса в БД для вставки данных
        public override string InsertTableQuery()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("USE Logistics; ");
            sb.Append("INSERT INTO Customers (customerID, orderID) VALUES ");
            sb.Append($"('{customerID}', '{orderID}') ");
            string sql = sb.ToString();
            return sql;
        }

        // Формирует строку запроса в БД для вставки данных
        public override string ViewTableQuery()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("USE Logistics; ");
            sb.Append("SELECT * FROM Persons p ");
            sb.Append("JOIN Customers c ON c.customerID = p.id ;");
            string sql = sb.ToString();
            return sql;
        }

    }

}
