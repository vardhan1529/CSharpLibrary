using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLibrary.ConceptSamples
{
    interface ILibrary
    {
        string GetPageContent(string bookName, int pageNo);
    }

    interface IBookStore
    {
        string GetPageContent(string bookName, int pageNo);
    }

    class ImplicitExplicitTest : ILibrary, IBookStore
    {
        public string GetPageContent(string bookName, int pageNo)
        {
            return null;
        }

        string ILibrary.GetPageContent(string bookName, int pageNo)
        {
            return "Libary book page content";
        }

        string IBookStore.GetPageContent(string bookName, int pageNo)
        {
            return "Page content from the book store";
        }
    }
}
