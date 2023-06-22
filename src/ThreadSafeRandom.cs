using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;



namespace BizhawkRandomizeInputs
{
    public static class ThreadSafeRandom
    {
        [ThreadStatic] private static Random? Local = null;
        public static Random ThisThreadsRandom
        {
            get { return Local ?? (Local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
        }



        private static void Swap<T>(ref List<T> list, int i1, int i2)
        {
            T tmp = list[i1];
            list[i1] = list[i2];
            list[i2] = tmp;
        }



        /// <summary>
        /// Ensures j < n, as long as n > 0
        /// </summary>
        /// <param name="n">a number > 0</param>
        /// <param name="j">a number such that 0 <= j <= n</param>
        /// <returns>j or j-1 if j == n</returns>
        private static int EnsureUnique(int n, int j)
        {
            if (j == n)
                return j - 1;

            return j >= 0 ? j : 0;
        }



        public static void RandomizeList<T>(ref List<T> bag, bool ensureUnique)
        {
            for (int i = bag.Count - 1; i > 0; i--)
            {
                int j = ThisThreadsRandom.Next(0, i + 1);
                j = ensureUnique ? EnsureUnique(i, j) : j;
                Swap(ref bag, i, j);
            }
        }



        public static List<T> RandomizeList<T>(IReadOnlyList<T> bag, bool ensureUnique)
        {
            List<T> newBag = bag.ToList();
            for (int i = newBag.Count - 1; i > 0; i--)
            {
                int j = ThisThreadsRandom.Next(0, i + 1);
                j = ensureUnique ? EnsureUnique(i, j) : j;
                Swap(ref newBag, i, j);
            }

            return newBag;
        }
    }
}
