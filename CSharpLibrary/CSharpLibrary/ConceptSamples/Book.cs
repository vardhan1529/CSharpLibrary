using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLibrary.ConceptSamples
{
    public class Book : IBook
    {
        public string BookName { get; set; }

        public string Author { get; set; }

        public float Price { get; set; }

        public string Publisher { get; set; }

        public string GetPageContent(int pageNo)
        {
            return "Implicit Implemenation";
        }

        string IBook.GetPageContent(int pageNo)
        {
            return "Explict Implementation";
        }
    }
}
