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
            ConnDataBaseSQL db = new ConnDataBaseSQL();

            // создаем базу данных если не существует
            db.CreareDataBase();

            // создаем основные объекты для работы программы
            //Worker worker = new Worker();
            Worker worker = new Worker();
            Workers workers = new Workers();

            Customer customer = new Customer();
            Customers customers = new Customers();
            // 1, 2, ...


            // создаем необходимые таблицы в БД
            workers.CreateTable(db);
            customers.CreateTable(db);
            // 1, 2, ...


            // установка начального значения для счетчика объектов, 
            //чтобы корректно отражать ID вновь созданных объектов в текущей сессии программы
            worker.SetCountObj(db);
            customer.SetCountObj(db);

            // создание объектов, а затем их запись в объект со списком объектов
            Worker worker1 = new Worker("Селезнев", "Павел", "Федорович", new DateTime(1981, 01, 08), 810108100011, new DateTime(2010, 01, 23), "менеджер", 180000);
            Worker worker2 = new Worker("Подорогин", "Семен", "Петрович", new DateTime(1992, 08, 25), 920825100021, new DateTime(2010, 01, 23), "экспедитор", 120000);
            Worker worker3 = new Worker("Синицына", "Анна", "Дмитриевна", new DateTime(1994, 03, 18), 940318200031, new DateTime(2010, 01, 23), "техник", 80000);
            Worker worker4 = new Worker("Нестеров", "Павел", "Николаевич", new DateTime(2003, 10, 12), 031012300041, new DateTime(2010, 01, 23), "экспедитор", 180000);

            Customer customer1 = new Customer("Быстров", "Олег", "Васильевич", new DateTime(2001, 01, 01), 010101300051);
            Customer customer2 = new Customer("Хотелов", "Василий", "Генадьевич", new DateTime(2003, 12, 01), 031201300061);
            Customer customer3 = new Customer("Подавайкин", "Иван", "Петрович", new DateTime(2012, 02, 25), 120225300071);
            Customer customer4 = new Customer("Занудов", "Петр", "Алексеевич", new DateTime(2008, 08, 12), 080812300081);
            Customer customer5 = new Customer("Хорошев", "Андрей", "Юрьевич", new DateTime(1985, 03, 21), 850321100091);


            //workers.AddWorker(worker1);
            workers.AddWorker(worker2);
            workers.AddWorker(worker3);
            workers.AddWorker(worker4);

            //customers.AddCustomer(customer1);
            customers.AddCustomer(customer2);
            customers.AddCustomer(customer3);
            customers.AddCustomer(customer4);
            customers.AddCustomer(customer5);


            workers.Info();
            customers.Info();

            // запись объектов в таблицу SQL
            worker1.InsertTable(db);
            //workers.InsertTable(db);

            customer1.InsertTable(db);
            //customers.InsertTable(db);

            // ----------------------------------------------------------------------------------------


            // загрузка данных из таблиц БД в объекты программы
            //workers = db.LoadData(workers);







            //Worker worker1 = new Worker("Нестеров", "Павел", "Николаевич", new DateTime(1994, 10, 12), 2586556655, 1, new DateTime(2010, 01, 23), "экспедитор", 180000);

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



            //workers.AddWorker(worker1);
            //workers.AddWorker(worker2);
            //workers.AddWorker(worker3);
            //workers.AddWorker(workerRUS);


            //wr.AddWorkerI(worker1);
            //wr.AddWorkerI(worker2);

            //workers.Info();

            //db.InsertTable(workers);

            //workers.Info();

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

