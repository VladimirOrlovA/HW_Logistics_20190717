﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_Logistics_20190717
{
    class Workers
    {
        private List<Worker> workersList = new List<Worker>();

        // Добавление работника в список работников !!! сделать интерфейс для добавки работников
        public void AddWorker(Worker obj)
        {
            workersList.Add(obj);
        }


        public void Info()
        {
            foreach (var worker in workersList)
                worker.Info();
        }



    }
}