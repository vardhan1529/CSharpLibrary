using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLibrary.Algorithms
{
    class CrossWord
    {
        class Word
        {
            public int Start { get; set; }

            public int End { get; set; }

            public bool Horizontal { get; set; }

            public int Line { get; set; }

            public int Length
            {
                get
                {
                    return End - Start;
                }
            }

            public bool Filled { get; set; }

            public int WordId { get; set; }
        }
        public static void Solve()
        {
            var l = File.ReadAllLines("D:\\puzzle.txt");
            var a = new char[10, 10];
            for (var i = 0; i < 10; i++)
            {
                for (var j = 0; j < 10; j++)
                {
                    a[i, j] = l[i][j];
                }
            }

            var es = new List<Word>();

            var words = l[10].Split(';').ToList();

            for (var i = 0; i < 10; i++)
            {
                var o = new Word() { Line = i, Horizontal = true };
                var f = false;
                for (var j = 0; j < 10; j++)
                {
                    if (a[i, j] == '-')
                    {
                        if (!f)
                        {
                            o.Start = j;
                            f = true;
                        }
                    }
                    else if(f)
                    {
                        if (j - o.Start > 1)
                        {
                            o.End = j;
                            es.Add(o);
                        }
                        f = false;
                        o = new Word() { Horizontal = true };
                    }
                }
            }

            for (var i = 0; i < 10; i++)
            {
                var o = new Word() { Line = i };
                var f = false;
                for (var j = 0; j < 10; j++)
                {
                    if (a[j, i] == '-')
                    {
                        if (!f)
                        {
                            o.Start = j;
                            f = true;
                        }
                    }
                    else if(f)
                    {
                        if (j - o.Start > 1)
                        {
                            o.End = j;
                            es.Add(o);
                        }
                        f = false;
                        o = new Word() { Line = i };
                    }
                }
            }
            words = words.OrderBy(m => m.Length).ToList();
            
        }

        void FindWords(List<string> words, List<Word> es, int current = 0)
        {
                var w = es.Where(m => m.Length == words[current].Length).ToList();
                w[0].Filled = true;
                w[0].WordId = current;

            
        }
    }
}
