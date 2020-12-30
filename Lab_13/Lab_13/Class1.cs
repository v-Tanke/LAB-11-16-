using System;
using System.Collections.Generic;//подключение пространства имен обобщенных или типизированных классов коллекций
using System.Linq;//подключаем пространство имен System.Linq
using System.Text;
using System.Threading.Tasks;
using System.IO;//Пространство имен содержит типы, позволяющие осуществлять чтение и запись в файлы и потоки данных, 
                //а также типы для базовой поддержки файлов и папок.
using System.IO.Compression;

namespace lab_13
{
    public delegate void userAction(string str);
    class DMVLog        //класс DMVLog.Он должен отвечать за работу с текстовым файлом
                         //в который записываются все действия пользователя и
                          //соответственно методами записи в текстовый файл, чтения, поиска нужной
                           //информации
    {
        private const string path = "D:\\task6.txt";//Путь к файлу

        public static class DMVDiskInfo //Создаём класс DMVDiskInfo c методами для вывода информации
        {
            public static void ShowFreeSpace(string driveName)
            {
                DriveInfo drive = new DriveInfo(driveName);//Класс DriveInfo предоставляет доступ к сведениям на диске
                Console.WriteLine($"свободное место на диске {drive.Name} {drive.TotalFreeSpace}");//свободное место
            }
            public static void ShowFileSystem(string driveName)
            {
                DriveInfo drive = new DriveInfo(driveName);//Класс DriveInfo предоставляет доступ к сведениям на диске
                Console.WriteLine($"Диск {drive.Name} с файловой системой: {drive.DriveFormat}");//файловая система
            }
            public static void AllInfo()//Вся информация
            {
                //Класс DriveInfo предоставляет доступ к сведениям на диске
                DriveInfo[] drives = DriveInfo.GetDrives();//Метод GetDrives возвращает имена всех логических дисков на компьютере
                foreach (DriveInfo drive in drives)
                {
                    if(drive.IsReady)//проверка
                    {
                        //Вывод информации
                        Console.WriteLine($"Имя диска {drive.Name}");
                        Console.WriteLine($"Объем диска {drive.TotalSize}");
                        Console.WriteLine($"Свободное место на диске {drive.TotalFreeSpace}");
                        Console.WriteLine($"Метка тома {drive.VolumeLabel}");
                    }
                }
            }
        }
        public static class DMVFileInfo//Создаём класс DMVFileInfo c методами для вывода информации о конкретном файле

        {
            public static void ShowFilePath(string path)
            {
                FileInfo file = new FileInfo(path);//Класс FileInfo предоставляет свойства и методы экземпляра для создания, копирования, удаления, 
                                                   //перемещения и открытия файлов, а также позволяет создавать объекты FileStream
                if (file.Exists)
                {
                    Console.WriteLine($"Имя файла: {file.Name}");
                    Console.WriteLine($"Полный путь к файлу: {file.FullName}");
                }
                else
                    Console.WriteLine("Файла по такому пути не существует");
            }
            public static void ShowFileSizeExtName(string path)
            {
                FileInfo file = new FileInfo(path);
                if(file.Exists)//Определяет существует ли заданный файл
                {
                    //Вывод информации
                    Console.WriteLine($"Имя файла: {file.Name}");
                    Console.WriteLine($"Размер файла: {file.Length} байт");
                    Console.WriteLine($"Расширение файла: {file.Extension}");
                }
                else
                    Console.WriteLine("Файла по такому пути не существует");
            }
            public static void ShowCreationFile(string path)
            {
                FileInfo file = new FileInfo(path);//Класс FileInfo предоставляет свойства и методы экземпляра для создания, копирования, удаления, 
                                                   //перемещения и открытия файлов, а также позволяет создавать объекты FileStream
                if (file.Exists)//Определяет существует ли заданный файл
                {
                    Console.WriteLine($"Имя файла: {file.Name}");
                    Console.WriteLine($"Время создания: {file.CreationTime}");
                }
            }
        }
        public static class DMVDirInfo//Создаем класс DMVDirInfo c методами для вывода информации о конкретном директории
        {
            public static void ListOfDirectory(string path)
            {
                DirectoryInfo directory = new DirectoryInfo(path);//Класс DirectoryInfo предоставляет методы экземпляра класса для создания, перемещения 
                                                                  //и перечисления в каталогах и подкаталогах
                if (directory.Exists)//Определяет существует ли заданный файл
                {
                    Console.WriteLine($"Папка {directory.Name}");
                    Console.WriteLine("Подкоталоги:");
                    string[] dirs = Directory.GetDirectories(path);//Метод GetDirectories возвращает имена подкаталогов, соответствующих указанным критериям
                    foreach (string s in dirs)
                    {
                        Console.WriteLine(s);
                    }
                }
                else
                    Console.WriteLine("Такого каталога не существует");
            }
            public static void CreationTime(string path)
            {
                DirectoryInfo directory = new DirectoryInfo(path);//Класс DirectoryInfo предоставляет методы экземпляра класса для создания, перемещения 
                                                                  //и перечисления в каталогах и подкаталогах
                if (directory.Exists)//Определяет существует ли заданный файл
                {
                    Console.WriteLine($"Папка: {directory.Name}");
                    Console.WriteLine($"Дата создания: {directory.CreationTime}");
                }
                else
                    Console.WriteLine("Такого каталога не существует ");
            }
            public static void NumerOfFiles(string path)
            {
                DirectoryInfo directory = new DirectoryInfo(path);//Класс DirectoryInfo предоставляет методы экземпляра класса для создания, перемещения 
                                                                  //и перечисления в каталогах и подкаталогах
                if (directory.Exists)//Определяет существует ли заданный файл
                {
                    int number = 0;
                    Console.WriteLine($"Папка: {directory.Name}");
                    string[] files = Directory.GetFiles(path);//Метод GetFiles возвращает имена файлов, соответствующих указанным критериям
                    foreach (string s in files)
                    {
                        Console.WriteLine(s);
                        number++;
                    }
                    Console.WriteLine($"Общее количество файлов в папке: {number}");
                }
                else
                    Console.WriteLine("Такого каталога не существует");
            }
            public static void ParentDirectory(string path)
            {
                DirectoryInfo directory = new DirectoryInfo(path);//Класс DirectoryInfo предоставляет методы экземпляра класса для создания, перемещения 
                                                                  //и перечисления в каталогах и подкаталогах
                if (directory.Exists)//Определяет существует ли заданный файл
                {
                    Console.WriteLine($"Папка: {directory.Name}");
                    Console.WriteLine($"Рлдительский каталок: {directory.Parent}");
                }
            }
        }
        public static class DMVFileManager//Создаем класс DMVFileManager
        {
            public static void Task_a()
            {
                string path1 = "d:\\";
                string path2 = "d:\\DMVInspect";
                string path3 = "d:\\DMVInspect\\DMVdirInfo.txt";
                string copyPath = "d:\\DMVInspect\\DMVdirinfoCopiedAndRenamed";

                if (Directory.Exists(path1))
                {
                    Console.WriteLine("Каталоги:");
                    string[] dirs = Directory.GetDirectories(path1);//Метод GetDirectories возвращает имена подкаталогов, 
                                                                         //соответствующих указанным критериям
                    foreach (string s in dirs)
                    {
                        Console.WriteLine(s);
                    }
                    Console.WriteLine();
                    Console.WriteLine("Файлы: ");
                    string[] files = Directory.GetFiles(path1);//Метод GetFiles возвращает имена файлов, соответствующих указанным критериям
                    foreach (string s in files)
                    {
                        Console.WriteLine(s);
                    }
                }

                DirectoryInfo newDir = new DirectoryInfo(path2);
                if (!newDir.Exists)
                {
                    newDir.Create();//Создание новой папки
                    Console.WriteLine("Новая папка успешно создана");
                }

                try
                {
                    string[] dirs = Directory.GetDirectories(path1);//Метод GetDirectories возвращает имена подкаталогов, 
                                                                    //соответствующих указанным критериям
                    string[] files = Directory.GetFiles(path1);//Метод GetFiles возвращает имена файлов, соответствующих указанным критериям
                    using (StreamWriter sw = new StreamWriter(path3, true, Encoding.Default))//Класс StreamWriter реализует TextWriter для 
                                                                                        //записи символов в поток в определенной кодировке
                    {
                        sw.WriteLine("Информация о диске D:");
                        sw.WriteLine("Каталоги:");
                        foreach(string s in dirs)
                        {
                            sw.WriteLine(s);
                        }
                        sw.WriteLine("Файлы:");
                        foreach(string s in files)
                        {
                            sw.WriteLine(s);
                        }
                        Console.WriteLine("Запись выполнена!");
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                FileInfo file = new FileInfo(path3);//Класс FileInfoпредоставляет свойства и методы экземпляра 
                                    //для создания, копирования, удаления, перемещения и открытия файлов, а также позволяет создавать объекты FileStream
                if (file.Exists)
                {
                    file.CopyTo(copyPath);//Копирование в
                    file.Delete();//Удаление файла
                    Console.WriteLine("Файл скопирован и удален");
                }
            }
            public static void Task_b(string path)
            {
                string path1 = "d:\\DMVFiles";
                string path2 = "d:\\DMVInspect\\DMVFiles";
                DirectoryInfo newDir = new DirectoryInfo(path1);
                //Класс DirectoryInfo предоставляет методы экземпляра класса для создания, перемещения 
                //и перечисления в каталогах и подкаталогах
                if (!newDir.Exists)
                {
                    newDir.Create();
                    Console.WriteLine("Новая папка успешно создана!");
                }

                DirectoryInfo dir = new DirectoryInfo(path);
                //Класс DirectoryInfo предоставляет методы экземпляра класса для создания, перемещения 
                //и перечисления в каталогах и подкаталогах
                if (dir.Exists)
                {
                    foreach(FileInfo item in dir.GetFiles())
                    {
                        if(item.Name.Contains(".txt"))
                        {
                            item.CopyTo($"d:\\DMVFiles\\{item.Name}");//Копирование
                        }
                    }
                }

                DirectoryInfo directory = new DirectoryInfo(path1);
                if (directory.Exists)//Определяет существует ли заданный файл
                {
                    directory.MoveTo(path2);
                    Console.WriteLine("Перемещение прошло успешно!");
                }
            }
            public static void Task_c()
            {
                string path1 = "d:\\DMVInspect\\DMVFiles";
                string path2 = "d:\\DMVInspect\\DMVFiles\\compress.gz";
                DirectoryInfo dir = new DirectoryInfo(path1);
                foreach(FileInfo file in dir.GetFiles())
                {
                    //поток для чтения исходного файла
                    using(FileStream sourceStream = new FileStream(file.FullName, FileMode.OpenOrCreate))
                    {
                        //поток для записи сжатого файла
                        using(FileStream targetStream = File.Create(path2))
                        {
                            //поток архивации 
                            using(GZipStream gz = new GZipStream(targetStream, CompressionMode.Compress))
                            {
                                sourceStream.CopyTo(gz);//Копирование в
                                Console.WriteLine("Сжатие файла {0} завершено. Исходный размер: {1} сжатый размер: {2}.", file.FullName, sourceStream.Length.ToString(),
                                    targetStream.Length.ToString());
                            }
                        }
                    }
                }

                string path3 = "d:\\Документы";
                DirectoryInfo directory = new DirectoryInfo(path1);
                foreach(FileInfo file in directory.GetFiles())//Метод GetFiles возвращает имена файлов, соответствующих указанным критериям
                {
                    if (file.Name.Contains(".txt"))//Имя содержит ".txt"
                    {
                        using(FileStream sourceStream = new FileStream(path2, FileMode.OpenOrCreate))
                        {
                            //поток для записи востоновленного файла
                            using (FileStream targetStream = File.Create($"{path3}\\{file.Name}"))
                            //Класс FileStream предоставляет Stream в файле, поддерживая синхронные и асинхронные операции чтения и записи
                            {
                                //поток разархивации
                                using (GZipStream decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress))
                                {
                                    decompressionStream.CopyTo(targetStream);//Копиравание в
                                    Console.WriteLine("Восстановлен файл: {0}", path2);
                                }
                            }
                        }
                    }
                }
            }
        }
        
    }
}
