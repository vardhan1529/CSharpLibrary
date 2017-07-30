using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLibrary.Utility
{
    public class Formatters
    {
        /// <summary>
        /// Formatter for converting list of objects containing key and a value in object to dictionary of key value pairs
        /// </summary>
        public class KeyValuePairConverter<T> : JsonConverter
        {
            public override bool CanRead {
                get { return true; }
            }
            public override bool CanWrite {
                get { return false; }
            }

            public override bool CanConvert(Type objectType)
            {
                throw new NotImplementedException("Unnecessary because CanWrite is false. The type will skip the converter.");
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                var res = new Dictionary<string, T>();
                JArray s = JArray.Load(reader);
                foreach(var a in s)
                {
                    var o = JObject.FromObject(a);
                    foreach(var p in o.Properties())
                    {
                        res[p.Name] = p.ToObject<T>();
                    }
                }

                return res;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {

            }
        }
    }
}
