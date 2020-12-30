using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Runtime.Serialization.Json;
using System.Xml;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;

//Сериализация позволяет разработчику сохранять состояние объекта и воссоздавать его при необходимости.

//Это полезно для длительного хранения объектов или для обмена данными.

namespace ЛАБА14

{

    class Program

    {

        static void Main(string[] args)

        {

            Point point = new Point(5, 9);
            Point point_2 = new Point(7, 23);
            Point[] points = { point, point_2 };
            Console.WriteLine("_________Бинарная сериализация_________");

            BinaryFormatter formatter = new BinaryFormatter();//Сериализует и десериализует объект или полный граф связанных объектов
                                                                                     //в двоичном формате
            using (FileStream fs = new FileStream("point.dat", FileMode.OpenOrCreate))//FileStream - синхронные и асинхронные операции 
                //                                                            чтения и записи. FileMode.OpenOrCreate - Указывает, что
                //                                                             операционная система должна открыть файл, если он существует,
                //                                                                в противном случае должен быть создан новый файл
            {

                formatter.Serialize(fs, point);//Сериализация — это процесс преобразования объекта в поток байтов для сохранения или 
                                                           //передачи в память, базу данных или файл.
                Console.WriteLine("Объект сериализован");
                formatter.Serialize(fs, points);//Сериализация — это процесс преобразования объекта в поток байтов для сохранения или 
                                                   //передачи в память, базу данных или файл.
                Console.WriteLine("Объект сериализован");

            }

            using (FileStream fs = new FileStream("point.dat", FileMode.OpenOrCreate))//FileStream - синхронные и асинхронные операции 
                                                                  //чтения и записи. FileMode.OpenOrCreate - Указывает, что операционная 
                                                                  //система должна открыть файл, если он существует, в противном случае 
                                                                   //должен быть создан новый файл
            {

                Point newPoint = (Point)formatter.Deserialize(fs);//получить из потока байтов ранее сохраненный объект
                Console.WriteLine("Объект десериализован");
                Console.WriteLine($"X: {newPoint.X}, Y: {newPoint.Y}");
                Point[] newPoint2 = (Point[])formatter.Deserialize(fs);//получить из потока байтов ранее сохраненный объект
                Console.WriteLine("Объект десериализован");

                foreach (Point p in newPoint2)
                {
                    Console.WriteLine($"X: {p.X}, Y: {p.Y}");
                }

            }

            Console.WriteLine();
            Console.WriteLine("_________Сериализация в XML_________");

            XmlSerializer serializer = new XmlSerializer(typeof(Point));//Сериализует и десериализует объекты в XML-документы и из них. 
                                                                //XmlSerializer позволяет контролировать способ кодирования объектов в XML
            XmlSerializer serializer1 = new XmlSerializer(typeof(Point[]));//Сериализует и десериализует объекты в XML-документы и из них. 
                                                                //XmlSerializer позволяет контролировать способ кодирования объектов в XML
            using (FileStream fs = new FileStream("point2.xml", FileMode.OpenOrCreate))////FileStream - синхронные и асинхронные операции 
                                                                //чтения и записи. FileMode.OpenOrCreate - Указывает, что операционная 
                                                                //система должна открыть файл, если он существует, в противном случае 
                                                                //должен быть создан новый файл
            {
                serializer.Serialize(fs, point);
                Console.WriteLine("Объект сериализован");
            }

            using (FileStream fs = new FileStream("point2.xml", FileMode.OpenOrCreate))////FileStream - синхронные и асинхронные операции 
                                                                //чтения и записи. FileMode.OpenOrCreate - Указывает, что операционная 
                                                                //система должна открыть файл, если он существует, в противном случае 
                                                                //должен быть создан новый файл
            {
                Point newPoint = (Point)serializer.Deserialize(fs);
                Console.WriteLine("Объект десериализован");
                Console.WriteLine($"X: {newPoint.X}, Y: {newPoint.Y}");
            }

            using (FileStream fs = new FileStream("point3.xml", FileMode.OpenOrCreate))////FileStream - синхронные и асинхронные операции 
                                                                //чтения и записи. FileMode.OpenOrCreate - Указывает, что операционная 
                                                                //система должна открыть файл, если он существует, в противном случае 
                                                                //должен быть создан новый файл
            {
                serializer1.Serialize(fs, points);
                Console.WriteLine("Объект сериализован");
            }

            using (FileStream fs = new FileStream("point3.xml", FileMode.OpenOrCreate))////FileStream - синхронные и асинхронные операции 
                                                                //чтения и записи. FileMode.OpenOrCreate - Указывает, что операционная 
                                                                //система должна открыть файл, если он существует, в противном случае 
                                                                //должен быть создан новый файл
            {
                Point[] newPoint2 = (Point[])serializer1.Deserialize(fs);
                Console.WriteLine("Объект десериализован");
                foreach (Point p in newPoint2)
                {
                    Console.WriteLine($"X: {p.X}, Y: {p.Y}");
                }
            }

            Console.WriteLine();
            Console.WriteLine("_________Сериализация в JSON_________");

            Person person1 = new Person("Tom", 29);
            Person person2 = new Person("Will", 25);
            Person[] people = { person1, person2 };
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Person[]));//Сериализует объекты в нотацию 
                                                                                //объектов JavaScript (JSON) и десериализует данные JSON 
                                                                                //в объекты. Этот класс не наследуется

