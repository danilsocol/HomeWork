using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.IO;
namespace Lab
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> list = new List<int>();

            CreateList(list);
            //OutputData(list, СonstantFunction);
            //OutputData(list, SumOfElements);
            //OutputData(list, СompositionfElements);
            //OutputData(list, GornerMethod);
            // OutputData(list, BubbleSort); 
            OutputData(list, QuickSort);

        }

        static void OutputData(List<int> list,Action<List<int>,int> f)
        {
            List<int> time = new List<int>();

            for (int j = 0; j < 10; j++)
            {
                for (int i = 0; i < 5; i++)
                {
                    FindTime(list, f, time,j+i);
                }

                int sum = 0;
                for (int i = 0; i < time.Count; i++)
                {
                    sum += time[i];
                }
                sum /= time.Count;

                using (StreamWriter file = new StreamWriter("Time.txt", true))
                {
                    file.WriteLine(sum);
                    file.Close();
                }
                Console.WriteLine(sum);
                time.Clear();
            }
        }

        static void CreateList(List<int> list)
        {
            Random rnd = new Random();
            for (int i = 0; i < 100; i++)
            {
                list.Add(rnd.Next(1,9));
            }
        }

        static void FindTime(List<int> list, Action<List<int>,int> f, List<int> time,int count)
        {
            List<int> newList = new List<int>();
            newList.AddRange(list.ToArray());

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            f(newList, count);
            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}", ts.Milliseconds);
            Console.WriteLine(elapsedTime);

            time.Add(Convert.ToInt32(elapsedTime));
        }

        static void СonstantFunction(List<int> list,int count)
        {
            for(int i=0; i < list.Count; i++)
            {
                Console.WriteLine(list[i]);
            }
        }

        static void SumOfElements(List<int> list, int count)
        {
            int sum = 0;
            for (int i = 0; i < count*3; i++)
            {
                sum = sum + list[i];
                Console.WriteLine(sum);
            }
        }

        static void СompositionfElements(List<int> list, int count)
        {
            ulong сomposition = 1;
            for (int i = 0; i < count * 3; i++)
            {
                сomposition = сomposition * (ulong)list[i];
                Console.WriteLine(сomposition);
            }
        }
        static void GornerMethod(List<int> list, int count)
        {
            ulong num = 1;
            for (int i = 0; i < count * 3; i++)
            {
                num =num + (ulong)list[i] * (ulong)Math.Pow(1.5, count - i);
                Console.WriteLine(num);
            }
        }

        static void Swap(List<int> list, int i, int j)
        {
            int temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }

        static void BubbleSort(List<int> list,int count)
        {
            for (int i = 0; i < count * 3; i++)
            {
                for (int j = i + 1; j < count * 3; j++)
                {
                    if (list[i] > list[j])
                    {
                        Swap(list, i, j);
                    }
                }
            }
            Console.WriteLine();
        }

        static int Partition(List<int> list, int minIndex, int maxIndex)
        {
            var pivot = minIndex - 1;
            for (var i = minIndex; i < maxIndex; i++)
            {
                if (list[i] < list[maxIndex])
                {
                    pivot++;
                    Swap( list,pivot, i);
                }
            }

            pivot++;
            Swap(list, pivot,maxIndex);
            return pivot;
        }

        static List<int> QuickSort(List<int> list, int minIndex, int maxIndex)
        {
            if (minIndex >= maxIndex)
            {
                return list;
            }

            var pivotIndex = Partition(list, minIndex, maxIndex);
            QuickSort(list, minIndex, pivotIndex - 1);
            QuickSort(list, pivotIndex + 1, maxIndex);

            return list;
        }

        static void QuickSort(List<int> list,int count)
        {
             QuickSort(list, 0, count);
        }

    }
}