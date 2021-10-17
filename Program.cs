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
            Algoritm[,] PowAlgArray = new Algoritm[4, 2000];
            for(int i = 0; i < 4; i++)
            {
                for(int j = 0; j < 2000; j++)
                {
                    if(i == 0)
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
            try
            {
                using (StreamWriter SW = new StreamWriter("../../../Exponent1.csv", false, Encoding.Default))
                {
                    for (int i = 0; i < 2000; i++) 
                    {
                        SW.WriteLine($"{i}|{PowAlgArray[0, i].Result}|{PowAlgArray[1, i].Result}|{PowAlgArray[2, i].Result}|{PowAlgArray[3, i].Result}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }

    abstract class Algoritm
    {
        public abstract long Result { get; set; }
        public abstract int PerformingAlgoritm(int[] @param);
        public long ElapsedTime(int[] @param)
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
            if(n == 0)
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
}
