using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_Logistics_20190717
{
    abstract class Person : IWorkWithSQL
    {
        protected string lastName;
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                InputCheckUppercaseLetter(value);
                lastName = InputCheckOnlyCyrillicLetters(value);
            }
        }

        protected string firstName;
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                InputCheckUppercaseLetter(value);
                firstName = InputCheckOnlyCyrillicLetters(value);
            }
        }

        protected string middleName;
        public string MiddleName
        {
            get
            {
                return middleName;
            }
            set
            {
                InputCheckUppercaseLetter(value);
                middleName = InputCheckOnlyCyrillicLetters(value);
            }
        }

        public DateTime birthday { get; }
        public long inn { get; }

        public Person() { }
        public Person(string lastName, string firstName, string middleName, DateTime birthday, long inn)
        {
            LastName = lastName;
            FirstName = firstName;
            MiddleName = middleName;
            this.birthday = birthday;
            this.inn = inn;
        }

        // Проверка ввода - условие первый символ - прописная/заглавная буква
        public void InputCheckUppercaseLetter(string value)
        {
            if (value[0] <= 65 || value[0] >= 91 && value[0] <= 1040 || value[0] >= 1071)
            {
                Console.WriteLine("Неверный ввод.");
                Console.WriteLine("Первая буква должна быть прописной - заглавной");
            }
        }

        // Проверка ввода - условие только латинские буквы
        public string InputCheckOnlyLatinLetters(string value)
        {
            for (int i = 0; i < value.Length; i++)
                if (value[i] < 65 || value[i] > 122)
                {
                    //throw new ArgumentException("Неверный ввод. Имя должно содержать только латинские буквы");
                    Console.WriteLine("Неверный ввод.");
                    Console.WriteLine("ФИО должно содержать только латинские буквы, без пробелов и прочих символов");
                    return value = "abcd";
                }
            return value;
        }

        // Проверка ввода - условие только буквы кириллицы
        public string InputCheckOnlyCyrillicLetters(string value)
        {
            for (int i = 0; i < value.Length; i++)
                if (value[i] < 1040 || value[i] > 1103)
                {
                    //throw new ArgumentException("Неверный ввод. Имя должно содержать только буквы кририлицы");
                    Console.WriteLine("Неверный ввод.");
                    Console.WriteLine("ФИО должно содержать только буквы кририлицы без пробелов и прочих символов");
                    return value = "абвг";
                }
            return value;
        }

        // Проверка ввода - условие только буквы кириллицы или латиницы
        public string InputCheckOnlyCyrOrLatLetters(string value)
        {
            if (value[0] < 1040 || value[0] > 1103)
                InputCheckOnlyCyrillicLetters(value);

            if (value[0] < 65 || value[0] > 122)
                InputCheckOnlyLatinLetters(value);
            return value = "ФИО LFM";
        }

        public void InfoPerson()
        {
            //Console.WriteLine("\n----------------- Информация о человеке -----------------\n\n");
            Console.WriteLine("ФИО полностью ----------- " + GetLFM());
            Console.WriteLine("Дата рождения ----------- " + birthday);
            Console.WriteLine("Возраст ----------------- " + Age());
            Console.WriteLine("ИНН --------------------- " + inn);
            //Console.WriteLine("\n---------------------------------------------------------\n\n");
        }

        // Формирует строку запроса в БД для создания таблицы
        abstract public string CreateTableQuery();

        // Формирует строку запроса в БД для вставки данных
        abstract public string InsertTableQuery();

        // Формирует строку запроса в БД для вставки данных
        abstract public string ViewTableQuery();

        // Возвращает строку с полным ФИО
        public string GetLFM()
        {
            return (lastName + " " + firstName + " " + middleName);
        }

        // Возвращает строку с фамилией и инициалами
        public string GetLastNameAndFM()
        {
            return (lastName + " " + firstName.Substring(0, 1) + "." + middleName.Substring(0, 1) + ".");
        }

        //Возвращает текущий возраст
        public int Age()
        {
            return DateTime.Today.Year - birthday.Year;
        }
    }

}
