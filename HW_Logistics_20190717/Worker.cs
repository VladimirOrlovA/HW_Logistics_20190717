using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_Logistics_20190717
{
    internal class Worker : Person
    {
        public int workerID { get; set; }
        public DateTime employmentDate { get; set; }
        public string position { get; set; }
        public int solary { get; set; }


        public Worker(string lastName, string firstName,  string middleName, DateTime birthday, 
                        long inn, int workerID, DateTime employmentDate, string position, int solary) 
            : base(lastName, firstName,  middleName, birthday, inn)
        {
            this.workerID = workerID;
            this.employmentDate = employmentDate;
            this.position = position;
            this.solary = solary;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public void InfoWorker()
        {
            Console.WriteLine("\n----------------- Информация о работнике -----------------\n\n");
            Console.WriteLine("Номер работника --------- " + workerID);
            InfoPerson();
            Console.WriteLine("Дата приема на работу --- " + employmentDate);
            Console.WriteLine("Должность --------------- " + position);
            Console.WriteLine("Оклад ------------------- " + solary);
            Console.WriteLine("\n---------------------------------------------------------\n\n");
        }

        // Формирует строку запроса в БД для создания таблицы
        public new string CreateTableQuery()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("USE Logistics; ");
            sb.Append("CREATE TABLE Workers (");
            sb.Append(" Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY, ");
            sb.Append(" workerID INT, ");
            sb.Append(" employmentDate DATE, ");
            sb.Append(" position NVARCHAR(50), ");
            sb.Append(" solary MONEY ");
            sb.Append("); ");
            string sql = sb.ToString();
            return sql;
        }

        // Формирует строку запроса в БД для вставки данных
        public string InsertTableQuery()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("USE Logistics; ");
            sb.Append("INSERT INTO Workers (workerID, employmentDate, position, solary) VALUES ");
            sb.Append($"('{workerID}', '{employmentDate.Year}-{employmentDate.Month}-{employmentDate.Day}', '{position}', '{solary}') ");
            string sql = sb.ToString();
            return sql;
        }

        // Формирует строку запроса в БД для вставки данных
        public new string ViewTableQuery()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("USE Logistics; ");
            sb.Append("SELECT * FROM Persons p ");
            sb.Append("JOIN Workers w ON w.workerID = p.id ;");
            string sql = sb.ToString();
            return sql;
        }

    }
}
