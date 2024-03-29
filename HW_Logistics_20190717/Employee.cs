﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_Logistics_20190717
{
    class Employee : Person, IComparable
    {
        private static int employeesCount = 0;
        public int employeeID { get; set; }
        public DateTime employmentDate { get; set; }
        public string position { get; set; }
        public int solary { get; set; }

        public Employee() { }

        public Employee(string lastName, string firstName, string middleName, DateTime birthday,
                        long iin, DateTime employmentDate, string position, int solary)
            : base(lastName, firstName, middleName, birthday, iin)
        {
            employeeID = ++employeesCount;
            this.employmentDate = employmentDate;
            this.position = position;
            this.solary = solary;
        }

        // Перегрузка базовых методов
        public override bool Equals(object obj)
        {
            Employee empl = (Employee)obj; // as Employee;

            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }

            return 
                (lastName == empl.lastName) && (firstName == empl.firstName) && (middleName == empl.middleName)
                && (birthday == empl.birthday) && (employmentDate == empl.employmentDate)
                && (position == empl.position && solary == empl.solary);

        }

        public override int GetHashCode()
        {
            // создаем временную строку для вычисления hash кода для отличных типов данных
            string tmp = null;

            // Для операндов целочисленного типа Оператор ^ вычисляет побитовое логическое 
            //исключающее ИЛИ, также известное как побитовое логическое XOR, своих операндов

            int hash = 0;
            hash ^= lastName.GetHashCode();
            hash ^= firstName.GetHashCode();
            hash ^= middleName.GetHashCode();
            hash ^= iin.GetHashCode();
            hash ^= employmentDate.GetHashCode();
            hash ^= position.GetHashCode();
            hash ^= employmentDate.GetHashCode();
            hash ^= solary.GetHashCode();

            // возвращаем сгенерированный hash code в результате побитовго XOR 
            return hash;
        }

        public override string ToString()
        {
            return $"Объект хэш \'{GetHashCode()}\' класса Employee со значениями: " +
                $"{lastName}, {firstName}, {middleName}, {middleName}, {birthday}, " +
                $"{employmentDate}, {position}, {solary}, {Age()}";
            //return base.ToString();
        }

        // Сортировка по умолчанию
        public int CompareTo(object obj)
        {
            return String.Compare(this.lastName, (obj as Employee).lastName);
        }

        // Сортировка по имени, через реализацию предопределенного интерфейса.
        public class SortByName : IComparer<Employee>
        {
            int IComparer<Employee>.Compare(Employee x, Employee y)
            {
                return String.Compare(x.firstName, y.firstName);
            }
        }
        // Сортировка по возрасту, через реализацию предопределенного интерфейса.
        public class SortByAge : IComparer<Employee>
        {
            int IComparer<Employee>.Compare(Employee x, Employee y)
            {
                return Convert.ToInt32( x.Age() - y.Age());
            }
        }

        public override void Info()
        {
            Console.WriteLine("\n----------------- Информация о сотруднике -----------------\n\n");
            Console.WriteLine("Номер работника --------- " + employeeID);
            InfoPerson();
            Console.WriteLine("Дата приема на работу --- " + employmentDate);
            Console.WriteLine("Должность --------------- " + position);
            Console.WriteLine("Оклад ------------------- " + solary);
            Console.WriteLine("\n-----------------------------------------------------------\n\n");
        }

        // Вносит данные в таблицу БД
        public void InsertTable(IConnDataBaseSQL obj)
        {
            Console.WriteLine(@"Insert Data to table ""Employees"" about "
               + Convert.ToString(this.GetType()).Substring(22));

            StringBuilder sb = new StringBuilder();
            sb.Append("USE LogisticsOVA; ");
            sb.Append("INSERT INTO Employees (lastName, firstName, middleName, birthday, iin, employmentDate, position, solary) VALUES ");
            sb.Append($"('{lastName}', '{firstName}', '{middleName}', '{birthday.Year}-{birthday.Month}-{birthday.Day}', '{iin}'," +
                $" '{employmentDate.Year}-{employmentDate.Month}-{employmentDate.Day}', '{position}', '{solary}') ");
            string sqlQuery = sb.ToString();

            obj.SaveData(sqlQuery);
        }

        // Устнавливает счетчик на цифру кол-ва ранее созданных объектов
        public void SetCountObj(IConnDataBaseSQL obj)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("USE LogisticsOVA; ");
            sb.Append("SELECT COUNT(1) FROM Employees");
            string sqlQuery = sb.ToString();

            employeesCount = obj.ReadCountRowInTable(sqlQuery);
            Console.WriteLine("\n-------------------------------------------------------------------");
            Console.WriteLine(@"Кол-во строк в tаблице ""Employees"" - " + employeesCount);
            Console.WriteLine("\n-------------------------------------------------------------------");
        }
    }
}
