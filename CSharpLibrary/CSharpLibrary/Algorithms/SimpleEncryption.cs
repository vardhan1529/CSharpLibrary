using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLibrary.Algorithms
{
    class SimpleEncryption
    {
        public static string Encrypt(string s)
        {
            var res = new byte[s.Length];
            int k = 2;
            for (var i = 0; i < s.Length; i++)
            {
                var c = s[i];
                var uc = (int)((byte)s[i]);
                if (uc >= 65 && uc <= 90)
                {
                    uc += k;
                    if (uc > 90)
                    {
                        res[i] = (byte)(64 + uc - 90);
                    }
                    else
                    {
                        res[i] = (byte)uc;
                    }
                }
                else if (uc >= 97 && uc <= 122)
                {
                    uc += k;
                    if (uc > 122)
                    {
                        res[i] = (byte)(96 + uc - 122);
                    }
                    else
                    {
                        res[i] = (byte)uc;
                    }
                }
                else
                {
                    res[i] = (byte)(uc);
                }
            }

            return Encoding.ASCII.GetString(res);
        }
    }
}
