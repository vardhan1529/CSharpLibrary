using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLibrary.ConceptSamples
{
    /*
    Any of the following types may be a pointer type:
    sbyte, byte, short, ushort, int, uint, long, ulong, char, float, double, decimal, or bool.
    Any enum type.
    Any pointer type.
    Any user-defined struct type that contains fields of unmanaged types only.
    */
    class UnSafeContext
    {
        //An unsafe code. Will be a successful build only if the compiler is configured to compile unsafe code
        //void InlineUnsafe()
        //{
        //    unsafe
        //    {
        //        char p = 'V';
        //        char* pp = &p;
        //    }
        //}

        //unsafe void MethodLevelUnsafe()
        //{
        //    int i = 90;
        //    int* ip = &i;
        //}
    }
}
