using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_Logistics_20190717
{
    class Order
    {
        private static int ordersCount = 0;
        public int orderID { get; set; }
        public int customerID { get; set; }
        public double weight { get; set; }
        public double volume { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public int orderStatus { get; set; }
        public enum OrderStatuses
        {
            newOrder = 1,
            openOrder,
            closeOrder,
            cancelOrder
        }

        public Order() { }

        public Order(int customerID, double weight, double volume, string from, string to, OrderStatuses os)
        {
            this.orderID = ++ordersCount;
            this.customerID = customerID;
            this.weight = weight;
            this.volume = volume;
            this.from = from;
            this.to = to;
            this.startDate = DateTime.Today.ToString();
            this.endDate = null;
            switch (os)
            {
                case OrderStatuses.newOrder:
                    this.orderStatus = 1;
                    break;
                case OrderStatuses.openOrder:
                    this.orderStatus = 2;
                    break;
                case OrderStatuses.closeOrder:
                    this.orderStatus = 3;
                    break;
                case OrderStatuses.cancelOrder:
                    this.orderStatus = 4;
                    break;
            }
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

        public void ChangeOrderStatus(OrderStatuses os)
        {
            switch (os)
            {
                case OrderStatuses.newOrder:
                    this.orderStatus = 1;
                    break;
                case OrderStatuses.openOrder:
                    this.orderStatus = 2;
                    this.startDate = DateTime.Today.ToString();
                    break;
                case OrderStatuses.closeOrder:
                    this.orderStatus = 3;
                    this.endDate = DateTime.Today.ToString();
                    break;
                case OrderStatuses.cancelOrder:
                    this.orderStatus = 4;
                    break;
            }
        }

        public string OrderStatusToStr()
        {
            switch (orderStatus)
            {
                case 1:
                    return "новый";
                case 2:
                    return "выполняется";
                case 3:
                    return "выполнен";

                case 4:
                    return "отменен";
            }
            return "ошибка";
        }

        public string OrderDuration()
        {
            string str = null;
            if (orderStatus == 3)
                str = Convert.ToString(Convert.ToDateTime(endDate) - Convert.ToDateTime(startDate));
            return str;
        }

        public void Info()
        {

            Console.WriteLine("\n----------------- Информация о заказе -------------------\n\n");
            Console.WriteLine("Номер заказа ------------ " + orderID);
            Console.WriteLine("Номер клиента ----------- " + customerID);
            Console.WriteLine("Статус заказа ----------- " + OrderStatusToStr());
            Console.WriteLine("Вес посылки ------------- " + weight);
            Console.WriteLine("Объем посылки ----------- " + volume);
            Console.WriteLine("Откуда ------------------ " + from);
            Console.WriteLine("Куда -------------------- " + to);
            Console.WriteLine("Дата приема ------------- " + startDate);
            Console.WriteLine("Дата выдачи ------------- " + endDate);
            Console.WriteLine("Длительность заказа ----- " + OrderDuration());
            Console.WriteLine("\n---------------------------------------------------------\n\n");
        }

        // Вставляет данные в таблицу БД
        public void InsertTable(IConnDataBaseSQL obj)
        {
            Console.WriteLine(@"Insert Data to table ""Orders"" about "
               + Convert.ToString(this.GetType()).Substring(22));

            StringBuilder sb = new StringBuilder();
            sb.Append("USE LogisticsOVA; ");
            sb.Append("INSERT INTO Orders (customerID, weight, volume, from, to, " +
                "startDate, endDate, orderStatus) VALUES ");
            sb.Append($"('{customerID}', '{weight}', '{volume}', '{from}', '{to}'," +
                $" '{startDate}', '{endDate}', '{orderStatus}') ");

            string sqlQuery = sb.ToString();

            obj.SaveData(sqlQuery);

        //public int customerID { get; set; }
        //public double weight { get; set; }
        //public double volume { get; set; }
        //public string from { get; set; }
        //public string to { get; set; }
        //public string startDate { get; set; }
        //public string endDate { get; set; }
        //public int orderStatus { get; set; }

    }

    // Устнавливает счетчик на цифру кол-ва ранее созданных объектов
    public void SetCountObj(IConnDataBaseSQL obj)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("USE LogisticsOVA; ");
            sb.Append("SELECT COUNT(1) FROM Orders");
            string sqlQuery = sb.ToString();

            ordersCount = obj.ReadCountRowInTable(sqlQuery);
            Console.WriteLine("\n-------------------------------------------------------------------");
            Console.WriteLine(@"Кол-во строк в tаблице ""Routes"" - " + ordersCount);
            Console.WriteLine("\n-------------------------------------------------------------------");
        }

    }
}
