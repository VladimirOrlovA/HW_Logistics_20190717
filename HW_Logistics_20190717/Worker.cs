using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_Logistics_20190717
{
    class Worker : Person
    {
        private static int workersCount = 0;
        public int workerID { get; set; }
        public DateTime employmentDate { get; set; }
        public string position { get; set; }
        public int solary { get; set; }

        public Worker() { }

        public Worker(string lastName, string firstName, string middleName, DateTime birthday,
                        long inn, DateTime employmentDate, string position, int solary)
            : base(lastName, firstName, middleName, birthday, inn)
        {
            workerID = ++workersCount;
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

        public override void Info()
        {
            Console.WriteLine("\n----------------- Информация о работнике -----------------\n\n");
            Console.WriteLine("Номер работника --------- " + workerID);
            InfoPerson();
            Console.WriteLine("Дата приема на работу --- " + employmentDate);
            Console.WriteLine("Должность --------------- " + position);
            Console.WriteLine("Оклад ------------------- " + solary);
            Console.WriteLine("\n---------------------------------------------------------\n\n");
        }

        // Вставляет данные в таблицу БД
        public void InsertTable(IConnDataBaseSQL obj)
        {
            Console.WriteLine(@"Insert Data to table ""Workers"" about "
               + Convert.ToString(this.GetType()).Substring(22));

            StringBuilder sb = new StringBuilder();
            sb.Append("USE LogisticsOVA; ");
            sb.Append("INSERT INTO Workers (lastName, firstName, middleName, birthday, inn, employmentDate, position, solary) VALUES ");
            sb.Append($"('{lastName}', '{firstName}', '{middleName}', '{birthday.Year}-{birthday.Month}-{birthday.Day}', '{inn}'," +
                $" '{employmentDate.Year}-{employmentDate.Month}-{employmentDate.Day}', '{position}', '{solary}') ");
            string sqlQuery = sb.ToString();

            obj.SaveData(sqlQuery);
        }

        // Устнавливает счетчик на цифру кол-ва ранее созданных объектов
        public void SetCountObj(IConnDataBaseSQL obj)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("USE LogisticsOVA; ");
            sb.Append("SELECT COUNT(1) FROM Workers");
            string sqlQuery = sb.ToString();

            workersCount = obj.ReadCountRowInTable(sqlQuery);
            Console.WriteLine("\n-------------------------------------------------------------------");
            Console.WriteLine(@"Кол-во строк в tаблице ""Workers"" - " + workersCount);
            Console.WriteLine("\n-------------------------------------------------------------------");
        }
    }
}
