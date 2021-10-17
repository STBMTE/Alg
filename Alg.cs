using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    class alg
    {
        public int[] VectorGeneration(int n)
        {
            Random rnd = new Random();
            int[] nArr = new int[n];
            for (int i = 0; i < n; i++)
            {
                nArr[i] = Math.Abs(rnd.Next());
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
            for(int i = 0; i < n.Length; i++)
            {
                for(int j = 0; j < n.Length - i - 1; j++)
                {
                    if(n[j] > n[j + 1])
                    {
                        int c = n[j];
                        n[j] = n[j + 1];
                        n[j + 1] = c;
                    }
                }
            }
        }
        public int partition(int[] array, int start, int end)
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

        public void quicksort(int[] array, int start, int end)
        {
            if (start >= end)
            {
                return;
            }
            int pivot = partition(array, start, end);
            quicksort(array, start, pivot - 1);
            quicksort(array, pivot + 1, end);
        }
    }
}
