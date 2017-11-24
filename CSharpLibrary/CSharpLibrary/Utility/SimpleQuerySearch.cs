using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLibrary.Utility
{
    class SimpleQuerySearch
    {
        /// <summary>
        /// Searches the strings q in the list l and prints the count
        /// </summary>
        /// <param name="l">Input strings</param>
        /// <param name="q">Query strings</param>
        public static void Search(List<string> l, List<string> q)
        {
            var ln = Convert.ToInt32(Console.ReadLine());
            for (var i = 0; i < ln; i++)
            {
                l.Add(Console.ReadLine());
            }
            var qn = Convert.ToInt32(Console.ReadLine());
            for (var i = 0; i < ln; i++)
            {
                q.Add(Console.ReadLine());
            }
            var d = new Dictionary<string, int>();
            int lc = l.Count;
            foreach (var qe in q)
            {
                if (d.ContainsKey(qe))
                {
                    Console.WriteLine(d[qe]);
                    continue;
                }
                var c = 0;
                for (var i = 0; i < lc; i++)
                {
                    if (qe == l[i])
                    {
                        c++;
                        l.RemoveAt(i);
                        lc--;
                        i--;
                    }
                }

                Console.WriteLine(c);
                d[qe] = c;
            }
        }
    }
}
