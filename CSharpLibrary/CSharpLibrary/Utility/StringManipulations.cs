using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLibrary.Utility
{
    static class StringManipulations
    {
        public static void  Iterate(this string data)
        {
            foreach(var d in data)
            {
                Console.Write(d);
            }

            Console.WriteLine();

            for(var i=0;i<data.Length; i++)
            {
                Console.Write(data[i]);
            }

            Console.WriteLine();

            var de = data.GetEnumerator();
            while(de.MoveNext())
            {
                Console.Write(de.Current);
            }

            Console.WriteLine();

            var da = data.ToArray();
            foreach(var d in da)
            {
                Console.Write(d);
            }
        }
    }
}
