using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace Algorithms
{
    class ExponentiationAlgorithm
    {
        public ExponentiationAlgorithm(int x)
        {
            List<(int, long, long, long, long)> list = new List<(int, long, long, long, long)>();
            for (int i = 0; i < x + 1; i++)
            {
                list.Add(ExponentiationTest(i));
            }
            Writer(list);
        }

        private void Writer(List<(int, long, long, long, long)> l)
        {
            try
            {
                using (StreamWriter SW = new StreamWriter("../../../Exponent.csv", false, Encoding.Default))
                {
                    for (int i = 1; i < l.Count; i++)
                    {
                        SW.WriteLine($"{l[i].Item1};{l[i].Item2};{l[i].Item3};{l[i].Item4};{l[i].Item5}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private (int, long, long, long, long) ExponentiationTest(int x)
        {
            var sw = new Stopwatch();
            (int, long, long, long, long) Return;
            Return.Item1 = x;

            sw.Start();

            Pow(x, x);

            sw.Stop();

            Return.Item2 = sw.ElapsedTicks;

            sw.Restart();

            RecPow(x, x);

            sw.Stop();

            Return.Item3 = sw.ElapsedTicks;


            sw.Restart();

            QuickPow(x, x);
            sw.Stop();

            Return.Item4 = sw.ElapsedTicks;

            sw.Restart();

            ClassicQuickPow(x, x);

            sw.Stop();

            Return.Item5 = sw.ElapsedTicks;
            return Return;
        }

        private int Pow(int x, int n)
        {
            int result = 1;
            for (float i = 0; i < n; i++)
            {
                result *= x;
            }
            return result;
        }

        private int RecPow(int x, int n)
        {
            int f;
            if (n == 0)
            {
                f = 1;
            }
            else
            {
                f = RecPow(x, n / 2);
                if (n % 2 == 1)
                {
                    f = f * f * x;
                }
                else
                {
                    f = f * f;
                }
            }
            return f;
        }

        private int QuickPow(int x, int n)
        {
            int c = x;
            int k = n;
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

        private int ClassicQuickPow(int x, int n)
        {
            int c = x;
            int f = 1;
            int k = n;
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
