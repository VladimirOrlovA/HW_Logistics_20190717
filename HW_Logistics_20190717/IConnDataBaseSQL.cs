using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_Logistics_20190717
{
    interface IConnDataBaseSQL
    {
        void CreateTable(string sqlQuery);
        void InsertTable(string sqlQuery);
        void ViewTable(string sqlQuery);
        void LoadData(string sqlQuery);
    }
}
