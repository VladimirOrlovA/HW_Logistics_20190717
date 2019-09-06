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
            // создаем строку с данными для подключения к БД SQL
            ConnDBSQL db = new ConnDBSQL();

            // создаем базу данных если не существует
            db.CreareDataBase();

            // создаем основные объекты для работы программы
            Workers wr = new Workers();
            // 1, 2, ...

            // создаем таблицу для Workers
            db.CreateTable(wr);
            // 1, 2, ...

            //Person person1 = new Person("Орлов", "Владимир", "Александрович", new DateTime(1980, 07, 16), 20660716888);
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

            //Worker worker1 = new Worker("Нестеров", "Павел", "Николаевич", new DateTime(1994, 10, 12), 2586556655, 1, new DateTime(2010, 01, 23), "экспедитор", 180000);
            //Сustomer customer1 = new Сustomer("Быстров", "Олег", "Васильевич", new DateTime(2001, 01, 12), 2586556586, 2);

            //Order order1 = new Order(1, 1, 15.5, 10.8, 111, 222, DateTime.Today, DateTime.Now);
            //Route route1 = new Route(1, "Алматы", "Нур-Султан");
            //Route route2 = new Route(2, "Нур-Султан", "Павлодар");
            //Сarrier carrier1 = new Сarrier("FastTran");

            //person1.InfoPerson();
            //worker1.InfoWorker();
            //customer1.InfoCustomer();
            //order1.InfoOrder();
            //route1.InfoRoute();
            //carrier1.InfoCarrier();

            //carrier1.addRoute(route1);
            //carrier1.addRoute(route2);
            //carrier1.InfoCarrier();

            //worker1.InfoWorker();
            //customer1.InfoCustomer();

            //Console.WriteLine(Convert.ToString(worker1.GetType()).Substring(22));



            //Worker workerRUS = new Worker("Нестеров", "Павел", "Николаевич", new DateTime(1994, 10, 12), 2586556655, 1, new DateTime(2010, 01, 23), "экспедитор", 180000);
            //Worker workerUS = new Worker("Nesterov", "Pavel", "Nikolaevich", new DateTime(1994, 10, 12), 2586556655, 1, new DateTime(2010, 01, 23), "экспедитор", 180000);
            //Сustomer customer1 = new Сustomer("Быстров", "Олег", "Васильевич", new DateTime(2001, 01, 12), 2586556586, 2);

            //db.CreateTable(person1.CreateTableQuery());
            //db.CreateTable(worker1.CreateTableQuery());
            //db.CreateTable(customer1.CreateTableQuery());

            //db.InsertTable(worker1.InsertTableQueryPerson());
            //db.InsertTable(worker1.InsertTableQuery());

            //db.InsertTable(customer1.InsertTableQueryPerson());
            //db.InsertTable(customer1.InsertTableQuery());

            //db.ViewTable(worker1.ViewTableQuery());
            //db.ViewTable(customer1.ViewTableQuery());

            //Workers wr = new Workers();
            //db.ReadTable(wr);

            //Worker worker1 = new Worker("Брежнев", "Павел", "Федорович", new DateTime(1981, 01, 08), 2586556655, new DateTime(2010, 01, 23), "менеджер", 180000);
            //Worker worker2 = new Worker("Андропов", "Семен", "Петрович", new DateTime(1992, 08, 25), 2586556655, new DateTime(2010, 01, 23), "экспедитор", 120000);
            //Worker worker3 = new Worker("Горбачев", "Артем", "Дмитриевич", new DateTime(1994, 03, 18), 2586556655, new DateTime(2010, 01, 23), "техник", 80000);

            //db.ReadTable(wr);

            //wr.AddWorker(worker1);
            //wr.AddWorker(worker2);
            //wr.AddWorker(worker3);

            //wr.AddWorkerI(worker1);
            //wr.AddWorkerI(worker2);

            //wr.Info();


            //db.CreateTable(wr);
            //db.InsertTable(wr);

            //db.ViewTable(wr);
            //db.InsertTable(wr);
            //db.ViewTable(wr);
            //Console.WriteLine("----------------------------------------------------");
            //db.InsertTable(worker1);
            //db.ViewTable(worker1);

            // ====== Реализовать загрузку данных из таблиц при запуске программы


            //db.CreateTable(wr);
            //db.CreateTable(worker1);

            //Console.WriteLine(workerUS.LastName);


            Console.ReadKey();
        }
    }
}

