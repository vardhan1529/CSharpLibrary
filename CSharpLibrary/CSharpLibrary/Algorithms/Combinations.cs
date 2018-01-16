using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLibrary.Algorithms
{
    class Combinations
    {
        /// <summary>
        /// Returns the list of different input string combinations
        /// A string of length n has 2^n combinations
        /// So integers from 1 to 2^n are base 2 encoded and left padded to length of input string with 0's
        /// </summary>
        /// <param name="input"> The input string </param>
        /// <returns> The list of different string combinations </returns>
        static List<string> Get(string input)
        {
            var len = input.Length;
            var res = new List<string>();
            double count = Math.Pow(2, len);
            for (int i = 1; i <= count - 1; i++)
            {
                string str = Convert.ToString(i, 2).PadLeft(len, '0');
                var temp = string.Empty;
                for (int j = 0; j < str.Length; j++)
                {
                    if (str[j] == '1')
                    {
                        temp += input[j];
                    }
                }

                res.Add(temp);
            }

            return res;
        }

        static List<string> GetCombinations(string input, int nelem, int start)
        {
            while(true)
            {
               
            }
        }
    }
}
