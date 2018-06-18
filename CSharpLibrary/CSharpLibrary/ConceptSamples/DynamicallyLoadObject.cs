using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLibrary.ConceptSamples
{
    public class DynamicallyLoadObject
    {
        public class State
        {
            public string Name { get; set; }
            public int Population { get; set; }
            public int? FormationYear { get; set; }
        }

        /// <summary>
        /// Simple Dynamic CSV Parser
        /// </summary>
        public static void Load()
        {
            var text = File.ReadAllLines("D:\\States.csv");
            var columnNames = text[0].Split(',');
            var dic = new Dictionary<string, int>();
            var c = 0;
            foreach(var column in columnNames)
            {
                dic[column] = c++;
            }         
            List<State> states = new List<State>();
            string[] temp = new string[c];
            for(var i=1; i< text.Length; i++)
            {
                temp = text[i].Split(',');
                var s = new State();
                var fields = s.GetType().GetProperties();
                foreach (var field in fields)
                {                 
                    if (dic.Keys.Contains(field.Name))
                    {
                        var value = temp[dic[field.Name]];
                        if (!string.IsNullOrWhiteSpace(value))
                        {
                            field.SetValue(s, Convert.ChangeType(temp[dic[field.Name]], field.PropertyType), null);
                        }
                    }
                }

                states.Add(s);
            }
            
        }


    }
}
