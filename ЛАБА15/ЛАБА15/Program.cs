
    using System;
    using System.Diagnostics;//Предоставляет классы, позволяющие осуществлять взаимодействие с системными процессами, 
                             //журналами событий и счетчиками производительности
    using System.IO;//Пространство имен содержит типы, позволяющие осуществлять чтение и запись в файлы и потоки данных, 
                //а также типы для базовой поддержки файлов и папок.
    using System.Reflection;//Пространство имен содержит типы, предназначенные для извлечения сведений о сборках, модулях, 
                            // членах, параметрах и других объектах в управляемом коде путем обработки их метаданных
    using System.Threading;//Предоставляет классы и интерфейсы для многопоточного программирования

namespace ЛАБА15
    {
        class Program
        {
            public static bool Prostie(int n)//Создаем метод с возвращаемым типом bool
            {
                bool result = true;//присваиваем переменной result - true
                if (n > 1)//выполняем проверку
                {
                //если выполняется то запускаем цикл
                    for (uint i = 2u; i < n; i++)
                    {
                        if (n % i == 0)
                        {
                            result = false;
                            break;
                        }
                    }
                }
                else
                {
                    result = false;
                }
                return result;
            }
            public static void Operation()
            {
                int n = 100;
                for (int i = 2; i <= n; i++)
                {
                    if (Prostie(i))
                    {
                        Console.Write("Второй поток:");
                        Console.WriteLine(i);
                    }
                }
            }
            public static void Event()
            {
                for (int i = 2; i < 100; i += 2)
                {
                //Класс Thread создает и контролирует поток, задает приоритет и возвращает статус
                Thread.Sleep(100);//Метод Sleep приостанавливает текущий поток на заданное время
                Console.WriteLine(i);
                }
                Thread.Sleep(20);//приостанавливаем поток
            }
            public static void NEvent()
            {
                for (int i = 1; i < 100; i += 2)//Запускаем цикл от 1 до 100 с шагом 2
                {
                    Thread.Sleep(100);// каждый шаг приостанавливаем поток
                    Console.WriteLine(i);
                }
                Thread.Sleep(20);//после цикла приостанавливаем поток 
            }
            public static void Count(object obj)
            {
                int x = (int)obj;
                for (int i = 1; i < 9; i++, x++)
                {
                    Console.WriteLine($"{x * i}");
                }
            }
            static Mutex mutexObj = new Mutex();//Класс Mutex примитив синхронизации, который также может использоваться в межпроцессной синхронизации
        static void Main(string[] args)
            {
                using (StreamWriter sw = new StreamWriter("prosesses.txt", false, System.Text.Encoding.Default))
            //Класс StreamWriter реализует TextWriter для записи символов в поток в определенной кодировке
            {
                //Класс Process позволяет управлять уже запущенными процессами, а также запускать новые
                foreach (Process p in Process.GetProcesses())//Метод GetProcesses создает массив из новых компонентов Process и связывает 
                                                            //их с существующими ресурсами процесса
                {
                        sw.WriteLine("Id " + p.Id);
                        sw.WriteLine("Name " + p.ProcessName);
                        sw.WriteLine("Priority " + p.BasePriority);
                        sw.WriteLine("Responding " + p.Responding);
                        sw.WriteLine("HandleConut " + p.HandleCount);
                        sw.WriteLine();
                    }
                }
                using (StreamWriter sw = new StreamWriter("Domain.txt"))
            //Класс StreamWriter реализует TextWriter для записи символов в поток в определенной кодировке
            {
                //Класс AppDomain представляет домен приложения, являющийся изолированной средой, в которой выполняются приложения
                AppDomain app = AppDomain.CurrentDomain;//свойство CurrentDomain возвращает текущий домен приложения для текущего объекта Thread
                sw.WriteLine("Id: " + app.Id);
                    sw.WriteLine("Name: " + app.FriendlyName);
                    sw.WriteLine("Directory: " + app.BaseDirectory);
                    Assembly[] assApp = app.GetAssemblies();//метод возвращает сборки, загруженные в контекст выполнения этого домена приложения
                //Класс  Assembly представляет сборку, которая является модулем с возможностью многократного использования, поддержкой версий и встроенным 
                //механизмом описания общеязыковой исполняющей среды
                foreach (Assembly item in assApp)
                    {
                        sw.WriteLine("Assembly name: " + item.FullName);
                    }
                }
                int n = 100;

                Thread th = new Thread(new ThreadStart(Operation));//Класс Thread создает и контролирует поток, задает приоритет и возвращает статус
            Console.WriteLine(th.ThreadState);
                Console.WriteLine(th.Priority);
                th.Name = "Second Thread";
                Console.WriteLine(th.Name);
                Console.WriteLine("id " + th.ManagedThreadId);
                th.Start();//Запускаем поток
                for (int i = 2; i <= n; i++)
                {
                    if (Prostie(i))
                    {
                        Console.Write("Главный поток:");
                        Console.WriteLine(i);
                    }
                }
                Thread.Sleep(1000);//приостанавливаем поток

                Thread th1 = new Thread(new ThreadStart(Event));//Класс Thread создает и контролирует поток, задает приоритет и возвращает статус
            Thread th2 = new Thread(new ThreadStart(NEvent));
                th1.Priority = ThreadPriority.AboveNormal;//Свойство AboveNormal возвращает или задает значение, указывающее на планируемый приоритет потока
            th1.Start();//Запускаем поток th1
            th2.Start();//Запускаем поток th2

            Console.ReadLine();
                int num = 0;
                TimerCallback tm = new TimerCallback(Count);//Делегат TimerCallback представляет метод, обрабатывающий вызовы от события Timer
            Timer timer = new Timer(tm, num, 0, 2000); //класс Timer создает событие после заданного интервала с возможностью создания повторяющихся событий
            Console.ReadLine();
            }
        }
    }

