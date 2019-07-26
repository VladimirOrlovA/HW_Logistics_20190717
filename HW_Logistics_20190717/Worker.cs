using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_Logistics_20190717
{
    internal class Worker : Person
    {
        public DateTime employmentDate { get; set; }
        public string position { get; set; }
        public int solary { get; set; }


        public Worker(DateTime birthday, long inn, DateTime employmentDate, string position, int solary) : base(birthday, inn)
        {
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
            InfoPerson();
            Console.WriteLine("Дата приема на работу ----- " + employmentDate);
            Console.WriteLine("Должность ----------------- " + position);
            Console.WriteLine("Оклад --------------------- " + solary);
            Console.WriteLine("\n---------------------------------------------------------\n\n");
        }
    }
}
