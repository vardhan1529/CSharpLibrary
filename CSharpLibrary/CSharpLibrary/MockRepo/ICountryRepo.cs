using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLibrary.MockRepo
{
    public interface ICountryRepo
    {
        List<string> GetCountryNames();
    }
}
