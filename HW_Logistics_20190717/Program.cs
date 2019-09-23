using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_Logistics_20190717
{
    delegate void ObjectsInfo();

    // Объявляем делегат для обработки событий
    public delegate void ObjectsStateHandler(string message);

    class Program
    {
        private static void Show_Message(string message)
        {
            Console.WriteLine(message);
        }

        static void Main(string[] args)
        {
            // создаем строку с данными для подключения к БД SQL
            ConnDataBaseSQL db = new ConnDataBaseSQL();

            // создаем базу данных если не существует
            //db.CreareDataBase();

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
            Orders orders = new Orders();
            // 1, 2, ...

            //==== делегаты ==== https://metanit.com/sharp/tutorial/3.13.php 

            ObjectsInfo objectsInfo;
            objectsInfo = employees.Info;
            objectsInfo += carriers.Info;
            objectsInfo += orders.Info;
              

            //==== события ==== https://metanit.com/sharp/tutorial/3.14.php

            employees.EmployeeAdded += Show_Message;
            employees.EmployeesAddedtoDB += Show_Message;

            customers.CustomerAdded += Show_Message;
            customers.CustomersAddedtoDB += Show_Message;

            carriers.CarrierAdded += Show_Message;
            customers.CustomersAddedtoDB += Show_Message;

            // ==================== создаем необходимые таблицы в БД ====================
            //employees.CreateTable(db);
            //customers.CreateTable(db);
            //carriers.CreateTable(db);
            //transports.CreateTable(db);
            //routes.CreateTable(db);
            // 1, 2, ...

            // ==================== Счетчики объектов ====================
            // установка начального значения для счетчика объектов, 
            //чтобы корректно отражать ID вновь созданных объектов в текущей сессии программы
            //employee.SetCountObj(db);
            //customer.SetCountObj(db);
            //carrier.SetCountObj(db);
            //transport.SetCountObj(db);

            // ==================== Создаем объекты ====================
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
            Carrier carrier4 = new Carrier("Запасов", "Тарас", "Олегович", new DateTime(1985, 05, 15), 850515100093);

            Transport transport1 = new Transport("Volkswagen_Caddy", 815, 3);
            Transport transport2 = new Transport("Газель-Фургон", 1500, 9);
            Transport transport3 = new Transport("Камаз-Автофургон", 10200, 35);

            Order order1 = new Order(1, 100, 0.5, "Алматы", "Усть-Каменогорск", Order.OrderStatuses.newOrder);

            // === создаем матрицу маршрутов и заполняем ее из файла ===
            //routes.FillArray();
            //routes.PrintArray();

            // ==================== Создаем списки объектов ====================

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
            carriers.AddCarrier(carrier4);

            transports.AddTransport(transport1);
            transports.AddTransport(transport2);
            transports.AddTransport(transport3);

            orders.AddOrder(order1);

            // ==================== Собираем маршрут перевозчику - формируем реальный маршрут ====================

            carrier1.AddRouteToСarrierRouteListByCityNamesAB(db, "Алматы", "Тараз");
            carrier1.AddRouteToСarrierRouteListByCityNamesAB(db, "Тараз", "Шымкент");
            carrier1.AddRouteToСarrierRouteListByCityNamesAB(db, "Шымкент", "Кызылорда");
            carrier1.AddRouteToСarrierRouteListByCityNamesAB(db, "Кызылорда", "Жезказган");
            carrier1.AddRouteToСarrierRouteListByCityNamesAB(db, "Жезказган", "Караганда");
            carrier1.AddRouteToСarrierRouteListByCityNamesAB(db, "Караганда", "Костанай");

            carrier2.AddRouteToСarrierRouteListByCityNamesAB(db, "Алматы", "Караганда");
            carrier2.AddRouteToСarrierRouteListByCityNamesAB(db, "Караганда", "Жезказган");
            carrier2.AddRouteToСarrierRouteListByCityNamesAB(db, "Жезказган", "Нур-Султан");
            carrier2.AddRouteToСarrierRouteListByCityNamesAB(db, "Нур-Султан", "Петропавловск");
            carrier2.AddRouteToСarrierRouteListByCityNamesAB(db, "Петропавловск", "Костанай");

            carrier3.AddRouteToСarrierRouteListByCityNamesAB(db, "Алматы", "Семей");
            carrier3.AddRouteToСarrierRouteListByCityNamesAB(db, "Семей", "Павлодар");
            carrier3.AddRouteToСarrierRouteListByCityNamesAB(db, "Павлодар", "Петропавловск");
            carrier3.AddRouteToСarrierRouteListByCityNamesAB(db, "Петропавловск", "Нур-Султан");
            carrier3.AddRouteToСarrierRouteListByCityNamesAB(db, "Нур-Султан", "Костанай");

            carrier4.AddRouteToСarrierRouteListByCityNamesAB(db, "Алматы", "Усть-Каменогорск");
            carrier4.AddRouteToСarrierRouteListByCityNamesAB(db, "Усть-Каменогорск", "Караганда");
            carrier4.AddRouteToСarrierRouteListByCityNamesAB(db, "Караганда", "Жезказган");
            carrier4.AddRouteToСarrierRouteListByCityNamesAB(db, "Жезказган", "Актобе");
            carrier4.AddRouteToСarrierRouteListByCityNamesAB(db, "Актобе", "Костанай");

            // ==================== Закрепляем транспорт за перевозчиком ====================
            carrier1.AddTransportToTransportsList(db, 1);
            carrier2.AddTransportToTransportsList(db, 2);
            carrier3.AddTransportToTransportsList(db, 3);
            carrier4.AddTransportToTransportsList(db, 1);

            // Запись одиночных объектов в таблицу SQL
            //employee1.InsertTable(db);
            //carrier1.InsertTable(db);
            //transport1.InsertTable(db);
            //route1.InsertTable(db);

            // запись коллекции объектов в таблицу SQL
            //employees.InsertTable(db);
            //customers.InsertTable(db);
            //routes.InsertTable(db);
            //transports.InsertTable(db);



            // Вывод информации об объекте
            //employees.Info();
            //customers.Info();
            carriers.Info();
            //transports.Info();
            //routes.InfoFromSQLtable();
            //routes.InfoFromSQLtable();
            //transports.InfoFromSQLtable();

            ///////////////////////////////////////////////////////////
            //order1.Info();
            //order1.ChangeOrderStatus(Order.OrderStatuses.closeOrder);
            //order1.Info();


            // !!!!!!!!!!!!!! Реализовать алгоритм Дейкстры !!!!!!!!!!!!

            //DijkstraAlgorithm da = new DijkstraAlgorithm();

            //da.Algorithm();


            // ========= Перегрузка базовых методов =========
            //Console.WriteLine($"Hash code for object {employee.GetType()} : {employee1.GetHashCode()}");
            //Console.WriteLine($"Hash code for object {employee.GetType()} : {employee2.GetHashCode()}");
            //Console.WriteLine($"Hash code for object {employee.GetType()} : {employee3.GetHashCode()}");
            //Console.WriteLine($"Hash code for object {employee.GetType()} : {employee4.GetHashCode()}");

            //Console.WriteLine("----------------------------------------------------------------");
            //Console.WriteLine(employee1.Equals(employee1));
            //Console.WriteLine("----------------------------------------------------------------");
            //Console.WriteLine(employee1.ToString());

            // === сортировки через обобщенные интерфейсы ===
            /*
            employees.Show();
            employees.employeesList.Sort();
            Console.WriteLine("===== сортировка фамилии =====");
            employees.Show();

            employees.Show();
            Console.WriteLine("===== сортировка по имени =====");
            employees.employeesList.Sort(new Employee.SortByName());
            employees.Show();

            employees.Show();
            Console.WriteLine("===== сортировка по возрасту =====");
            employees.employeesList.Sort(new Employee.SortByAge());
            employees.Show();
            */

            // === Вызов методов инфо через делегата ===
            //objectsInfo();

            Console.ReadKey();
        }
    }
}


