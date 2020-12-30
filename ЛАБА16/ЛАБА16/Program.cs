using System;
using System.Collections.Generic;//подключение пространства имен обобщенных или типизированных классов коллекций
using System.Linq;//подключаем пространство имен System.Linq
using System.Text;
using System.Collections.Concurrent;//Предоставляет несколько потокобезопасных классов коллекций, которые следует использовать вместо соответствующих типов в 
//пространствах имен System.Collections и System.Collections.Generic, если несколько потоков параллельно обращаются к такой коллекции
using System.Threading;//Предоставляет классы и интерфейсы для многопоточного программирования
using System.Diagnostics;//Предоставляет классы, позволяющие осуществлять взаимодействие с системными процессами, 
//журналами событий и счетчиками производительности
using System.Threading.Tasks;

namespace ЛАБА16

{

    class Program

    {

        static BlockingCollection<int> bc;

        static async void SayAsync()//async - метод, который вызывает асинхронную функцию

        {

            await Task.Run(() => Console.WriteLine("Async stream"));//Оператор await приостанавливает вычисление до завершения 
            //асинхронной операции. После завершения асинхронной операции оператор await возвращает результат операции

        }//Run Запускает стандартный цикл обработки сообщений приложения в текущем потоке

        static void producer()

        {

            for (int i = 0; i < 10; i++)

            {

                bc.Add(i * i);//позволяет добавить новый элемент в конец списка

                Console.WriteLine("Производится число " + i * i);

            }

            bc.CompleteAdding();//Помечает экземпляры BlockingCollection<T> как не допускающие добавления дополнительных элементов

        }

        static void consumer()

        {

            int i;

            while (!bc.IsCompleted)//Получает значение, указывающее, завершена ли задача

            {

                if (bc.TryTake(out i))//Пытается удалить элемент из коллекции BlockingCollection<T>

                    Console.WriteLine("Потребляется число: " + i);

            }

        }

        public static Task task1;

        public static void EasyNumbersIrato()//простые числа

        {

            Console.WriteLine("Current task ID: " + Task.CurrentId.ToString());//Возвращает идентификатор выполняющейся в настоящее
            //время задачи Task

            Console.WriteLine("Task Completed: " + task1.IsCompleted.ToString());//Получает значение, указывающее, завершена ли задача

            Console.WriteLine("Status: " + task1.Status.ToString());//Получает состояние данной задачи

            int i, j, n = 100;

            int[] mas = new int[n];

            for (i = 0; i < n; i++)

                mas[i] = i + 1;

            for (i = 1; i < n - 1; i++)

                if (mas[i] != -1)

                    for (j = i + 1; j < n; j++)

                        if ((mas[j] != -1) && (mas[j] % mas[i] == 0))

                            mas[j] = -1;

            for (i = 0; i < n; i++)

                if (mas[i] != -1)

                    Console.WriteLine(mas[i]);

        }

        static int Factorial(int x)

        {

            int result = 1;

            for (int i = 1; i <= x; i++)

            {

                result *= i;

            }

            Console.WriteLine("Current task ID: " + Task.CurrentId.ToString());////Возвращает идентификатор выполняющейся в настоящее 
            //время задачи Task

            Console.WriteLine("Result: " + result.ToString());

            return result;

        }

        static int Sum(int a, int b, int c)

        {

            return a + b + c;

        }

        static void Main(string[] args)

