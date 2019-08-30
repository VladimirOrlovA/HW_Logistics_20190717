using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_Logistics_20190717
{
    interface IWorkWithSQL
    {
        string CreateTableQuery();
        string InsertTableQuery();
        string ViewTableQuery();
    }
}
