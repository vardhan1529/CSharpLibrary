using CSharpLibrary.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLibrary.Modals
{
    public class TestKeyValuePairFormatterModel
    {

        [JsonConverter(typeof(Formatters.KeyValuePairConverter<double>))]
        public Dictionary<string, double> Values { get; set; }

        public string Key { get; set; }
    }
}
