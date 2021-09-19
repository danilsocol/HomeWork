using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Numerics;

namespace Lab
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> list = new List<int>();

            CreateList(list);
            OutputData(list, СonstantFunction, "СonstantFunction");
            OutputData(list, SumOfElements, "SumOfElements");
            OutputData(list, СompositionfElements, "СompositionfElements");
            OutputData(list, GornerMethod, "GornerMethod");
            OutputData(list, BubbleSort, "BubbleSort");
            OutputData(list, QuickSort, "QuickSort");
            OutputData(list, Pow, "Pow");
            OutputData(list, RecPow, "RecPow");
            OutputData(list, QuickPow, "QuickPow");
            OutputData(list, QuickPowTwo, "QuickPowTwo");
            
        }

        static void OutputData(List<int> list, Action<List<int>, int> f, string nameAction)
        {
            List<int> time = new List<int>();

            using (StreamWriter file = new StreamWriter("Time.txt", true))
            {
                file.Write($"{nameAction};;");

                for (int j = 0; j < 50; j++)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        FindTime(list, f, time, j + i);
                    }

                    int sum = 0;
                    for (int i = 0; i < time.Count; i++)
                    {
                        sum += time[i];
                    }
                    sum /= time.Count;

                   
                    file.Write($"{sum};");
                    time.Clear();
                }

                file.WriteLine();
                file.Close();
            }
        }

        static void CreateList(List<int> list)
        {
            Random rnd = new Random();
            for (int i = 0; i < 100; i++)
            {
                list.Add(rnd.Next(1, 9));
            }
        }

        static void FindTime(List<int> list, Action<List<int>, int> f, List<int> time, int count)
        {
            List<int> newList = new List<int>();
            newList.AddRange(list.ToArray());

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            f(newList, count);
            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}", ts.Ticks);
            Console.WriteLine(elapsedTime);

            time.Add(Convert.ToInt32(elapsedTime));
        }

        static void СonstantFunction(List<int> list, int count)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list[i]);
            }
        }

        static void SumOfElements(List<int> list, int count)
        {
            BigInteger sum = 0;
            for (int i = 0; i < count; i++)
            {
                sum = sum + list[i];
                Console.WriteLine(sum);
            }
        }

        static void СompositionfElements(List<int> list, int count)
        {
            BigInteger сomposition = 1;
            for (int i = 0; i < count; i++)
            {
                сomposition = сomposition * (BigInteger)list[i];
                Console.WriteLine(сomposition);
            }
        }
        static void GornerMethod(List<int> list, int count)
        {
            BigInteger num = 1;
            for (int i = 0; i < count; i++)
            {
                num = num + (BigInteger)list[i] * (BigInteger)Math.Pow(1.5, count - i);
                Console.WriteLine(num);
            }
        }

        static void Swap(List<int> list, int i, int j)
        {
            int temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }

        static void BubbleSort(List<int> list, int count)
        {
            for (int i = 0; i < count; i++)
            {
                for (int j = i + 1; j < count; j++)
                {
                    if (list[i] > list[j])
                    {
                        Swap(list, i, j);
                    }
                }
            }
            Console.WriteLine();
        }

        static void QuickSwap(ref int x, ref int y)
        {
            var t = x;
            x = y;
            y = t;
        }
        static int Partition(int[] array, int minIndex, int maxIndex)
        {
            var pivot = minIndex - 1;
            for (var i = minIndex; i < maxIndex; i++)
            {
                if (array[i] < array[maxIndex])
                {
                    pivot++;
                    QuickSwap(ref array[pivot], ref array[i]);
                }
            }

            pivot++;
            QuickSwap(ref array[pivot], ref array[maxIndex]);
            return pivot;
        }

        static int[] QuickSort(int[] array, int minIndex, int maxIndex)
        {
            if (minIndex >= maxIndex)
            {
                return array;
            }

            var pivotIndex = Partition(array, minIndex, maxIndex);
            QuickSort(array, minIndex, pivotIndex - 1);
            QuickSort(array, pivotIndex + 1, maxIndex);

            return array;
        }

        static void QuickSort(List<int> list, int count)
        {
            int[] newList = new int[list.Count-1];
            newList = list.ToArray();

            QuickSort(newList, 0, count);
        }

        static void Pow(List<int> list, int count)
        {
            int num = FindNumForPow(list);

            BigInteger pow = 1;
            for (int i = 0; i < count; i++)
            {
                pow *= num;
            }

        }
        static void RecPow(List<int> list, int count)
        {
            int num = FindNumForPow(list);
            BigInteger i = 0;
            BigInteger f = RecPow(num, count);
            Console.WriteLine();

        }

        static BigInteger h;
        static BigInteger RecPow(int num, int count)
        {
            BigInteger pow = 1;
            if (count == 0)
            {
                h = 1;
            }
            else
            {
                pow = RecPow(num, count / 2);
                if (count % 2 == 1)
                {
                    h = h * h * num;
                }
                else
                {
                    h *= h;
                }
            }
            return h;
        }

        
        static void QuickPow(List<int> list, int count)
        {
            BigInteger num = FindNumForPow(list);
            int k = count;
            BigInteger f = 0;

            if (k % 2 == 1)
            {
                f = num;
            }
            else f = 1;

            while (k != 0)
            {
                k = k / 2;
                num *= num;

                if (k%2==1)
                {
                    f = f * num;
                }
            }
        }

        static int FindNumForPow(List<int> list)
        {
            int index = 2;
            for (int j = 0; j < list.Count - 1; j++)
            {
                if (list[j] != 1)
                {
                    index = j;
                    break;
                }
            }

            return list[index];
        }

        static void QuickPowTwo(List<int> list, int count)
        {
            BigInteger num = FindNumForPow(list);
            BigInteger f = 1;
            int k = count;

            while (k!=0)
            {
                if (k%2==0)
                {
                    num *= num;
                    k = k / 2;
                }
                else
                {
                    f *= num;
                    k--;
                }
            }
            Console.WriteLine();
        }
    }
}

