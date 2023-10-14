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
            while (q != 'q')
            {
                Console.Clear();
                Console.WriteLine("Choose task: [1 - 5]");
                q = Console.ReadKey().KeyChar;
                Console.WriteLine();
                switch (q)
                {
                    case '1':
                        {
                            task1();
                        }
                        break;

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
                            count = Convert.ToInt32(Console.ReadLine());
                            task3(count, min ,max);
                        }
                        break;

                    case '4':
                        {

                        }
                        break;

                    case '5':
                        {

                        }
                        break;

                    case 'q':
                        {
                            q = 'q';
                            break;
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

        }
    }
}