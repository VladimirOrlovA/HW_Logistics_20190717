using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;

namespace HW_Logistics_20190717
{
    class Carriers: IWriteToXML, IReadFromXML
    {
        public List<Carrier> carriersList = new List<Carrier>();

        // Событие, возникающее при добавлении перевозчика
        public event ObjectsStateHandler CarrierAdded;
        // Событие, возникающее при внесении перевозчиков в БД
        public event ObjectsStateHandler CarriersAddedtoDB;

        // Добавляет работника в список работников
        public void AddCarrier(Carrier obj)
        {
            carriersList.Add(obj);
            if (CarrierAdded != null)
                CarrierAdded($"Добавлен новый перевозчик - {obj.GetLastNameAndFM()}");
        }

        // Выводит информацию в консоль по содержимому каждого объекта листа
        public void Info()
        {
            for (int i = 0; i != carriersList.Count; i++)
                carriersList[i].Info();

            //foreach (var carrier in carriersList)
            //    carrier.Info();
        }

        // Создает таблицу в БД
        public void CreateTable(IConnDataBaseSQL obj)
        {
            Console.WriteLine(@"Creating Table --- ""Carriers""");

            StringBuilder sb = new StringBuilder();
            sb.Append("USE LogisticsOVA; ");
            sb.Append("CREATE TABLE Carriers (");
            sb.Append(" carrierID INT IDENTITY(1,1) NOT NULL PRIMARY KEY, ");
            sb.Append(" lastName NVARCHAR(50), ");
            sb.Append(" firstName NVARCHAR(50), ");
            sb.Append(" middleName NVARCHAR(50), ");
            sb.Append(" birthday DATE, ");
            sb.Append(" iin DECIMAL, ");
            sb.Append(" carrierRouteID INT DEFAULT NULL, ");
            sb.Append(" carrierTransportID INT DEFAULT NULL ");
            sb.Append("); ");
            string sqlQuery = sb.ToString();

            obj.SaveData(sqlQuery);

            //throw new NotImplementedException();
        }

        // Вносит данные в таблицу БД
        public void InsertTable(IConnDataBaseSQL obj)
        {
            Console.WriteLine(@"Insert Data to table ""Carriers"" about "
                    + Convert.ToString(this.GetType()).Substring(22));

            StringBuilder sb = new StringBuilder();
            sb.Append("USE LogisticsOVA; ");
            sb.Append("INSERT INTO Carriers (lastName, firstName, middleName, birthday, iin, " +
                "carrierID, carrierRouteFromIDs, carrierTransportID ) VALUES ");

            // объявляем переменную счетчика для подсчета кол-ва итерации, чтобы в запросе на последний
            // ввод строки в таблицу не ставить "," (обеспечение правильности синтаксиса запроса SQL)
            int count = 0;
            string sqlQuery = null;
            foreach (Carrier i in carriersList)
            {
                count++;
                sb.Append($"('{i.LastName}', '{i.FirstName}', '{i.MiddleName}', '{i.birthday.Year}-{i.birthday.Month}-{i.birthday.Day}', '{i.iin}'," +
                    $" '{i.carrierID}', '{i.carrierRoutesIdList}', '{i.carrierTransportsIdList}') ");
                if (carriersList.Count != count) sb.Append(", ");
            }

            sqlQuery = sb.ToString();
            obj.SaveData(sqlQuery);

            if (CarriersAddedtoDB != null)
                CarriersAddedtoDB("Данные о перевозчиках внесены в таблицу carriers базы данных.");
            //throw new NotImplementedException();
        }

        // Выполняет вывод таблицы в консоль
        public void ViewTable(IConnDataBaseSQL obj)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("USE LogisticsOVA; ");
            sb.Append("SELECT * FROM Carriers ");
            string sqlQuery = sb.ToString();
            obj.ReadData(sqlQuery);
            //throw new NotImplementedException();
        }

