using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AppDomainTest
{
    [Serializable]
    public class FileHandlerSerialization
    {

        public void PrintStatus()
        {
            Console.WriteLine(string.Format("App domain friendly name {0}", AppDomain.CurrentDomain.FriendlyName));
            if (File.Exists(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) +  "\\test.json"))
            {
                Console.WriteLine("File found");
            }
            else
            {
                Console.WriteLine("File doesn't exist");
            }
        }
    }

    public class FileHandlerMarshalByRefObject : MarshalByRefObject
    {
        public void PrintStatus()
        {
            Console.WriteLine(string.Format("App domain friendly name {0}", AppDomain.CurrentDomain.FriendlyName));
            if (File.Exists(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\test.json"))
            {
                Console.WriteLine("File found");
            }
            else
            {
                Console.WriteLine("File doesn't exist");
            }
        }
    }
}
