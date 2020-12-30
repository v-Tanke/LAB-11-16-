using System;
using System.Collections.Generic;//подключение пространства имен обобщенных или типизированных классов коллекций
using System.Linq;//подключаем пространство имен System.Linq
using System.Text;
using System.Threading.Tasks;
using System.Reflection;//Пространство имен содержит типы, предназначенные для извлечения сведений о сборках, модулях, 
                          // членах, параметрах и других объектах в управляемом коде путем обработки их метаданных

using System.IO;//Пространство имен содержит типы, позволяющие осуществлять чтение и запись в файлы и потоки данных, 
                  //а также типы для базовой поддержки файлов и папок.

namespace lab12
{
    static class Reflector//Создаем класс рефлектор
    {
        static public void AllClassContent(object obj)
        {
            string writePath = @"D:\2 семестр\ООП\lab12\write.txt";//Прописываем путь к папке в string

            // Класс StreamWriter реализует TextWriter для записи символов в поток в определенной кодировке
            StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default);


            Type m = obj.GetType();//Метод возвращает объект Type для текущего экземпляра.
            MemberInfo[] members = m.GetMembers();// Метод получает члены (свойства, методы, поля, события и т. д.) текущего объекта Type
            foreach (MemberInfo item in members)//Вывод элементов MemberInfo
            {
                sw.WriteLine($"{item.DeclaringType} {item.MemberType} {item.Name}");
            }
            sw.Close();

        }



        static public void PublicMethods(object obj)
        {
            Type m = obj.GetType();//Метод возвращает объект Type для текущего экземпляра.
            // Класс MethodInfo выявляет атрибуты метода и обеспечивает доступ к его метаданным.
            MethodInfo[] pubMethods = m.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
            Console.WriteLine("Только публичные методы:");
            foreach (MethodInfo item in pubMethods)
            {
                Console.WriteLine(item.ReturnType.Name + " " + item.Name);
            }

        }



        static public void FieldsAndProperties(object obj)
        {
            Type m = obj.GetType();//Метод возвращает объект Type для текущего экземпляра.
            Console.WriteLine("Поля:");
            //FieldInfo класс обнаруживает атрибуты поля и обеспечивает доступ к его метаданным
            FieldInfo[] fields = m.GetFields();//Метод GetFields получает поля текущего объекта Type

            foreach (FieldInfo item in fields)
            {
                Console.WriteLine(item.FieldType + " " + item.Name);

            }
            Console.WriteLine("Свойства:");
            //Класс PropertyInfo выявляет атрибуты свойства и обеспечивает доступ к его метаданным
            PropertyInfo[] properties = m.GetProperties();//Метод GetProperties получает свойства текущего объекта Type.
            foreach (PropertyInfo item in properties)
            {
                Console.WriteLine($"{item.PropertyType} {item.Name}");
            }

        }


        static public void Interfaces(object obj)
        {
            Type m = obj.GetType();//Метод возвращает объект Type для текущего экземпляра.
            Console.WriteLine("Реализованные интерфейсы:");
            foreach (Type item in m.GetInterfaces())//Метод GetInterfaces возвращает определенный интерфейс, реализуемый или наследуемый текущим объектом Type
            {
                Console.WriteLine(item.Name);
            }

        }

        static public void MethodsWithParams(object obj)
        {
            Console.WriteLine("Введите название типа для параметров:");
            string findType = Console.ReadLine();


            Type m = obj.GetType();
            // Класс MethodInfo выявляет атрибуты метода и обеспечивает доступ к его метаданным.
            MethodInfo[] methods = m.GetMethods();//Метод GetMethods получает заданный метод текущего класса Type
            foreach (MethodInfo item in methods)
            {
                //Класс ParameterInfo обнаруживает атрибуты параметра и обеспечивает доступ к его метаданным
                ParameterInfo[] p = item.GetParameters();//Метод GetParameters при переопределении в производном классе возвращает параметры заданного метода или конструктора

                foreach (ParameterInfo param in p)
                {
                    if (param.ParameterType.Name == findType)
                    {
                        Console.WriteLine("Метод:" + item.ReturnType.Name + " " + item.Name);
                    }
                }


            }

        }
        public static void LastTask(string Class, string MethodName)
        {
            //Класс StreamReader реализует объект TextReader, который считывает символы из потока байтов в определенной кодировке
            StreamReader reader = new StreamReader(@"D:\2 семестр\ООП\lab12\read.txt", Encoding.Default);
            string param1, param2, param3;
            //Считывание
            param1 = reader.ReadLine();
            param2 = reader.ReadLine();
            param3 = reader.ReadLine();
            reader.Close();

            Type m = Type.GetType(Class, true);

            //Класс Activator содержит методы, позволяющие локально или удаленно создавать типы объектов или получать
            //ссылки на существующие удаленные объекты
            object st = Activator.CreateInstance(m, null);//Метод CreateInstance создает экземпляр указанного типа, используя конструктор, 
                                                          //который наиболее полно соответствует указанным параметрам

            // Класс MethodInfo выявляет атрибуты метода и обеспечивает доступ к его метаданным.
            MethodInfo method = m.GetMethod(MethodName);//Метод GetMethods получает заданный метод текущего класса Type
            method.Invoke(st, new object[] { param1, char.Parse(param2), int.Parse(param3) });
        }     //Метод Invoke вызывает метод или конструктор, отражаемый этим экземпляром MethodInfo
    }
    public class TestParams
    {
        public static void showParams(string str, char symbol, int number)
        {
            Console.WriteLine($"{str} {symbol} {number}");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Train train = new Train("train1", "БелЖД", 600000);
            Car car = new Car("car1", "Вася", 300, 220, 23);
            Reflector.AllClassContent(train);
            Console.WriteLine("----------------Поля и свойства объекта train----------------");
            Reflector.FieldsAndProperties(train);
            Console.WriteLine("----------------Интерфейсы, реализованные объектом car----------------");
            Reflector.Interfaces(car);
            Console.WriteLine("----------------Для объекта car----------------");
            Reflector.PublicMethods(car);
            Reflector.MethodsWithParams(train);
            Reflector.LastTask("lab12.TestParams", "showParams");
        }
    }
}
