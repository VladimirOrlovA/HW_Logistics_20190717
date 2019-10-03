using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;

namespace HW_Logistics_20190717
{
    
    class Employees: IWriteToXML, IReadFromXML
    {
        public List<Employee> employeesList = new List<Employee>();

        // Событие, возникающее при добавлении сотрудника
        public event ObjectsStateHandler EmployeeAdded;
        // Событие, возникающее при внесении сотрудников в БД
        public event ObjectsStateHandler EmployeesAddedtoDB;

        // Добавляет работника в список работников
        public void AddEmployee(Employee obj)
        {
            employeesList.Add(obj);
            if (EmployeeAdded != null)
                EmployeeAdded($"Добавлен новый сотрудник - {obj.GetLastNameAndFM()}");
        }

        // Выводит информацию в консоль по содержимому каждого объекта листа
        public void Info()
        {
            for (int i = 0; i != employeesList.Count; i++)
                employeesList[i].Info();

            //foreach (var employee in employeesList)
            //    employee.Info();
        }

        public void Show()
        {
            foreach (Person obj in employeesList)
                Console.WriteLine(obj);
            Console.WriteLine("---");
        }
        // Создает таблицу в БД
        public void CreateTable(IConnDataBaseSQL obj)
        {
            Console.WriteLine(@"Creating Table --- ""Employees""");

            StringBuilder sb = new StringBuilder();
            sb.Append("USE LogisticsOVA; ");
            sb.Append("CREATE TABLE Employees (");
            sb.Append(" employeeID INT IDENTITY(1,1) NOT NULL PRIMARY KEY, ");
            sb.Append(" lastName NVARCHAR(50), ");
            sb.Append(" firstName NVARCHAR(50), ");
            sb.Append(" middleName NVARCHAR(50), ");
            sb.Append(" birthday DATE, ");
            sb.Append(" iin DECIMAL, ");
            sb.Append(" employmentDate DATE, ");
            sb.Append(" position NVARCHAR(50), ");
            sb.Append(" solary INT ");
            sb.Append("); ");
            string sqlQuery = sb.ToString();

            obj.SaveData(sqlQuery);

            if (EmployeesAddedtoDB != null)
                EmployeesAddedtoDB("Данные о сотрудниках внесены в таблицу Employees базы данных.");
            //throw new NotImplementedException();
        }

