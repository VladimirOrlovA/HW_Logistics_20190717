using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_Logistics_20190717
{
    interface IConnDataBaseSQL
    {
        void SaveData(string sqlQuery);
        void ReadData(string sqlQuery);
        void LoadData(string sqlQuery);
        int ReadCountRowInTable(string sqlQuery);
    }
}
