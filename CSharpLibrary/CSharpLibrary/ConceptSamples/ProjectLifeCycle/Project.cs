using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLibrary.ConceptSamples.ProjectLifeCycle
{ 
    public partial class Project
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public ProjectStatus ProjectStatus { get; set; }
        public int CategoryId { get; set; }
    }
}
