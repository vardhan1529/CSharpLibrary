using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CSharpLibrary.Utility
{
    static class RegularExpressions
    {
        public static void Validate()
        {
            //.s. b. c
            var r1 = new Regex(@"^([.\s]*[^. ]+[. ]+[^. ]+[. ]+[^. ]+)(?![. ]+[^. ]+)");
            var r2 = new Regex(@"(sasdf\.b\.c)(?!\.)");
            var list = new List<string>() { "..... ..", ".s. b.c.", ".sasd.b.c ..", "sasdf.b.c d ", "s cc c.d", " s.c   d", "adv sfd .  sfddsaf" };
            foreach (var d in list)
            {
                Console.WriteLine(string.Format("{0} - {1}", d, r1.IsMatch(d)));
            }
        }

        public static bool ValidateEmail(this string emailAddress)
        {
            //test.t@gmail.com
            return Regex.IsMatch(emailAddress, "");
        }

        public static bool ValidateJsonSimple(this string data)
        {
            while (true)
            {
                Console.Write("Enter text: ");
                data = Console.ReadLine().ToString();
                if (string.IsNullOrWhiteSpace(data))
                    break;
                Console.WriteLine(Regex.IsMatch(data, @"^({)?(\[)? (?(1)}|\])$"));
            }

            return false;
        }
    }
}
