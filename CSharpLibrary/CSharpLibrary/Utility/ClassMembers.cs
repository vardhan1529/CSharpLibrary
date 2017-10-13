using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLibrary.Utility
{
    public static class ClassMembers
    {
        private static Dictionary<string, Dictionary<string, Type>> CachedProperties;

        public static Dictionary<string, Dictionary<string, Type>> GetAllClassProperties()
        {
            if (CachedProperties != null)
            {
                return CachedProperties;
            }
            var repoNamespace = System.Configuration.ConfigurationManager.AppSettings["RepoNamespace"].ToString();
            Dictionary<string, Dictionary<string, Type>> res = new Dictionary<string, Dictionary<string, Type>>();
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(m => m.Namespace.Equals(repoNamespace, StringComparison.OrdinalIgnoreCase)).ToList();
            foreach (var type in types)
            {
                Dictionary<string, Type> typeInfo = new Dictionary<string, Type>();
                foreach (var prop in type.GetProperties())
                {
                    typeInfo[prop.Name] = prop.PropertyType;
                }

                res[type.Name] = typeInfo;
            }

            CachedProperties = res;

            return res;
        }

        public static Dictionary<string, Type> GetClassProperties(string cName)
        {
            return GetAllClassProperties()[cName];
        }

        public static void DynamicObjectCreation()
        {
            Assembly a = Assembly.LoadFile(@"D:\My Projects\CsharpLib\CSharpLibrary\CSharpLibrary\bin\Debug\Newtonsoft.Json.dll");
            a.GetTypes().ToList().ForEach(m => { Console.WriteLine(m.FullName); });
            // Get the type to use.
            Type t = a.GetType("Newtonsoft.Json.JsonConvert");
            // Get the method to call.
            MethodInfo me = t.GetMethod("SerializeObject", new Type[] { typeof(string) });
            // Create an instance.
            //object obj = Activator.CreateInstance(t);
            var oa = new[] { new { Name = "test" } };
            //// Execute the method.
            //var xs = me.Invoke(obj, oa);

            //For static type method, there is no need of instance creation
            var xss = me.Invoke(null, oa);

        }
    }
}
