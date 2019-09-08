using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_Logistics_20190717
{
    internal abstract class Person
    {
        protected string lastName;
        // сеттер геттер для lastName
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                if (InputCheckUppercaseLetter(value) && InputCheckOnlyCyrillicLetters(value))
                    lastName = value;
                else lastName = " ";
            }
        }

        protected string firstName;
        // сеттер геттер для firstName
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                if (InputCheckUppercaseLetter(value) && InputCheckOnlyCyrillicLetters(value))
                    firstName = value;
                else firstName = " ";
            }
        }

        protected string middleName;
        // сеттер геттер для MiddleName
        public string MiddleName
        {
            get
            {
                return middleName;
            }
            set
            {
                if (InputCheckUppercaseLetter(value) && InputCheckOnlyCyrillicLetters(value))
                    middleName = value;
                else middleName = " ";
            }
        }

        public DateTime birthday { get; set; }
        public long inn { get; set; }

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
        public bool InputCheckUppercaseLetter(string value)
        {
            if (Convert.ToInt16(value[0]) <= 65 || Convert.ToInt16(value[0]) >= 91 
                    && Convert.ToInt16(value[0]) < 1040 || Convert.ToInt16(value[0]) > 1071)
            {
                Console.WriteLine("Неверный ввод.");
                Console.WriteLine("Первая буква должна быть прописной - заглавной");
                return false;
            }
            return true;
        }

        // Проверка ввода - условие только латинские буквы
        public bool InputCheckOnlyLatinLetters(string value)
        {
            for (int i = 0; i < value.Length; i++)
                if (Convert.ToInt16(value[i]) < 65 || Convert.ToInt16(value[i]) > 122)
                {
                    //throw new ArgumentException("Неверный ввод. Имя должно содержать только латинские буквы");
                    Console.WriteLine("Неверный ввод.");
                    Console.WriteLine("ФИО должно содержать только латинские буквы, без пробелов и прочих символов");
                    return false;
                }
            return true;
        }

        // Проверка ввода - условие только буквы кириллицы
        public bool InputCheckOnlyCyrillicLetters(string value)
        {
            for (int i = 0; i < value.Length; i++)
                if (Convert.ToInt16(value[i]) < 1040 || Convert.ToInt16(value[i]) > 1103)
                {
                    //throw new ArgumentException("Неверный ввод. Имя должно содержать только буквы кририлицы");
                    Console.WriteLine("Неверный ввод.");
                    Console.WriteLine("ФИО должно содержать только буквы кририлицы без пробелов и прочих символов");
                    return false;
                }
            return true;
        }

        // Проверка ввода - условие только буквы кириллицы или латиницы
        public bool InputCheckOnlyCyrOrLatLetters(string value)
        {
            bool flag = true;
            if (Convert.ToInt16(value[0]) < 1040 || Convert.ToInt16(value[0]) > 1103)
                flag = InputCheckOnlyCyrillicLetters(value);

            if (Convert.ToInt16(value[0]) < 65 || Convert.ToInt16(value[0]) > 122)
                flag = InputCheckOnlyLatinLetters(value);
            return flag;
        }

        // вывод в консоль информации по определенным полям объекта
        public void InfoPerson()
        {
            //Console.WriteLine("\n----------------- Информация о человеке -----------------\n\n");
            Console.WriteLine("ФИО полностью ----------- " + GetLFM());
            Console.WriteLine("Дата рождения ----------- " + birthday);
            Console.WriteLine("Возраст ----------------- " + Age());
            Console.WriteLine("ИНН --------------------- " + inn);
            //Console.WriteLine("\n---------------------------------------------------------\n\n");
        }

        // для обязательного переопределения метода вывода информации в дочерних классах
        abstract public void Info();

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
