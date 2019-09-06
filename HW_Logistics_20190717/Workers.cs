using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_Logistics_20190717
{
    class Workers : ICountObj
    {
        private List<Worker> workersList = new List<Worker>();

        // Добавление работника в список работников
        public void AddWorker(Worker obj)
        {
            workersList.Add(obj);
        }

        // Добавление работника в список работников через интерфейс
        public void AddWorkerI(IAddWorkerToWorkers obj)
        {
            workersList.Add(obj.ThisWorker());
        }

        // получаем информацию по содержимому каждого объекта листа
        public void Info()
        {
            for (int i = 0; i != workersList.Count; i++)
                workersList[i].Info();

            //foreach (var worker in workersList)
            //    worker.Info();
        }

        // Возвращаем текущее кол-во записей объектов в листе
        int ICountObj.CountObj()
        {
            return workersList.Count;
            //throw new NotImplementedException();
        }

        // Формирует строку запроса в БД для создания таблицы
        public void CreateTable(IConnDataBaseSQL obj)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("USE LogisticsOVA; ");
            sb.Append("CREATE TABLE Workers (");
            sb.Append(" workerID INT NOT NULL, ");
            sb.Append(" lastName NVARCHAR(50), ");
            sb.Append(" firstName NVARCHAR(50), ");
            sb.Append(" middleName NVARCHAR(50), ");
            sb.Append(" birthday DATE, ");
            sb.Append(" inn DECIMAL, ");
            sb.Append(" employmentDate DATE, ");
            sb.Append(" position NVARCHAR(50), ");
            sb.Append(" solary INT ");
            sb.Append("); ");
            string sqlQuery = sb.ToString();
            obj.CreateTable(sqlQuery);
            //throw new NotImplementedException();
        }

        // Формирует строку запроса в БД для вставки данных в таблицу
        public void InsertTable(IConnDataBaseSQL obj)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("USE LogisticsOVA; ");
            sb.Append("INSERT INTO Workers (workerID, lastName, firstName, middleName, birthday, inn, employmentDate, position, solary) VALUES ");

            // объявляем переменную счетчика для подсчета кол-ва итерации, чтобы в запросе на последний
            // ввод строки в таблицу не ставить "," (обеспечение правильности синтаксиса запроса SQL)
            int count = 0;
            string sqlQuery = null;
            foreach (Worker i in workersList)
            {
                count++;
                sb.Append($"('{i.workerID}', '{i.LastName}', '{i.FirstName}', '{i.MiddleName}', '{i.birthday.Year}-{i.birthday.Month}-{i.birthday.Day}', '{i.inn}'," +
                    $" '{i.employmentDate.Year}-{i.employmentDate.Month}-{i.employmentDate.Day}', '{i.position}', '{i.solary}')");
                if (workersList.Count != count) sb.Append(", ");
            }

            sqlQuery = sb.ToString();
            obj.InsertTable(sqlQuery);
            //throw new NotImplementedException();
        }

        // Формирует строку запроса в БД для чтения данных из таблицы
        public void ViewTable(IConnDataBaseSQL obj)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("USE LogisticsOVA; ");
            sb.Append("SELECT * FROM Workers p ");
            string sqlQuery = sb.ToString();
            obj.InsertTable(sqlQuery);
            //throw new NotImplementedException();
        }

    }
}
