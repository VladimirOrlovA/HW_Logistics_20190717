using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HW_Logistics_20190717
{
    class Customers : IWriteToXML, IReadFromXML
    {
        private List<Customer> customersList = new List<Customer>();

        // Событие, возникающее при добавлении заказчика
        public event ObjectsStateHandler CustomerAdded;
        // Событие, возникающее при внесении заказчиков в БД
        public event ObjectsStateHandler CustomersAddedtoDB;

        // Добавление заказчика в список заказчиков
        public void AddCustomer(Customer obj)
        {
            customersList.Add(obj);
            if (CustomerAdded != null)
                CustomerAdded($"Добавлен новый заказчик - {obj.GetLastNameAndFM()}");
        }

        // Выводит информацию в консоль по содержимому каждого объекта листа
        public void Info()
        {
            for (int i = 0; i != customersList.Count; i++)
                customersList[i].Info();

            //foreach (var worker in workersList)
            //    worker.Info();
        }

        // Создает таблицу в БД
        public void CreateTable(IConnDataBaseSQL obj)
        {
            Console.WriteLine(@"Creating Table --- ""Customers""");

            StringBuilder sb = new StringBuilder();
            sb.Append("USE LogisticsOVA; ");
            sb.Append("CREATE TABLE Customers (");
            sb.Append(" customerID INT IDENTITY(1,1) NOT NULL PRIMARY KEY, ");
            sb.Append(" lastName NVARCHAR(50), ");
            sb.Append(" firstName NVARCHAR(50), ");
            sb.Append(" middleName NVARCHAR(50), ");
            sb.Append(" birthday DATE, ");
            sb.Append(" iin DECIMAL, ");
            sb.Append(" orderID INT, ");
            sb.Append(" registrationDate DATE NOT NULL DEFAULT CONVERT(DATE, GETDATE()) ");
            sb.Append("); ");
            string sqlQuery = sb.ToString();

            obj.SaveData(sqlQuery);

            if (CustomersAddedtoDB != null)
                CustomersAddedtoDB("Данные о заказчиках внесены в таблицу Customers базы данных.");
            //throw new NotImplementedException();
        }

        // Вносит данные в таблицу БД
        public void InsertTable(IConnDataBaseSQL obj)
        {
            Console.WriteLine(@"Insert Data to table ""Workers"" about "
                    + Convert.ToString(this.GetType()).Substring(22));

            StringBuilder sb = new StringBuilder();
            sb.Append("USE LogisticsOVA; ");
            sb.Append("INSERT INTO Customers (lastName, firstName, middleName, " +
                "birthday, iin, orderID) VALUES ");

            // объявляем переменную счетчика для подсчета кол-ва итерации, чтобы в запросе на последний
            // ввод строки в таблицу не ставить "," (обеспечение правильности синтаксиса запроса SQL)
            int count = 0;
            string sqlQuery = null;
            foreach (Customer i in customersList)
            {
                count++;
                sb.Append($"('{i.LastName}', '{i.FirstName}', '{i.MiddleName}', " +
                    $"'{i.birthday.Year}-{i.birthday.Month}-{i.birthday.Day}', '{i.iin}', '{i.orderID}') ");
                if (customersList.Count != count) sb.Append(", ");
            }

            sqlQuery = sb.ToString();
            obj.SaveData(sqlQuery);
            //throw new NotImplementedException();
        }

        // Выполняет вывод таблицы в консоль
        public void ViewTable(IConnDataBaseSQL obj)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("USE LogisticsOVA; ");
            sb.Append("SELECT * FROM Customers");
            string sqlQuery = sb.ToString();
            obj.ReadData(sqlQuery);
            //throw new NotImplementedException();
        }

        // Запись коллекции в файл XML
        public void DOMWriteXML(string filename)
        {
            // Создаем объектную модель
            XmlDocument doc = new XmlDocument();

            //Создаем заголовок XML 
            XmlDeclaration xmldecl = doc.CreateXmlDeclaration("1.0", null, null);

            //string lastName, string firstName, string middleName, DateTime birthday, long iin) //, int orderID)


            // Создаем основную ноду
            XmlElement node = doc.CreateElement("customers");
            foreach (Customer customer in customersList)
            {
                // Создаем ноду для заказчика
                XmlElement xmlcustomer = doc.CreateElement("customer");

                // Создаем артрибут фамилии 
                XmlAttribute xmlLastName = doc.CreateAttribute("lastName");
                xmlLastName.InnerText = customer.LastName;
                xmlcustomer.Attributes.Append(xmlLastName);

                // Создаем артрибут имени 
                XmlAttribute xmlFirstName = doc.CreateAttribute("firstName");
                xmlFirstName.InnerText = customer.FirstName;
                xmlcustomer.Attributes.Append(xmlFirstName);

                // Создаем артрибут отчества 
                XmlAttribute xmlMiddleName = doc.CreateAttribute("middleName");
                xmlMiddleName.InnerText = customer.MiddleName;
                xmlcustomer.Attributes.Append(xmlMiddleName);

                // Создаем артрибут даты рождения
                XmlAttribute xmlBirthday = doc.CreateAttribute("birthday");
                xmlBirthday.InnerText = Convert.ToString($"{customer.birthday.Year}-{customer.birthday.Month}-{customer.birthday.Day}");
                xmlcustomer.Attributes.Append(xmlBirthday);

                // Создаем артрибут ИИН
                XmlAttribute xmlIin = doc.CreateAttribute("iin");
                xmlIin.InnerText = Convert.ToString(customer.iin);
                xmlcustomer.Attributes.Append(xmlIin);

                // Добавляем артрибут работника в ноды списка
                node.AppendChild(xmlcustomer);
            }
            // Добавляем созданную структуру в корень
            doc.AppendChild(node);
            doc.InsertBefore(xmldecl, node);
            doc.Save(filename);

            Console.WriteLine("Display the modified XML...");
            doc.Save(Console.Out);
        }

        // Запись коллекции в файл XML
        public void DOMReadXML(string filename)
        {
            // Создаем объектную модель
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);

            // Выбираем необходимый список нод
            XmlNodeList list = doc.GetElementsByTagName("customer");
            foreach (XmlElement elem in list)
            {
                //string lastName, string firstName, string middleName, DateTime birthday,
                //        long iin, DateTime employmentDate, string position, int solary
                XmlNode attrib1 = elem.Attributes.GetNamedItem("lastName");
                XmlNode attrib2 = elem.Attributes.GetNamedItem("firstName");
                XmlNode attrib3 = elem.Attributes.GetNamedItem("middleName");
                XmlNode attrib4 = elem.Attributes.GetNamedItem("birthday");
                XmlNode attrib5 = elem.Attributes.GetNamedItem("iin");

                this.customersList.Add(new Customer(
                    attrib1.Value,
                    attrib2.Value,
                    attrib3.Value,
                    Convert.ToDateTime(attrib4.Value),
                    Convert.ToInt64(attrib5.Value)
                    ));
            }
        }
    }
}
