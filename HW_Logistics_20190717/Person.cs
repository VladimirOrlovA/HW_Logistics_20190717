using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_Logistics_20190717
{
    class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime Birthday { get; }
        public long Inn { get; }
       
        public Person(DateTime birthday, long inn)
        {
            this.Birthday = birthday;
            this.Inn = inn;
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
            Console.WriteLine("ФИО полностью ----- " + GetLFM());
            Console.WriteLine("Дата рождения ----- " + Birthday);
            Console.WriteLine("Возраст ----------- " + Age());
            Console.WriteLine("ИНН --------------- " + Inn);
            Console.WriteLine("\n---------------------------------------------------------\n\n");
        }
        
        // Возвращает строку с полным ФИО
        public string GetLFM()
        {
            return (LastName + " " + FirstName + " " + MiddleName);
        }

        // Возвращает строку с фамилией и инициалами
        public string GetLastNameAndFM()
        {
            return (LastName + " " + FirstName.Substring(0,1) + "." + MiddleName.Substring(0,1) + ".");
        }

        //Возвращает текущий возраст
        public int Age()
        {
            return DateTime.Today.Year - Birthday.Year;
        }

    }

}
