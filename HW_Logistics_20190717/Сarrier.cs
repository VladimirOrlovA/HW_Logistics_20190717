using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_Logistics_20190717
{
    class Сarrier
    {
        string carrierName;
        Route[] routeList = new Route[1];

        public Сarrier(string carrierName)
        {
            this.carrierName = carrierName;
        }
    }
}
