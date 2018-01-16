using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLibrary.Algorithms
{
    class Longest2CharacterString
    {
        public string Algorithm(string str)
        {
            var u = string.Empty;

            foreach (var c in str)
            {
                if (!u.Contains(c))
                {
                    u += c;
                }
            }

            var output = string.Empty;
            var uc = u.Length;
            var strlen = str.Length;
            var com = uc - 2;
            if (uc >= 2)
            {
                if (uc == 2)
                {
                    if (Validate(str) && str.Length > output.Length)
                    {
                        output = str;
                    }
                }
                else
                {
                    for (var i = 0; i < uc; i++)
                    {
                        for (var j = i + 1; j < uc; j++)
                        {
                            var st = u.Remove(i, 1);
                            st = st.Remove(j - 1, 1);

                            var cstr = string.Empty;
                            foreach (var s in str)
                            {
                                if (!st.Contains(s))
                                {
                                    cstr += s;
                                }
                            }
                            if (Validate(cstr) && cstr.Length > output.Length)
                            {
                                output = cstr;
                            }
                        }
                    }
                }
            }

            return output;
        }

        public static bool Validate(string str)
        {
            var t1 = str[0];
            var t2 = str[1];
            if (t1 == t2)
            {
                return false;
            }
            var l = str.Length;
            for (var i = 2; i < l; i = i + 2)
            {
                if (i == l - 1)
                {
                    if (t1 != str[i])
                    {
                        return false;
                    }
                }
                else if (t1 != str[i] || t2 != str[i + 1])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