        {

            Console.WriteLine("Задание 1");

            Action action1 = new Action(EasyNumbersIrato);//delegate Инкапсулирует метод, который не имеет параметров и не возвращает 
                                                                       //значений

            Stopwatch watch = Stopwatch.StartNew();//Предоставляет набор методов и свойств, которые можно использовать для точного 
                                                   //измерения затраченного времени. StartNew Создает и запускает задачу

            task1 = Task.Factory.StartNew(action1); //StartNew Создает и запускает задачу

            task1.Wait();//Ожидает завершения выполнения задачи

            task1.Dispose();//Реализация метода Dispose в основном используется для освобождения неуправляемых ресурсов.

            watch.Stop();//вызов Stop завершает текущую меру интервала и замораживает совокупное значение затраченного времени

            Console.WriteLine("Time for task: " + watch.Elapsed.ToString());//событие, которое Происходит по истечении интервала времени

            Console.WriteLine("Task Completed: " + task1.IsCompleted.ToString());//Получает значение, указывающее, завершена ли задача

            Console.WriteLine("Status: " + task1.Status.ToString());//Получает состояние данной задачи

            Console.WriteLine();

            Console.WriteLine("Задание 2");

            CancellationTokenSource cancellation = new CancellationTokenSource();//создаем токен-ресурс, то есть отмену выполнения задачи

            task1 = Task.Factory.StartNew(action1, cancellation.Token);////StartNew Создает и запускает задачу и используем токен-ресурс

            cancellation.Cancel();//Возвращает или задает значение, показывающее, следует ли отменить событие

            Console.WriteLine("Task Canceled: " + task1.IsCanceled.ToString());//Возвращает значение, указывающее, завершилось ли 
                                                                               //выполнение данного экземпляра Task из-за отмены

            Console.WriteLine("Status: " + task1.Status.ToString());//Получает состояние данной задачи

            Console.WriteLine();

            Console.WriteLine("Задание 3 - 4");

            Task<int> fact1 = new Task<int>(() => Factorial(5));//Task представляет собой работу над потоками для выполнения асинхронных 
                                                                //операций

            Task<int> fact2 = fact1.ContinueWith(a => Factorial(10));//Создает продолжение, которое выполняется асинхронно после завершения выполнения целевой задачи

            Task<int> fact3 = fact2.ContinueWith(a => Factorial(15));

            Task<int> res = new Task<int>(() => Sum(fact1.Result, fact2.Result, fact3.Result));

            fact1.Start();//Запускает ресурс процесса и связывает его с компонентом Process

            res.Start();//Запускает ресурс процесса и связывает его с компонентом Process

            var awaiter = res.GetAwaiter();//Получает объект типа awaiter, используемый для данного объекта Task

            awaiter.OnCompleted(() => Console.WriteLine("Last Result:" + awaiter.GetResult()));//Уведомляет наблюдателя о том, что 
                                 //поставщик завершил отправку push-уведомлений. GetResult Завершает ожидание завершения асинхронной задачи

            Thread.Sleep(1000);

            Console.WriteLine();

            Console.WriteLine("Задание 5");

            watch = Stopwatch.StartNew();//StartNew Создает и запускает задачу

            int[] array = new int[100000000];

            ParallelLoopResult result = Parallel.For(0, 10000000, (int z, ParallelLoopState loop) => { array[z] = z + 1; if (z == 1000) loop.Break(); });

            //ParallelLoopResult Предоставляет состояние выполнения цикла Parallel. Parallel.For - Выполняет цикл for, в котором итерации 
            //могут выполняться параллельно

            if (result.IsCompleted) Console.WriteLine("Выполнен");////Получает значение, указывающее, завершена ли задача

            else Console.WriteLine("Выполнение цикла завершено на итерации {0}", result.LowestBreakIteration.ToString());//Получает первую 
                                                                                 //итерацию цикла, из которой был прервал метод

            watch.Stop();//вызов Stop завершает текущую меру интервала и замораживает совокупное значение затраченного времени

            Console.WriteLine("Time for task: " + watch.Elapsed.ToString());////событие, которое Происходит по истечении интервала времени

            Console.WriteLine();

            Console.WriteLine("Задание 6");

            Parallel.Invoke(() =>//Выполняет все предоставленные действия, по возможности в параллельном режиме

            {

                for (int i = 0; i < 10; i++)

                {

                    Console.Write("1 ");

                }

            },

            () =>

            {

                for (int i = 0; i < 10; i++)

                {

                    Console.Write("2 ");

                }

            });

            Console.WriteLine();

            Console.WriteLine("\nЗадание 7");

            bc = new BlockingCollection<int>(5);// Создадим задачи поставщика и потребителя

            Task Pr = new Task(producer);

            Task Cn = new Task(consumer);

            Pr.Start();//Запускает ресурс процесса и связывает его с компонентом Process

            Cn.Start();//Запускает ресурс процесса и связывает его с компонентом Process

            try

            {

                Task.WaitAll(Cn, Pr);//Ожидает завершения выполнения всех указанных объектов Task

            }

            catch (Exception ex)

            {

                Console.WriteLine(ex);

            }

            finally

            {

                Cn.Dispose();////Реализация метода Dispose в основном используется для освобождения неуправляемых ресурсов

                Pr.Dispose();

                bc.Dispose();

            }

            Console.WriteLine("\nЗадание 8");

            SayAsync();

            Console.WriteLine("I\'m first!");

            Console.ReadKey();

        }

    }

}