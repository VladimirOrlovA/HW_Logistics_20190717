using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_Logistics_20190717
{
    class Workers: IWorkWithSQL
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

        public void Info()
        {
            foreach (var worker in workersList)
                worker.Info();
        }

        // Формирует строку запроса в БД для создания таблицы
        string IWorkWithSQL.CreateTableQuery()
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
            return sqlQuery;
            //throw new NotImplementedException();
        }

        //Workers IWorkWithSQL.CreateTmpObj()
        //{
        //    Workers wrks = new Workers();
        //    return wrks;
        //    //throw new NotImplementedException();
        //}

        // Формирует строку запроса в БД для вставки данных в таблицу
        string IWorkWithSQL.InsertTableQuery()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("USE LogisticsOVA; ");
            sb.Append("INSERT INTO Workers (workerID, lastName, firstName, middleName, birthday, inn, employmentDate, position, solary) VALUES ");

            int count = 0;
            foreach (Worker i in workersList)
            {
                count++;
                sb.Append($"('{i.workerID}', '{i.LastName}', '{i.FirstName}', '{i.MiddleName}', '{i.birthday.Year}-{i.birthday.Month}-{i.birthday.Day}', '{i.inn}'," +
                    $" '{i.employmentDate.Year}-{i.employmentDate.Month}-{i.employmentDate.Day}', '{i.position}', '{i.solary}')");
                if(workersList.Count != count) sb.Append(", ");
            }
                string sqlQuery = sb.ToString();
            Console.WriteLine(sqlQuery);
            return sqlQuery;

            //throw new NotImplementedException();
        }

        // Формирует строку запроса в БД для чтения данных из таблицы
        string IWorkWithSQL.ViewTableQuery()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("USE LogisticsOVA; ");
            sb.Append("SELECT * FROM Workers p ");
            string sqlQuery = sb.ToString();
            return sqlQuery;
            //throw new NotImplementedException();
        }
    }
}
