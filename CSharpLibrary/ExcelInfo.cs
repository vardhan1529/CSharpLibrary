using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelPOC
{
    public class ExcelInfo
    {
        public static string[] Columns { get; private set; } = null;
        public static string GetColumnAt(int index)
        {
            return Columns[index];
        }
        static ExcelInfo()
        {
            //Not so generic code to generate. Limited the code to 702 Excel Columns
            if (Columns == null)
            {
                Columns = new string[702];
                for (var i = 0; i < 702; i++)
                {
                    Columns[i] = (i / 26 == 0 ? string.Empty : ((char)(i / 26 + 64)).ToString()) + (char)(i % 26 + 65);
                }
            }
        }

        private ExcelInfo()
        {
        }
    }
}
