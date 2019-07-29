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

            Worker worker1 = new Worker("Нестеров", "Павел", "Николаевич", new DateTime(1994,10,12), 2586556655, new DateTime(2010,01,23), "экспедитор", 180000);
            Сustomer customer1 = new Сustomer("Нестеров", "Павел", "Николаевич", new DateTime(1994, 10, 12), 2586556655, 1);
            Order order1 = new Order(1, 1, 15.5, 10.8, 111,222, DateTime.Today, DateTime.Now);

            person1.InfoPerson();
            worker1.InfoWorker();
            customer1.InfoCustomer();
            order1.InfoOrder();

            Console.ReadKey();
        }
    }
}

