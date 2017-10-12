using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CSharpLibrary.ConceptSamples
{
    class Singleton
    {
        private static Singleton instance;
        private static object lockCreation = new object();
        private Singleton() { }
        public static Singleton Instance
        {
            get
            {
                if(instance == null)
                {         
                    lock(lockCreation)
                    {
                        if (instance == null)
                        {
                            instance = new Singleton();
                        }
                    }                   
                }

                return instance;
            }
        }
    }
}
