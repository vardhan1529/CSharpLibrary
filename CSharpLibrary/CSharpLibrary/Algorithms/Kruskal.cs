using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLibrary.Algorithms
{
    class Kruskal
    {
        public struct NPath : IComparer<NPath>, IComparable<NPath>
        {
            public int NodeA { get; set; }
            public int NodeB { get; set; }
            public double Weight { get; set; }

            public int Compare(NPath x, NPath y)
            {
                if (x.Weight > y.Weight)
                {
                    return 1;
                }
                if (x.Weight == y.Weight)
                {
                    return 0;
                }

                return -1;
            }

            public int CompareTo(NPath y)
            {
                if (y.Weight > this.Weight)
                {
                    return -1;
                }
                if (y.Weight == this.Weight)
                {
                    return 0;
                }

                return 1;
            }

            public override string ToString()
            {
                return string.Format("{0} {1} {2}", NodeA, NodeB, Weight);
            }
        }

        /// <summary>
        /// Returns the weight of the least path
        /// </summary>
        public static void Algorithm()
        {
            var paths = new List<NPath>();
            var l = File.ReadLines(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "AppData\\Kruskal\\simple_input.txt")).ToList();
            for (var i = 0; i < l.Count; i++)
            {
                var t = l[i].Split(' ');
                paths.Add(new NPath()
                {
                    NodeA = Convert.ToInt16(t[0].Trim()),
                    NodeB = Convert.ToInt16(t[1].Trim()),
                    Weight = Convert.ToInt32(t[2].Trim())
                });
            }

            var fp = new List<NPath>();
            paths.Sort();
            paths = paths.GroupBy(m => new { a = m.NodeA, b = m.NodeB }).Select(z => z.ToList()[0]).ToList();

            fp.Add(paths[0]);
            for (var i = 1; i < paths.Count; i++)
            {
                var curr = paths[i];
                var completed = false;
                if (OpenLoopCheck(fp, curr, curr, curr.NodeA, ref completed))
                {
                    fp.Add(curr);
                }
            }
            Console.WriteLine(fp.Select(m => m.Weight).ToList().Sum());
        }

        public static bool OpenLoopCheck(List<NPath> fp, NPath curr, NPath rel, int lastNode, ref bool completed)
        {
            var status = true;
            List<NPath> associatedPaths;
            if (lastNode == rel.NodeB)
            {
                lastNode = rel.NodeA;
                associatedPaths = fp.Where(m => (m.NodeA == rel.NodeA && m.NodeB != rel.NodeB) || (m.NodeB == rel.NodeA && m.NodeA != rel.NodeB)).ToList();
            }
            else
            {
                lastNode = rel.NodeB;
                associatedPaths = fp.Where(m => (m.NodeA == rel.NodeB && m.NodeB != rel.NodeA) || (rel.NodeB == m.NodeB && rel.NodeA != m.NodeA)).ToList();
            }

            foreach (var node in associatedPaths)
            {
                if (!completed)
                {
                    if (node.NodeA == curr.NodeA || node.NodeB == curr.NodeA)
                    {
                        completed = true;
                        return false;
                    }
                    status = OpenLoopCheck(fp, curr, node, lastNode, ref completed);
                }
            }

            return status;
        }
    }
}
