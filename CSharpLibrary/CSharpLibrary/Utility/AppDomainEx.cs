using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLibrary.Utility
{
    [Serializable]
    public class E
    {
        public string Name { get; set; }

        public void PrintMessage()
        {
            Console.WriteLine(AppDomain.CurrentDomain.FriendlyName);
        }
    }

    public class Test : MarshalByRefObject
    {
        public string Name { get; set; }

        public void PrintMessage()
        {
            Console.WriteLine(AppDomain.CurrentDomain.FriendlyName);
        }

        public void ModifyObject(E t)
        {
            t.Name += " Modified";
            Console.WriteLine(AppDomain.CurrentDomain.FriendlyName);
        }

        public void ModifySameObject()
        {
            this.Name += " Modified";
            Console.WriteLine(Name);
        }
    }
    class AppDomainSample
    {
        public void CreateTwoDomainsLoadUnload()
        {

            var x = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var d1 = AppDomainEx.CreateAppDomainAndLoadAssembly("App1", x + "\\CSharpLibrary.exe");
            //var t = d1.GetAssemblies().Where(m => m.FullName == "CSharpLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null").FirstOrDefault();
            //Type myType = t.GetType("CSharpLibrary.Utility.Test");
            ////// Get the method to call.
            //MethodInfo myMethod = myType.GetMethod("PrintMessage");
            ////// Create an instance.
            //object obj = Activator.CreateInstance(myType);
            //var oa = new Test[] { new Test{ Name = "test" } };
            ////// Execute the method.
            //myMethod.Invoke(obj, null);
            //var d1 = AppDomainEx.CreateApplicationDomain("App1");
            dynamic d1i = d1.CreateInstanceAndUnwrap("CSharpLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "CSharpLibrary.Utility.Test");
            var T = new E();
            T.Name = "VARdhan";
            d1i.PrintMessage();
            var d2 = AppDomainEx.CreateAppDomainAndLoadAssembly("App1", x + "\\CSharpLibrary.exe");
            dynamic d2i = d1.CreateInstanceAndUnwrap("CSharpLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "CSharpLibrary.Utility.E");
            d2i.PrintMessage();
            //    Worker remoteWorker = (Worker)ad.CreateInstanceAndUnwrap(
            //typeof(Worker).Assembly.FullName,
            //"Worker");
            //remoteWorker.PrintDomain();
        }

        public void TestWithDifferentAssembly()
        {
            var d1 = AppDomainEx.CreateAppDomainAndLoadAssembly("App1", @"D:\My Projects\CsharpLib\CSharpLibrary\AppDomainTest\bin\Debug\AppDomainTest.exe");
            dynamic d1i = d1.CreateInstanceAndUnwrap("AppDomainTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "AppDomainTest.FileHandlerSerialization");
            d1i.PrintStatus();
            var d2 = AppDomainEx.CreateAppDomainAndLoadAssembly("App1", @"D:\My Projects\CsharpLib\CSharpLibrary\AppDomainTest\bin\Debug\AppDomainTest.exe");
            dynamic d2i = d1.CreateInstanceAndUnwrap("AppDomainTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "AppDomainTest.FileHandlerMarshalByRefObject");
            d2i.PrintStatus();
        }
    }
    class AppDomainEx
    {
        public static AppDomain CreateApplicationDomain(string domainName)
        {
            AppDomainSetup domaininfo = new AppDomainSetup();
            domaininfo.ApplicationBase = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Evidence adevidence = AppDomain.CurrentDomain.Evidence;
            return AppDomain.CreateDomain(domainName, adevidence, domaininfo);
        }

        public static void UnloadApplicationDomain(AppDomain domain)
        {
            AppDomain.Unload(domain);
        }

        public static void LoadAssembly(string path, AppDomain domain)
        {
            domain.Load(Assembly.LoadFile(path).FullName);
        }

        public static AppDomain CreateAppDomainAndLoadAssembly(string domainName, string path)
        {
            var domain = CreateApplicationDomain(domainName);
            LoadAssembly(path, domain);
            return domain;
        }
    }
}
