using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_Logistics_20190717
{
    internal class Сustomer : Person
    {
        public int customerID;

        public Сustomer(string lastName, string firstName, string middleName, DateTime birthday, long inn, int customerID)
            : base(lastName, firstName, middleName, birthday, inn)
        {
            this.customerID = customerID;
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

        public void InfoWorker()
        {
            Console.WriteLine("\n----------------- Информация о заказчике -----------------\n\n");
            InfoPerson();
            Console.WriteLine("Номер клиента ------------- " + customerID);
            Console.WriteLine("\n---------------------------------------------------------\n\n");
        }
    }

}
