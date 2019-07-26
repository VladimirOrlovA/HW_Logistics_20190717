using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_Logistics_20190717
{
    internal class Сustomer : Person
    {
        public int customerID;

        public Сustomer(string firstName, string lastName, string middleName, DateTime birthday, long inn, int customerID)
            : base(firstName, lastName, middleName, birthday, inn)
        {
            this.customerID = customerID;
        }
    }
}
