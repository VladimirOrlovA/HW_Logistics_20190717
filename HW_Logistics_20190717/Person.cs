using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_Logistics_20190717
{
    class Person
    {
        public string lastName { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public DateTime birthday { get; }
        public long inn { get; }
       
        public Person(string lastName, string firstName, string middleName, DateTime birthday, long inn)
        {
            this.lastName = lastName;
            this.firstName = firstName;
            this.middleName = middleName;
            this.birthday = birthday;
            this.inn = inn;
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

         public void InfoPerson() 
        {
            //Console.WriteLine("\n----------------- Информация о человеке -----------------\n\n");
            Console.WriteLine("ФИО полностью ----------- " + GetLFM());
            Console.WriteLine("Дата рождения ----------- " + birthday);
            Console.WriteLine("Возраст ----------------- " + Age());
            Console.WriteLine("ИНН --------------------- " + inn);
            //Console.WriteLine("\n---------------------------------------------------------\n\n");
        }
        
        // Возвращает строку с полным ФИО
        public string GetLFM()
        {
            return (lastName + " " + firstName + " " + middleName);
        }

        // Возвращает строку с фамилией и инициалами
        public string GetLastNameAndFM()
        {
            return (lastName + " " + firstName.Substring(0,1) + "." + middleName.Substring(0,1) + ".");
        }

        //Возвращает текущий возраст
        public int Age()
        {
            return DateTime.Today.Year - birthday.Year;
        }
    }

}
