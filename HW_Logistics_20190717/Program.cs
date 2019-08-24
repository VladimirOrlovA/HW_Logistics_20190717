using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_Logistics_20190717
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            Person person1 = new Person("Орлов", "Владимир", "Александрович", new DateTime(1980, 07, 16), 20660716888);
            //first.FirstName = "Владимир";
            //first.LastName = "Орлов";
            //first.MiddleName = "Александрович";

            //Console.WriteLine("ФИО полностью \t" + first.GetLFM() + "\n");
            //Console.WriteLine("Фамилия и И.О. \t" + first.GetLastNameAndFM() + "\n\n");

            //Console.WriteLine("Дата рождения \t" + first.Birthday.ToShortDateString());
            //Console.WriteLine("ИНН \t\t" + first.Inn);
            //Console.WriteLine("Возраст \t" + first.Age());

            //Person second = new Person(new DateTime(1860, 01, 29), 20456885556);
            //second.FirstName = "Антон";
            //second.LastName = "Чехов";
            //second.MiddleName = "Павлович";

            //first.InfoPerson();
            //second.InfoPerson();

            Worker worker1 = new Worker("Нестеров", "Павел", "Николаевич", new DateTime(1994, 10, 12), 2586556655, new DateTime(2010, 01, 23), "экспедитор", 180000);
            Сustomer customer1 = new Сustomer("Быстров", "Олег", "Васильевич", new DateTime(2001, 01, 12), 2586556586, 1);
            Order order1 = new Order(1, 1, 15.5, 10.8, 111, 222, DateTime.Today, DateTime.Now);
            Route route1 = new Route(1, "Алматы", "Нур-Султан");
            Route route2 = new Route(2, "Нур-Султан", "Павлодар");
            Сarrier carrier1 = new Сarrier("FastTran");

            person1.InfoPerson();
            worker1.InfoWorker();
            customer1.InfoCustomer();
            order1.InfoOrder();
            route1.InfoRoute();
            carrier1.InfoCarrier();

            carrier1.addRoute(route1);
            carrier1.addRoute(route2);
            carrier1.InfoCarrier();

            worker1.InfoWorker();
            

            Console.WriteLine(Convert.ToString(worker1.GetType()).Substring(22));
            */

            //Person person1 = new Person("Орлов", "Владимир", "Александрович", new DateTime(1980, 07, 16), 20660716888);

            ConnDBSQL db = new ConnDBSQL();

            //db.CreateTable();
            //db.InsertTable();
            db.ViewTable();

            Console.ReadKey();
        }
    }
}

