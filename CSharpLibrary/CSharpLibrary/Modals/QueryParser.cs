using CSharpLibrary.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLibrary.Modals
{
    public class Student
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public int Age { get; set; }
    }

    public static class QueryParser
    {
        private class Query
        {
            public string Filter { get; set; }

            public List<string> Fields { get; set; }

            public string Source { get; set; }
        }

        public static object SimpleParse(string query)
        {
            var q = JsonConvert.DeserializeObject<Query>(query);
            Dictionary<string, int> d = new Dictionary<string, int>() { { "vv", 1 }, { "ff", 2 } };
            var y = JsonConvert.SerializeObject(d);
            var x = ValidateFieldsAndSource(q);
            return null;
        }

        private static bool ValidateFieldsAndSource(Query q)
        {
            var s = ClassMembers.GetClassProperties(q.Source);
            return true;
        }

        
    }
}