        // Запись коллекции в файл XML
        public void DOMWriteXML(string filename)
        {
            // Создаем объектную модель
            XmlDocument doc = new XmlDocument();

            //Создаем заголовок XML 
            XmlDeclaration xmldecl = doc.CreateXmlDeclaration("1.0", null, null);

            //lastName, firstName, middleName, birthday, iin
            //string[] carrierRoutesIdList = new string[0];
            //[] carrierTransportsIdList = new [0];


            // Создаем основную ноду
            XmlElement node = doc.CreateElement("carriers");
            foreach (Carrier carrier in carriersList)
            {
                // Создаем ноду для заказчика
                XmlElement xmlcarrier = doc.CreateElement("carrier");

                // Создаем артрибут фамилии 
                XmlAttribute xmlLastName = doc.CreateAttribute("lastName");
                xmlLastName.InnerText = carrier.LastName;
                xmlcarrier.Attributes.Append(xmlLastName);

                // Создаем артрибут имени 
                XmlAttribute xmlFirstName = doc.CreateAttribute("firstName");
                xmlFirstName.InnerText = carrier.FirstName;
                xmlcarrier.Attributes.Append(xmlFirstName);

                // Создаем артрибут отчества 
                XmlAttribute xmlMiddleName = doc.CreateAttribute("middleName");
                xmlMiddleName.InnerText = carrier.MiddleName;
                xmlcarrier.Attributes.Append(xmlMiddleName);

                // Создаем артрибут даты рождения
                XmlAttribute xmlBirthday = doc.CreateAttribute("birthday");
                xmlBirthday.InnerText = Convert.ToString($"{carrier.birthday.Year}-{carrier.birthday.Month}-{carrier.birthday.Day}");
                xmlcarrier.Attributes.Append(xmlBirthday);

                // Создаем артрибут ИИН
                XmlAttribute xmlIin = doc.CreateAttribute("iin");
                xmlIin.InnerText = Convert.ToString(carrier.iin);
                xmlcarrier.Attributes.Append(xmlIin);

                // Создаем ноду маршрутный лист (массив из идентификаторов маршрутов)
                XmlElement xmlcarrierRoutesIdList = doc.CreateElement("carrierRoutesIdList");
                //foreach (string routeID in carrier.carrierRoutesIdList)
                //{
                //    XmlAttribute xmlrouteID = doc.CreateAttribute("routeID");
                //    xmlrouteID.InnerText = (routeID);
                //    xmlcarrierRoutesIdList.Attributes.Append(xmlrouteID);
                //}
                node.AppendChild(xmlcarrierRoutesIdList);

                // Добавляем артрибут работника в ноды списка
                node.AppendChild(xmlcarrier);
            }
            // Добавляем созданную структуру в корень
            doc.AppendChild(node);
            doc.InsertBefore(xmldecl, node);
            doc.Save(filename);

            
            Console.WriteLine("Display the modified XML...");
            doc.Save(Console.Out);
        }

        // Запись коллекции в файл XML
        public void DOMReadXML(string filename)
        {
            // Создаем объектную модель
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);

            // Выбираем необходимый список нод
            XmlNodeList list = doc.GetElementsByTagName("carrier");
            foreach (XmlElement elem in list)
            {
                //string lastName, string firstName, string middleName, DateTime birthday,
                //        long iin, DateTime employmentDate, string position, int solary
                XmlNode attrib1 = elem.Attributes.GetNamedItem("lastName");
                XmlNode attrib2 = elem.Attributes.GetNamedItem("firstName");
                XmlNode attrib3 = elem.Attributes.GetNamedItem("middleName");
                XmlNode attrib4 = elem.Attributes.GetNamedItem("birthday");
                XmlNode attrib5 = elem.Attributes.GetNamedItem("iin");

                this.carriersList.Add(new Carrier(
                    attrib1.Value,
                    attrib2.Value,
                    attrib3.Value,
                    Convert.ToDateTime(attrib4.Value),
                    Convert.ToInt64(attrib5.Value)
                    ));

                //XmlNodeList solarylist = elem.GetElementsByTagName("solary");
                //XmlNodeList carrierRoutesIdList = doc.GetElementsByTagName("carrierRoutesIdList");
                //foreach (XmlElement elem in list)

            }
        }
    }
}
