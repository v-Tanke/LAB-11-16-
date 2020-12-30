using System;
using lab_13;

namespace Lab_13
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("====================Task1====================");
            Console.WriteLine("Введите название диска");
            string DriveName = Console.ReadLine();
            DMVLog.DMVDiskInfo.ShowFreeSpace(DriveName);
            DMVLog.DMVDiskInfo.ShowFileSystem(DriveName);
            Console.WriteLine(" ");
            DMVLog.DMVDiskInfo.AllInfo();
            Console.WriteLine("=============================================");
            Console.WriteLine("====================Task2====================");
            Console.WriteLine("Введите путь к файлу о котором будем получать информацию");
            string path1 = Console.ReadLine();
            DMVLog.DMVFileInfo.ShowFilePath(path1);
            DMVLog.DMVFileInfo.ShowFileSizeExtName(path1);
            DMVLog.DMVFileInfo.ShowCreationFile(path1);
            Console.WriteLine("=============================================");
            Console.WriteLine("====================Task3====================");
            Console.WriteLine("Введите путь к папке о которой будем получать информацию");
            string path2 = Console.ReadLine();
            DMVLog.DMVDirInfo.ListOfDirectory(path2);
            DMVLog.DMVDirInfo.CreationTime(path2);
            DMVLog.DMVDirInfo.NumerOfFiles(path2);
            DMVLog.DMVDirInfo.ParentDirectory(path2);
            Console.WriteLine("=============================================");
            Console.WriteLine("====================Task4====================");
            DMVLog.DMVFileManager.Task_a();
            Console.WriteLine("Введите путь к папке, из которой будут скопированы все файлы с расширением txt:");
            string path3 = Console.ReadLine();
            DMVLog.DMVFileManager.Task_b(path3);
            DMVLog.DMVFileManager.Task_c();
            Console.WriteLine("=============================================");
        }
    }
}
