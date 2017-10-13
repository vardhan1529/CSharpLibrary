using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLibrary.ConceptSamples
{
    class InstanceDelegateCall
    {
        internal class Main
        {
            public void Test()
            {
                var x = new Invoker();
                x.Notify();
            }
        }

        class Invoker
        {
            public string Context = "Default";

            public void UpdateCallback(bool completed)
            {
                var x = completed;
            }

            public void Notify()
            {
                var r = new Receiver();
                r.Update(UpdateCallback);
            }
        }

        class Receiver
        {
            public void Update(Action<bool> callback)
            {
                callback(true);
            }
        }
    }
}
