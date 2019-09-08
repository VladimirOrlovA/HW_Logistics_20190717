using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_Logistics_20190717
{
    class Customers
    {
        private List<Customer> customersList = new List<Customer>();

        // Добавление заказчика в список заказчиков
        public void AddCustomer(Customer obj)
        {
            customersList.Add(obj);
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
            sb.Append(" inn DECIMAL, ");
            sb.Append(" ContractID INT, ");
            sb.Append(" registrationDate DATE ");
            sb.Append("); ");
            string sqlQuery = sb.ToString();

            obj.SaveData(sqlQuery);

            //throw new NotImplementedException();
        }

        // Вставляет данные в таблицу БД
        public void InsertTable(IConnDataBaseSQL obj)
        {
            Console.WriteLine(@"Insert Data to table ""Workers"" about "
                    + Convert.ToString(this.GetType()).Substring(22));

            StringBuilder sb = new StringBuilder();
            sb.Append("USE LogisticsOVA; ");
            sb.Append("INSERT INTO Workers (lastName, firstName, middleName, birthday, inn, employmentDate, position, solary) VALUES ");

            // объявляем переменную счетчика для подсчета кол-ва итерации, чтобы в запросе на последний
            // ввод строки в таблицу не ставить "," (обеспечение правильности синтаксиса запроса SQL)
            int count = 0;
            string sqlQuery = null;
            foreach (Customer i in customersList)
            {
                count++;
                //sb.Append($"('{i.LastName}', '{i.FirstName}', '{i.MiddleName}', '{i.birthday.Year}-{i.birthday.Month}-{i.birthday.Day}', '{i.inn}'," +
                //    $" '{i.employmentDate.Year}-{i.employmentDate.Month}-{i.employmentDate.Day}', '{i.position}', '{i.solary}')");
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
            sb.Append("SELECT * FROM Workers p ");
            string sqlQuery = sb.ToString();
            obj.ReadData(sqlQuery);
            //throw new NotImplementedException();
        }
    }
}