        // Вносит данные в таблицу БД
        public void InsertTable(IConnDataBaseSQL obj)
        {
            Console.WriteLine(@"Insert Data to table ""Employees"" about "
                    + Convert.ToString(this.GetType()).Substring(22));

            StringBuilder sb = new StringBuilder();
            sb.Append("USE LogisticsOVA; ");
            sb.Append("INSERT INTO Employees (lastName, firstName, middleName, birthday, iin, employmentDate, position, solary) VALUES ");

            // объявляем переменную счетчика для подсчета кол-ва итерации, чтобы в запросе на последний
            // ввод строки в таблицу не ставить "," (обеспечение правильности синтаксиса запроса SQL)
            int count = 0;
            string sqlQuery = null;
            foreach (Employee i in employeesList)
            {
                count++;
                sb.Append($"('{i.LastName}', '{i.FirstName}', '{i.MiddleName}', '{i.birthday.Year}-{i.birthday.Month}-{i.birthday.Day}', '{i.iin}'," +
                    $" '{i.employmentDate.Year}-{i.employmentDate.Month}-{i.employmentDate.Day}', '{i.position}', '{i.solary}')");
                if (employeesList.Count != count) sb.Append(", ");
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
            sb.Append("SELECT * FROM Employees ");
            string sqlQuery = sb.ToString();
            obj.ReadData(sqlQuery);
            //throw new NotImplementedException();
        }

        public void DOMWriteXML(string filename)
        {
            // Создаем объектную модель
            XmlDocument doc = new XmlDocument();

            //Создаем заголовок XML 
            XmlDeclaration xmldecl = doc.CreateXmlDeclaration("1.0", null, null);

            // Создаем основную ноду
            XmlElement node = doc.CreateElement("employees");
            foreach (Employee employee in employeesList)
            {
                // Создаем ноду для сотрудника
                XmlElement xmlEmployee = doc.CreateElement("employee");

                // Создаем ноду фамилии 
                XmlElement xmlLastName = doc.CreateElement("lastName");
                xmlLastName.InnerText = employee.LastName;
                xmlEmployee.AppendChild(xmlLastName);

                // Создаем ноду имени 
                XmlElement xmlFirstName = doc.CreateElement("firstName");
                xmlFirstName.InnerText = employee.FirstName;
                xmlEmployee.AppendChild(xmlFirstName);

                // Создаем ноду отчества 
                XmlElement xmlMiddleName = doc.CreateElement("middleName");
                xmlMiddleName.InnerText = employee.MiddleName;
                xmlEmployee.AppendChild(xmlMiddleName);

                // Создаем ноду даты рождения
                XmlElement xmlBirthday = doc.CreateElement("birthday");
                xmlBirthday.InnerText = Convert.ToString($"{employee.birthday.Year}-{employee.birthday.Month}-{employee.birthday.Day}");
                xmlEmployee.AppendChild(xmlBirthday);

                // Создаем ноду ИИН
                XmlElement xmlIin = doc.CreateElement("iin");
                xmlIin.InnerText = Convert.ToString(employee.iin);
                xmlEmployee.AppendChild(xmlIin);

                // Создаем ноду идентификатор сотрудника
                //XmlElement xmlEmployeeID = doc.CreateElement("employeeID");
                //xmlEmployeeID.InnerText = Convert.ToString(employee.employeeID);
                //xmlEmployee.AppendChild(xmlEmployeeID);

                // Создаем ноду даты приема на работу 
                XmlElement xmlEmploymentDate = doc.CreateElement("employmentDate");
                xmlEmploymentDate.InnerText = Convert.ToString($"{employee.employmentDate.Year}-{employee.employmentDate.Month}-{employee.employmentDate.Day}");
                xmlEmployee.AppendChild(xmlEmploymentDate);

                // Создаем ноду даты должности
                XmlElement xmlPosition = doc.CreateElement("position");
                xmlPosition.InnerText = employee.position;
                xmlEmployee.AppendChild(xmlPosition);

                // Создаем ноду оклада
                XmlElement xmlSolary = doc.CreateElement("solary");
                xmlSolary.InnerText = Convert.ToString(employee.position);
                xmlEmployee.AppendChild(xmlSolary);

                // Добавляем ноду работника в ноды списка
                node.AppendChild(xmlEmployee);
            }
            // Добавляем созданную структуру в корень
            doc.AppendChild(node);
            doc.InsertBefore(xmldecl, node);
            doc.Save(filename);
        }

        public void DOMReadXML(string filename)
        {
            // Создаем объектную модель
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);

            // Выбираем необходимый список нод
            XmlNodeList list = doc.GetElementsByTagName("employee");
            foreach (XmlElement elem in list)
            {
                //string lastName, string firstName, string middleName, DateTime birthday,
                //        long iin, DateTime employmentDate, string position, int solary
                XmlNodeList lastNamelist = elem.GetElementsByTagName("lastName");
                XmlNodeList firstNamelist = elem.GetElementsByTagName("firstName");
                XmlNodeList middleNamelist = elem.GetElementsByTagName("middleName");
                XmlNodeList birthdaylist = elem.GetElementsByTagName("birthday");
                XmlNodeList iinlist = elem.GetElementsByTagName("iin");

                //XmlNodeList employeeIDlist = elem.GetElementsByTagName("employeeID");

                XmlNodeList employmentDatelist = elem.GetElementsByTagName("employmentDate");
                XmlNodeList positionlist = elem.GetElementsByTagName("position");
                XmlNodeList solarylist = elem.GetElementsByTagName("solary");

                this.employeesList.Add(new Employee(
                    lastNamelist.Item(0).InnerText,
                    firstNamelist.Item(0).InnerText,
                    middleNamelist.Item(0).InnerText,
                    Convert.ToDateTime(birthdaylist.Item(0).InnerText),
                    Convert.ToInt64(iinlist.Item(0).InnerText),
                    //Convert.ToInt32(employeeIDlist.Item(0).InnerText), 
                    Convert.ToDateTime(employmentDatelist.Item(0).InnerText),
                    positionlist.Item(0).InnerText,
                    Convert.ToInt32(solarylist.Item(0).InnerText)));
            }
        }
    }
}
