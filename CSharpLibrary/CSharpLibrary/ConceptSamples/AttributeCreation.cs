using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLibrary.ConceptSamples
{
    //Attributes are used to provide additional metadata required for the classes, methods and properties. This data can be used at the runtime.
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
    internal class AttributeCreationAttribute: Attribute
    {
        public string Name { get; set; }

        public AttributeCreationAttribute(string name)
        {
            Name = name;
        }
    }

    [AttributeCreation("vardhan")]
    internal class UseAttribute
    {
    }
}
