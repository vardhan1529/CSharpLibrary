using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLibrary.ConceptSamples
{
    /// <summary>
    /// Method overloading. A method with specific type as input will be executed
    /// </summary>
    public class Overloading
    {
        public string GetData(string s)
        {
            return s.Trim();
        }

        public string GetData(object s)
        {
            return s.ToString().Trim();
        }

        public string GetData(int s)
        {
            return s.ToString().Trim();
        }
    }
}
