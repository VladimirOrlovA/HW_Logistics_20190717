using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_Logistics_20190717
{
    class Order
    {
        public int orderID { get; set; }
        public int customerID { get; set; }
        public double weight { get; set; }
        public double volume { get; set; }
        public int from { get; set; }
        public int to { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }

        public Order(int orderID, int customerID, double weight, double volume, int from, int to, DateTime startDate, DateTime endDate)
        {
            this.orderID = orderID;
            this.customerID = customerID;
            this.weight = weight;
            this.volume = volume;
            this.from = from;
            this.to = to;
            this.startDate = startDate;
            this.endDate = endDate;
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

        public void InfoOrder()
        {
            Console.WriteLine("\n----------------- Информация о заказе -----------------\n\n");
            Console.WriteLine("Номер заказа ------------ " + orderID);
            Console.WriteLine("Номер клиента ----------- " + customerID);
            Console.WriteLine("Вес посылки ------------- " + weight);
            Console.WriteLine("Объем посылки ----------- " + volume);
            Console.WriteLine("Откуда ------------------ " + from);
            Console.WriteLine("Куда -------------------- " + to);
            Console.WriteLine("Дата приема ------------- " + startDate);
            Console.WriteLine("Дата выдачи ------------- " + endDate);
            Console.WriteLine("Длительность заказа ----- " + (endDate - startDate));
            Console.WriteLine("\n---------------------------------------------------------\n\n");
        }

    }
}
