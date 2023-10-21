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
            int[] arr = new int[2];
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
                            task1_2();
                            Console.ReadKey();
                        }
                        break;
                    case '2':
                        {
                            Console.WriteLine("Enter min and max numbers");
                            min = Convert.ToInt32(Console.ReadLine());
                            max = Convert.ToInt32(Console.ReadLine());

                            if (!isCorrectInput(min, max))
                            {
                                Console.WriteLine("Uncorrect input!");
                                Console.ReadKey();
                                break;
                            }
                            arr[0] = min;
                            arr[1] = max;
                            task1_2(arr);
                            Console.ReadKey();
                        }
                        break;
                    case '3':
                        {

                            Console.WriteLine("Enter min and max numbers:");
                            min = Convert.ToInt32(Console.ReadLine());
                            max = Convert.ToInt32(Console.ReadLine());

                            Console.WriteLine("Enter count of threads");
                            count = Convert.ToInt32(Console.ReadLine());

                            if (!isCorrectInput(min, max) || count < 1)
                            {
                                Console.WriteLine("Uncorrect input!");
                                Console.ReadKey();
                                break;
                            }
                            task3(count, min, max);
                            Console.ReadKey();
                        }
                        break;
                    case '4':
                        {                           
                            task4();
                            Console.ReadKey();
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
        private static bool isCorrectInput(int min, int max)
        {
            if (min > max)
                return false;
            else
                return true;
        }
        private static void task1_2(object obj = null)
        {
            int[] arr = ((int[])obj);
            int min = arr != null ? arr[0] : 0;
            int max = arr != null ? arr[1] : 51;
            int tCount = arr != null ? arr.Length > 2 ? arr[2] : 0:0;
            
            string tabs = new string('\t', tCount);
            Thread thread = new Thread(() =>
             {
                 for (int i = min; i < max; i++)
                     Console.WriteLine($"{tabs} | {i} |");
             });
            thread.Start();
        }


        private static void task3(int count, int min, int max) 
        {
            int someVal = max /count;
            int[] arr = new int[3];

            arr[0] = min;
            arr[1] = someVal;

            for(int i =0;i<count;i++)
                Console.Write($"Thread {i+1}|");

            Console.WriteLine();

            for (int i = 0; i < count; i++)
            {
                arr[0] = min + someVal * i;
                arr[1] = min + someVal * (i + 1);
                arr[2] = i;

                task1_2(arr);
            }
            Console.ReadKey();
        }
        private static void task4()
        {
            double avg = 0;

            int max = 0;
            int min = 0;

            var list = new List<int>();

            Random r = new Random();

            for (int i =0;i<1000;i++)
                list.Add(r.Next(-50, 100));
            Thread.Sleep(10);

            Thread minimalValue = new Thread(() =>
            {
                 min = 0;
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
                string path = Directory.GetCurrentDirectory() + @"\" + "log.txt";
  
                foreach (var item in list)
                    textToSave += $"{item} ";
                textToSave += $"\nMin: {min};\nMax: {max};\nAvg: {avg}\n";

                if (!File.Exists(path))
                    File.Create(path).Close();

                File.AppendAllText(path, textToSave);
            
            });
            averageValue.Join();
            minimalValue.Join();
            maximalValue.Join();
            
            saveResult.Start();
                      
        }
    }
}
