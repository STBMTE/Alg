using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Algorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            Algoritm[,] PowAlgArray = new Algoritm[5, 2001];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j <= 2000; j++)
                {
                    if (i == 0)
                    {
                        PowAlgArray[i, j] = new Pow(new int[] { j });
                    }
                    if (i == 1)
                    {
                        PowAlgArray[i, j] = new RecPow(new int[] { j });
                    }
                    if (i == 2)
                    {
                        PowAlgArray[i, j] = new QuickPow(new int[] { j });
                    }
                    if (i == 3)
                    {
                        PowAlgArray[i, j] = new ClassicQuickPow(new int[] { j });
                    }
                }
            }
            for (int i = 0; i <= 2000; i++)
            {
                PowAlgArray[4, i] = new Matrix(new int[i]);
            }
            try
            {
                using (StreamWriter SW = new StreamWriter("../../../Exponent1.csv", false, Encoding.Default))
                {
                    for (int i = 1; i <= 2000; i++)
                    {
                        SW.WriteLine($"{i};{PowAlgArray[0, i].Result};{PowAlgArray[1, i].Result};{PowAlgArray[2, i].Result};{PowAlgArray[3, i].Result};{PowAlgArray[4, i].Result}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            alg al = new alg();
        }
    }

    abstract class aMatrix : Algoritm
    {
        public override abstract long Result { get; set; }

        public override abstract int PerformingAlgoritm(int[] param);
        public abstract int PerformingAlgoritm(int[,] FirstMatrix, int[,] LastMatrix);
        public override long ElapsedTime(int[] param)
        {
            long time = 0;
            var sw = new Stopwatch();
            int[,] a = GenerationMatrix(param.GetUpperBound(0) + 1);
            int[,] b = GenerationMatrix(param.GetUpperBound(0) + 1);
            sw.Start();
            int result = PerformingAlgoritm(a, b);
            sw.Stop();
            time = sw.ElapsedTicks;
            Result = time;
            sw.Reset();
            return time;
        }

        public virtual int[,] GenerationMatrix(int length)
        {
            int[,] Matri = new int[length, length];
            Random rnd = new Random();
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    Matri[i, j] = Math.Abs(rnd.Next());
                }
            }
            return Matri;
        }
    }

    abstract class Algoritm
    {
        public abstract long Result { get; set; }
        public abstract int PerformingAlgoritm(int[] @param);
        public virtual long ElapsedTime(int[] @param)
        {
            long time = 0;
            var sw = new Stopwatch();
            sw.Start();
            int result = PerformingAlgoritm(@param);
            sw.Stop();
            time = sw.ElapsedTicks;
            Result = time;
            sw.Reset();
            return time;
        }
    }

    class Pow : Algoritm
    {
        public override long Result { get; set; }

        public Pow(int[] @param)
        {
            ElapsedTime(param);
        }
        public override int PerformingAlgoritm(int[] param)
        {
            int result = 1;
            for (float i = 0; i < param[0]; i++)
            {
                result *= param[0];
            }
            return result;
        }
    }

    class RecPow : Algoritm
    {
        public override long Result { get; set; }
        public RecPow(int[] @param)
        {
            ElapsedTime(param);
        }
        public override int PerformingAlgoritm(int[] param)
        {
            int n;
            if (param.Length == 1)
            {
                n = param[0];
            }
            else
            {
                n = param[1];
            }
            int f;
            if (param[0] == 0)
            {
                f = 1;
            }
            if (n == 0)
            {
                f = 1;
            }
            else
            {
                f = PerformingAlgoritm(new int[] { param[0], n / 2 });
                if (n % 2 == 1)
                {
                    f = f * f * param[0];
                }
                else
                {
                    f = f * f;
                }
            }
            return f;
        }
    }

    class QuickPow : Algoritm
    {
        public override long Result { get; set; }
        public QuickPow(int[] @param)
        {
            ElapsedTime(param);
        }
        public override int PerformingAlgoritm(int[] param)
        {
            int c = param[0];
            int k = param[0];
            int f;
            if (k % 2 == 1)
            {
                f = c;
            }
            else
            {
                f = 1;
            }
            while (k != 0)
            {
                k = k / 2;
                c = c * c;
                if (k % 2 == 1)
                {
                    f = f * c;
                }
            }
            return f;
        }
    }

    class ClassicQuickPow : Algoritm
    {
        public override long Result { get; set; }
        public ClassicQuickPow(int[] @param)
        {
            ElapsedTime(param);
        }
        public override int PerformingAlgoritm(int[] param)
        {
            int c = param[0];
            int f = 1;
            int k = param[0];
            while (k != 0)
            {
                if (k % 2 == 0)
                {
                    c = c * c;
                    k = k / 2;
                }
                else
                {
                    f = f * c;
                    k = k - 1;
                }
            }
            return f;
        }
    }

    class Matrix : aMatrix
    {
        public override long Result { get; set; }

        public Matrix(int[] @param)
        {

            ElapsedTime(param);
        }
        public override int PerformingAlgoritm(int[,] Matrix1, int[,] Matrix2)
        {
            Multiplay(Matrix1, Matrix2);
            return 0;
        }

        private void Multiplay(int[,] a, int[,] b)
        {
            if (a.GetUpperBound(0) + 1 != 0 && b.GetUpperBound(0) + 1 != 0)
            {
                int[,] ResultMatrix = new int[a.GetUpperBound(0) + 1, b.GetUpperBound(0) + 1];
                for (int i = 0; i < a.GetUpperBound(0) + 1; i++)
                {
                    for (int j = 0; j < b.GetUpperBound(0) + 1; j++)
                    {
                        ResultMatrix[i, j] = a[i, j] * b[j, i];
                    }
                }
            }
        }

        public override int PerformingAlgoritm(int[] param)
        {
            throw new NotImplementedException();
        }
    }
    class alg
    {
        public alg()
        {
            long[,] time = performing();
            Write(time);
        }
        public long[,] performing()
        {
            long[,] Time = new long[8, 2000];
            var sw = new Stopwatch();
            for (int i = 0; i < 2000; i++)
            {
                int[] array = VectorGeneration(i);
                long time = 0;
                sw.Start();
                Func1(i);
                sw.Stop();
                Time[0, i] = sw.ElapsedTicks;
                sw.Restart();
                Func2(array);
                sw.Stop();
                Time[1, i] = sw.ElapsedTicks;
                sw.Restart();
                Func3(array);
                sw.Stop();
                Time[2, i] = sw.ElapsedTicks;
                sw.Restart();
                BubbleSort(array);
                sw.Stop();
                Time[3, i] = sw.ElapsedTicks;
                sw.Restart();
                StartQuickSort(array);
                sw.Stop();
                Time[4, i] = sw.ElapsedTicks;
                sw.Restart();
                TimSortCall(array);
                sw.Stop();
                Time[5, i] = sw.ElapsedTicks;
                sw.Restart();
                CountingSort(array);
                sw.Stop();
                Time[6, i] = sw.ElapsedTicks;
                sw.Restart();
                Heapsort(array);
                sw.Stop();
                Time[7, i] = sw.ElapsedTicks;
            }
            return Time;
        }
        public void Write(long[,] array)
        {
            try
            {
                using (StreamWriter SW = new StreamWriter("../../../sort.csv", false, Encoding.Default))
                {
                    for (int i = 0; i < 2000; i++)
                    {
                        SW.WriteLine($"{i};{array[0, i]};{array[1, i]};{array[2, i]};{array[3, i]};{array[4, i]};{array[5, i]};{array[6, i]};{array[7, i]}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public int[] VectorGeneration(int n)
        {
            Random rnd = new Random();
            int[] nArr = new int[n];
            for (int i = 0; i < n; i++)
            {
                nArr[i] = Math.Abs(rnd.Next() % 3000);
            }
            return nArr;
        }

        public void Func1(int n)
        {
            DateTime dt = new DateTime();
            dt = DateTime.Now;
        }

        public void Func2(int[] n)
        {
            int a = 0;
            for (int i = 0; i < n.Length; i++)
            {
                a += n[i];
            }
        }

        public void Func3(int[] n)
        {
            long a = 0;
            for (int i = 0; i < n.Length; i++)
            {
                a = a * n[i];
            }
        }

        public void BubbleSort(int[] n)
        {
            for (int i = 0; i < n.Length; i++)
            {
                for (int j = 0; j < n.Length - i - 1; j++)
                {
                    if (n[j] > n[j + 1])
                    {
                        int c = n[j];
                        n[j] = n[j + 1];
                        n[j + 1] = c;
                    }
                }
            }
        }

        public void StartQuickSort(int[] array)
        {
            quicksort(array, 0, array.Length - 1);
        }
        private int partition(int[] array, int start, int end)
        {
            int marker = start;
            for (int i = start; i <= end; i++)
            {
                if (array[i] <= array[end])
                {
                    int temp = array[marker]; // swap
                    array[marker] = array[i];
                    array[i] = temp;
                    marker += 1;
                }
            }
            return marker - 1;
        }

        private void quicksort(int[] array, int start, int end)
        {
            if (start >= end)
            {
                return;
            }
            int pivot = partition(array, start, end);
            quicksort(array, start, pivot - 1);
            quicksort(array, pivot + 1, end);
        }

        private void Swap(ref int Element, ref int lElement)
        {
            int a = Element;
            Element = lElement;
            lElement = a;
        }

        private int[] IsertionSort(int[] array)
        {
            for (var i = 1; i < array.Length; i++)
            {
                var key = array[i];
                var j = i;
                while ((j > 1) && (array[j - 1] > key))
                {
                    Swap(ref array[j - 1], ref array[j]);
                    j--;
                }

                array[j] = key;
            }

            return array;
        }
        private void TimSortCall(int[] vector)
        {
            int n = vector.Length;
            TimSort(vector, n);
        }
        public const int run = 32;
        public void InsertionSort(int[] vector, int left, int right)
        {
            for (int i = left + 1; i <= right; i++)
            {
                int temp = Convert.ToInt32(vector[i]);
                int j = i - 1;
                while (j >= left && vector[j] > temp)
                {
                    vector[j + 1] = vector[j];
                    j--;
                }
                vector[j + 1] = temp;
            }
        }
        public static void Merge(int[] vector, int first, int second, int third)
        {
            int len1 = second - first + 1, len2 = third - second;
            int[] left = new int[len1];
            int[] right = new int[len2];
            for (int x = 0; x < len1; x++)
                left[x] = Convert.ToInt32(vector[first + x]);
            for (int x = 0; x < len2; x++)
                right[x] = Convert.ToInt32(vector[second + 1 + x]);
            int i = 0;
            int j = 0;
            int k = first;
            while (i < len1 && j < len2)
            {
                if (left[i] <= right[j])
                {
                    vector[k] = left[i];
                    i++;
                }
                else
                {
                    vector[k] = right[j];
                    j++;
                }
                k++;
            }
            while (i < len1)
            {
                vector[k] = left[i];
                k++;
                i++;
            }
            while (j < len2)
            {
                vector[k] = right[j];
                k++;
                j++;
            }
        }
        public void TimSort(int[] vector, int n)
        {
            for (int i = 0; i < n; i += run)
                InsertionSort(vector, i, Math.Min((i + run - 1), (n - 1)));
            for (int size = run; size < n;
                                     size = 2 * size)
            {
                for (int left = 0; left < n;
                                      left += 2 * size)
                {
                    int mid = left + size - 1;
                    int right = Math.Min((left +
                                        2 * size - 1), (n - 1));
                    if (mid < right)
                        Merge(vector, left, mid, right);
                }
            }
        }
        public void CountingSort(int[] array)
        {
            if (array.Length != 0)
            {
                int maxValue = int.MinValue;
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i] > maxValue)
                    {
                        maxValue = array[i];
                    }
                }
                int[] count = new int[maxValue + 1];
                for (int i = 0; i < array.Length; i++)
                {
                    count[array[i]]++;
                }
                int index = 0;
                for (int i = 0; i < count.Length; i++)
                {
                    for (int j = 0; j < count[i]; j++)
                    {
                        array[index] = i;
                        index++;
                    }
                }
            }
        }
        public void Heapsort(int[] arr)
        {
            int n = arr.Length;
            for (int i = n / 2 - 1; i >= 0; i--)
            {
                heapify(arr, n, i);
            }
            for (int i = n - 1; i >= 0; i--)
            {
                int temp = arr[0];
                arr[0] = arr[i];
                arr[i] = temp;
                heapify(arr, i, 0);
            }
            
        }
        void heapify(int[] arr, int n, int i)
        {
            int largest = i;
            int l = 2 * i + 1;
            int r = 2 * i + 2;
            if (l < n && arr[l] > arr[largest])
            {
                largest = l;
            }
            if (r < n && arr[r] > arr[largest])
            {
                largest = r;
            }
            if (largest != i)
            {
                int swap = arr[i];
                arr[i] = arr[largest];
                arr[largest] = swap;
                heapify(arr, n, largest);
            }
        }
    }
}