            using (FileStream fs = new FileStream("people.json", FileMode.OpenOrCreate))//FileStream - синхронные и асинхронные операции 
                                                                                //чтения и записи. FileMode.OpenOrCreate - Указывает, что 
                                                                                //операционная система должна открыть файл, если он 
                                                                             //существует, в противном случае должен быть создан новый файл
            {
                jsonFormatter.WriteObject(fs, people);//Записывает все данные объекта (начальный XML-элемент, содержимое и закрывающий 
                                                                                             //элемент) в XML-документ или поток
            }

            using (FileStream fs = new FileStream("people.json", FileMode.OpenOrCreate))//FileStream - синхронные и асинхронные операции 
                //чтения и записи. FileMode.OpenOrCreate - Указывает, что операционная система должна открыть файл, если он 
                //существует, в противном случае должен быть создан новый файл
            {
                Person[] newpeople = (Person[])jsonFormatter.ReadObject(fs);//Считывает XML-поток и возвращает десериализованный объект

                foreach (Person p in newpeople)
                {
                    Console.WriteLine("Имя: {0} --- Возраст: {1}", p.Name, p.Age);
                }
            }

            Console.WriteLine();
            Console.WriteLine("_________Linq to XML_________");

            //создание документа

            XDocument xdoc = new XDocument(new XElement("users",//создается документ, а затем в него добавляется комментарий и элемент
            new XElement("user",
            new XAttribute("name", "Bill Gates"),
            new XElement("company", "Microsoft"),
            new XElement("age", "48")),
            new XElement("user",
            new XAttribute("name", "Larry Page"),
            new XElement("company", "Google"),
            new XElement("price", "42"))));
            xdoc.Save("users.xml");

            IEnumerable<XElement> elements = xdoc.Descendants("user"); //Предоставляет перечислитель, который поддерживает простой перебор
                                                           //элементов неуниверсальной коллекции(kollectiya)
                                                           //получаем отдельный эл-т

            foreach (XElement e in elements)
                Console.WriteLine("Элемент {0} : значение = {1}", e.Name, e.Value);

            IEnumerable<XElement> elements2 = xdoc.Descendants("user").Where(e => ((string)e.Element("company")) == "Microsoft");

            foreach (XElement e in elements2)
                Console.WriteLine("Элемент {0} : значение = {1}", e.Name, e.Value);
            Console.WriteLine();

            //Используя XPath напишите два селектора для вашего XML документа

            Console.WriteLine("_________XPath_________");

            XmlDocument xml = new XmlDocument();
            xml.Load("users.xml");
            XmlElement xRoot = xml.DocumentElement;//только компании, все узлы корневого элемента//Возвращает корень XmlElement для документа
            XmlNodeList list = xRoot.SelectNodes("//user/company");//Выбирает список узлов в соответствии с выражением XPath

            foreach (XmlNode nodes in list)
            {
                Console.WriteLine(nodes.InnerText);//Возвращает или задает связанные значения узла и всех его дочерних узлов
            }

            XmlNode childnode = xRoot.SelectSingleNode("user[company='Microsoft']");//узел, у которого вложенный элемент "company" имеет 
                                                                                    //значение "Microsoft"

            if (childnode != null)
                Console.WriteLine(childnode.OuterXml);

        }
    }
}