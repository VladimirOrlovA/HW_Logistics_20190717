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
            Employee employee = new Employee();
            Employees employees = new Employees();

            Customer customer = new Customer();
            Customers customers = new Customers();

            Carrier carrier = new Carrier();
            Carriers carriers = new Carriers();

            Transport transport = new Transport();
            Transports transports = new Transports();

            Route route = new Route();
            Routes routes = new Routes();

            Order order = new Order();
            // 1, 2, ...


            // создаем необходимые таблицы в БД
            employees.CreateTable(db);
            customers.CreateTable(db);
            carriers.CreateTable(db);
            transports.CreateTable(db);
            routes.CreateTable(db);
            // 1, 2, ...


            // установка начального значения для счетчика объектов, 
            //чтобы корректно отражать ID вновь созданных объектов в текущей сессии программы
            employee.SetCountObj(db);
            customer.SetCountObj(db);
            carrier.SetCountObj(db);
            transport.SetCountObj(db);
            route.SetCountObj(db);

            // Создаем объекты
            Employee employee1 = new Employee("Селезнев", "Павел", "Федорович", new DateTime(1981, 01, 08), 810108100011, new DateTime(2010, 01, 23), "менеджер", 180000);
            Employee employee2 = new Employee("Подорогин", "Семен", "Петрович", new DateTime(1992, 08, 25), 920825100021, new DateTime(2010, 01, 23), "экспедитор", 120000);
            Employee employee3 = new Employee("Синицына", "Анна", "Дмитриевна", new DateTime(1994, 03, 18), 940318200031, new DateTime(2010, 01, 23), "техник", 80000);
            Employee employee4 = new Employee("Нестеров", "Павел", "Николаевич", new DateTime(2003, 10, 12), 031012300041, new DateTime(2010, 01, 23), "экспедитор", 180000);

            Customer customer1 = new Customer("Быстров", "Олег", "Васильевич", new DateTime(2001, 01, 01), 010101300051);
            Customer customer2 = new Customer("Хотелов", "Василий", "Генадьевич", new DateTime(2003, 12, 01), 031201300061);
            Customer customer3 = new Customer("Подавайкин", "Иван", "Петрович", new DateTime(2012, 02, 25), 120225300071);
            Customer customer4 = new Customer("Занудов", "Петр", "Алексеевич", new DateTime(2008, 08, 12), 080812300081);
            Customer customer5 = new Customer("Хорошев", "Андрей", "Юрьевич", new DateTime(1985, 03, 21), 850321100091);

            Carrier carrier1 = new Carrier("Скороходов", "Павел", "Васильевич", new DateTime(1985, 01, 01), 850101100051);
            Carrier carrier2 = new Carrier("Тяжеловесов", "Григорий", "Генадьевич", new DateTime(1991, 12, 01), 911201100061);
            Carrier carrier3 = new Carrier("Далеков", "Валентин", "Петрович", new DateTime(1987, 02, 25), 870225100071);

            Transport transport1 = new Transport("Volkswagen_Caddy", 815, 3);
            Transport transport2 = new Transport("Газель-Фургон", 1500, 9);
            Transport transport3 = new Transport("Камаз-Автофургон", 10200, 35);
            

            Route route1 = new Route("Алматы", "Шымкент", 845);
            Route route2 = new Route("Алматы", "Тараз", 499);
            Route route3 = new Route("Алматы", "Нур-Султан", 1217);

            Order order1 = new Order(1, 100, 0.5, "Алматы", "Шымкент", Order.OrderStatuses.newOrder);


            // Создаем списки объектов

            employees.AddEmployee(employee1);
            employees.AddEmployee(employee2);
            employees.AddEmployee(employee3);
            employees.AddEmployee(employee4);

            customers.AddCustomer(customer1);
            customers.AddCustomer(customer2);
            customers.AddCustomer(customer3);
            customers.AddCustomer(customer4);
            customers.AddCustomer(customer5);

            carriers.AddCarrier(carrier1);
            carriers.AddCarrier(carrier2);
            carriers.AddCarrier(carrier3);

            transports.AddTransport(transport1);
            transports.AddTransport(transport2);
            transports.AddTransport(transport3);

            routes.addRoute(route1);
            routes.addRoute(route2);
            routes.addRoute(route3);

            // Назначаем транспорт перевозчику
            //transport1.carrierTransportID = 1;  // при назначении сделать UpDate данных в таблице !!!
            //transport1.carrierTransportID = 2;
            //transport1.carrierTransportID = 3;

            // Назначаем маршрут перевозчику
            //route1.carrierRouteID = 1;
            //route2.carrierRouteID = 2;
            //route3.carrierRouteID = 3;

            // Запись одиночных объектов в таблицу SQL
            //employee1.InsertTable(db);
            //carrier1.InsertTable(db);
            //transport1.InsertTable(db);
            //route1.InsertTable(db);



            //employees.Info();
            //customers.Info();
            // carriers.Info();
            //transports.Info();
            //routes.Info();
            order1.Info();
            order1.ChangeOrderStatus(Order.OrderStatuses.closeOrder);
            order1.Info();


            // запись объектов в таблицу SQL
            //worker1.InsertTable(db);
            //workers.InsertTable(db);

            //customer1.InsertTable(db);
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

