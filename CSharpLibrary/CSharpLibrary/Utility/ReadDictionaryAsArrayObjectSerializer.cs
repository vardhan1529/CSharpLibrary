﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLibrary.Utility
{
    public class ReadDictionaryAsArrayObjectSerializer : JsonConverter
    {
        public override bool CanRead
        {
            get { return true; }
        }
        public override bool CanWrite
        {
            get { return true; }
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var res = new Dictionary<string, string>();
            JArray s = JArray.Load(reader);
            foreach (var a in s)
            {
                var o = JObject.FromObject(a);
                foreach (var p in o.Properties())
                {
                    res[p.Name] = p.ToObject<string>();
                }
            }

            return res;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var dicValues = value as Dictionary<string, string>;
            var result = new StringBuilder();
            result.AppendLine("[");
            foreach (var val in dicValues)
            {
                result.AppendLine($"{{ {val.Key} : \"{val.Value}\" }},");
            }

            result.AppendLine("]");
            writer.WriteRaw(result.ToString());
        }
    }
}
