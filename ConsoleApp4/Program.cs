using System.Collections.Generic;
using System.IO;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
          
            char q = ' ';
            int min = 0;
            int max = 10;
            int count = 1;
            q = ' ';
            while (q != 'e')
            {
               Console.Clear();
                Console.WriteLine("Choose task: [1 - 4] (e - exit)");
                q = Console.ReadKey().KeyChar;
                Console.WriteLine();
                switch (q)
                {
                    case '1':
                        {
                            task1();
                        }break;
                    case '2':
                        {
                            Console.WriteLine("Enter min and max numbers");
                            min = Convert.ToInt32(Console.ReadLine());
                            max = Convert.ToInt32(Console.ReadLine());
                            if (min > max)
                            {
                                while (min > max)
                                {
                                    Console.WriteLine("wrong values!");
                                    Console.WriteLine("Enter min and max numbers");
                                    min = Convert.ToInt32(Console.ReadLine());
                                    max = Convert.ToInt32(Console.ReadLine());
                                }

                            }
                            task2(min, max);
                        }
                        break;
                    case '3':
                        {

                            Console.WriteLine("Enter min and max numbers:");
                            min = Convert.ToInt32(Console.ReadLine());
                            max = Convert.ToInt32(Console.ReadLine());
                            if (min > max)
                                while (min > max)
                                {
                                    Console.WriteLine("wrong values! pls try again");
                                    Console.WriteLine("Enter min and max numbers:");
                                    min = Convert.ToInt32(Console.ReadLine());
                                    max = Convert.ToInt32(Console.ReadLine());
                                }

                            count = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter count of threads:");
                            if (count == 0)
                                while (count == 0)
                                {
                                    Console.WriteLine("counter is zero! pls try again");
                                    count = Convert.ToInt32(Console.ReadLine());

                                }
                            task3(count, min, max);
                        }
                        break;
                    case '4':
                        {                           
                            task4();
                        }
                        break;
                    case 'e':
                        {
                          
                            q = 'e';
                        }
                        break;
                  
                    default:
                        {
                            Console.WriteLine("Error input!");
                            Console.ReadKey();
                        }
                        break;
                }
            }
        }
        private static void forTask2(object obj) 
        {
            int[] minMax = (int[])obj;
            int min = minMax[0];
            int size = (minMax[1] - min)+1;
            
            var arr = new int[size];
            for (int i = 0; i < size; i++)
                arr[i] = min+i;

            foreach (int i in arr)
            {
                Console.WriteLine($"Thread: \t{i}");
            }

        }
        private static void task1() 
        {         
            Thread thread = new Thread(() => 
            {
                var arr = new int[51];
                for (int i = 0; i < 51; i++)
                    arr[i] = i;

                foreach (int i in arr)
                {
                    Console.WriteLine($"Thread: \t{i}");
                }
            });
            thread.Start();
            Console.ReadKey();
        }
        private static void task2(int min, int max)
        {
            var arr = new int[2];       
                arr[0] = min;
                 arr[1]= max;

            Thread thread = new Thread(new ParameterizedThreadStart(forTask2));
            thread.Start(arr);

            Console.ReadKey();
        }
        private static void task3(int count, int min, int max) 
        {
            int mult = 0;
            int size = (max - min) +1;
            var arr = new int[size];

            for (int i = 0; i < size; i++)
                arr[i] = min + i;

            for (int i = 0; i < count; i++)
            {
                Thread thread = new Thread(() =>
                {
                    for (int j = mult; j <  size/count+mult; j++)
                            Console.WriteLine($"Thread {i + 1}: \t{arr[j]}\t{mult}");                  
                });
                thread.Start();              
                thread.Join();
                mult += size/count;
            }

            Console.ReadKey();
        }
        private static void task4()
        {
            double avg = 0;
            int max = 0;
            int min = 0;
            var list =new List<int>();
            Random r = new Random();
            for (int i =0;i<1000;i++)
                list.Add(r.Next(-50, 101));
            Thread.Sleep(10);
            Thread minimalValue = new Thread(() =>
            {
                int min = list[0];
                foreach (var value in list)
                    if (value < min)
                        min = value;
                Console.WriteLine($"Minimal value is: {min}");       
            });
            Thread maximalValue = new Thread(() =>
            {
                max = list[0];
                foreach (var value in list)
                    if (value > max)
                        max = value;
                Console.WriteLine($"Maximal value is: {max}");
            });
            Thread averageValue = new Thread(() =>
            {               
                double sum = 0;
                foreach (var value in list)
                    avg += value;
                avg /=list.Count;
                Console.WriteLine($"Average is: {avg}");
            });

            averageValue.Start();
            minimalValue.Start();
            maximalValue.Start();
            Thread saveResult = new Thread(()=>
            {
                string textToSave = $"______{DateTime.Now}_______\n Values: ";
                foreach (var item in list)
                    textToSave += $"{item} ";
                textToSave += $"\nMin: {min};\nMax: {max};\nAvg: {avg}";
                if (!File.Exists(Directory.GetCurrentDirectory() + @"\" + "log.txt"))
                    File.Create(Directory.GetCurrentDirectory() + @"\" + "log.txt").Close();
                File.WriteAllText(Directory.GetCurrentDirectory() + @"\" + "log.txt", textToSave);
            });
            saveResult.Start();
            Console.ReadKey();
        }
    }
}
