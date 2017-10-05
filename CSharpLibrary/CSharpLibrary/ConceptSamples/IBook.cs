using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLibrary.ConceptSamples
{
    interface IBook
    {
        string BookName { get; set; }

        string Author { get; set; }

        float Price { get; set; }

        string Publisher { get; set; }

        string GetPageContent(int pageNo);
    }
}
