﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_Logistics_20190717
{
    class Worker : Person, IWorkWithSQL, IAddWorkerToWorkers
    {
        public static int workerCount=0;
        public int workerID{ get; set; }
        public DateTime employmentDate { get; set; }
        public string position { get; set; }
        public int solary { get; set; }


        public Worker(string lastName, string firstName,  string middleName, DateTime birthday, 
                        long inn, DateTime employmentDate, string position, int solary) 
            : base(lastName, firstName,  middleName, birthday, inn)
        {
            workerID = ++workerCount;
            this.employmentDate = employmentDate;
            this.position = position;
            this.solary = solary;
        }

        public Worker() { }

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

        // Формирует строку запроса в БД для создания таблицы
        public override string CreateTableQuery()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("USE LogisticsOVA; ");
            sb.Append("CREATE TABLE Worker (");
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
        }

        // Формирует строку запроса в БД для вставки данных в таблицу
        public override string InsertTableQuery()
        {
            string sqlQuery;
            // проверка на случай передачи в строку данных объекта с пустыми полями,
            // чтобы исключить ошибки в программе - проблемы с циклами и таблицы - внесение неверных данных
            if (workerID != 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("USE LogisticsOVA; ");
                sb.Append("INSERT INTO Workers (workerID, lastName, firstName, middleName, birthday, inn, employmentDate, position, solary) VALUES ");
                sb.Append($"('{workerID}', '{lastName}', '{firstName}', '{middleName}', '{birthday.Year}-{birthday.Month}-{birthday.Day}', '{inn}'," +
                    $" '{employmentDate.Year}-{employmentDate.Month}-{employmentDate.Day}', '{position}', '{solary}') ");
                return sqlQuery = sb.ToString();
            }
            return sqlQuery=null;
        }

        // Формирует строку запроса в БД для чтения данных из таблицы
        public override string ViewTableQuery()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("USE LogisticsOVA; ");
            sb.Append("SELECT * FROM Worker p ");
            string sqlQuery = sb.ToString();
            return sqlQuery;
        }

        // Отправляет через интерфейс объект класса Worker
        Worker IAddWorkerToWorkers.ThisWorker()
        {
            return this;
            //throw new NotImplementedException();
        }

        // устнавливает счетчик на цифру кол-ва ранее созданных объектов 
        public void SetCountObj(ICountObj obj)
        {
            workerCount = obj.CountObj();
        }
    }
}
