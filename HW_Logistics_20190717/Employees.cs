using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_Logistics_20190717
{
    class Employees
    {
        public List<Employee> employeesList = new List<Employee>();

        // Добавляет работника в список работников
        public void AddEmployee(Employee obj)
        {
            employeesList.Add(obj);
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

    }
}
