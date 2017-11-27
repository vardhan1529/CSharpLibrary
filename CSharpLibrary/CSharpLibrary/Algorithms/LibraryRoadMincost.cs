using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLibrary.Algorithms
{
    class LibraryRoadMincost
    {
        class RouteInfo
        {
            public int a { get; set; }
            public int b { get; set; }
        }

        public static void Algorithm()
        {
            var res = new List<long>();
            var inputs = File.ReadLines("D:\\i.txt").ToList();
            //int q = Convert.ToInt32(Console.ReadLine());
            for (int a0 = 1; a0 < inputs.Count; a0++)
            {
                string[] tokens_n = inputs[a0].Split(' ');
                var ro = new List<RouteInfo>();
                int cc = Convert.ToInt32(tokens_n[0]);
                int rc = Convert.ToInt32(tokens_n[1]);
                long lib = Convert.ToInt64(tokens_n[2]);
                long road = Convert.ToInt64(tokens_n[3]);
                for (int a1 = 0; a1 < rc; a1++)
                {
                    string[] tokens_city_1 = inputs[a0+a1+1].Split(' ');
                    int city_1 = Convert.ToInt32(tokens_city_1[0]);
                    int city_2 = Convert.ToInt32(tokens_city_1[1]);
                    ro.Add(new RouteInfo() { a = city_1, b = city_2 });
                }
                var cities = new List<int>();
                for (var i = 1; i <= cc; i++)
                {
                    cities.Add(i);
                }

                var ccount = new List<int>();
                while (true)
                {
                    var connectedCities = ConnectCities(ro, cities[0]);
                    //var count = connectedCities.Count;
                    //if(count == 1)
                    //{

                    //}
                    cities.RemoveAll(m => connectedCities.Contains(m));
                    ccount.Add(connectedCities.Count());
                    if (!cities.Any())
                    {
                        break;
                    }
                }

                long amount = 0;
                foreach (var r in ccount)
                {
                    var libAmount = r * lib;
                    var roadAmount = (r - 1) * road + lib;
                    if (roadAmount > libAmount)
                    {
                        amount += libAmount;
                    }
                    else
                    {
                        amount += roadAmount;
                    }
                }

                res.Add(amount);
                a0 += rc;
            }

            foreach (var l in res)
            {
                Console.WriteLine(l);
            }


        }

        static List<int> ConnectCities(List<RouteInfo> ro, int currentCity)
        {
            var connectedRoutes = new List<int>() { currentCity };
            var status = true;
            var pcr = new List<int>() { currentCity };

            while (status)
            {
                var cra = new List<int>();

                var cr = new List<RouteInfo>();

                foreach (var r in ro)
                {
                    foreach (var pc in pcr)
                    {
                        if (r.a == pc || r.b == pc)
                        {
                            cr.Add(r);
                            break;
                        }
                    }
                }
                ro.RemoveAll(m => pcr.Contains(m.a) || pcr.Contains(m.b));
                if (!cr.Any())
                {
                    break;
                }

                foreach (var r in cr)
                {
                    cra.Add(r.a);
                    cra.Add(r.b);
                }

                pcr = cra.Distinct().ToList();
                pcr.ForEach(m => { if (!connectedRoutes.Contains(m)) { connectedRoutes.Add(m); } });
            }

            return connectedRoutes;
        }
    }
}
