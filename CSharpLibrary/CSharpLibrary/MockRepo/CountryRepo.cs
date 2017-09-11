using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLibrary.MockRepo
{
    public class CountryRepo : ICountryRepo
    {
        public List<string> GetCountryNames()
        {
            return new List<string>() { "India", "Srilanka" };
        }
    }
}
