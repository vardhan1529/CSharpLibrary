using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CSharpLibrary.ConceptSamples.ProjectLifeCycle
{
    [Flags]
    public enum ProjectStatus
    {
        AcquireFailed = 1,
        DevelopmentStage = 2,
        NotStarted = 4,
        MaintainenceStage = 8,
        InBidding = 16,
        Completed = 32,
        RequirementAnalysis = 64
    }
    public partial class Project
    {
        public static List<Project> GetAllProjects()
        {
            var data = new XmlDocument();
            data.Load(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\AppData\\ProjectLifeCycle\\Projects.xml");
            var ps = data.SelectNodes("descendant::Project");
            var r = new List<Project>();
            foreach(XmlNode pc in ps)
            {
                r.Add(new Project()
                {
                    Id = Convert.ToInt32(pc.Attributes["id"].Value),
                    Name = pc.Attributes["name"].Value,
                    ProjectStatus = (ProjectStatus)Convert.ToInt32(pc.Attributes["status"].Value),
                    CategoryId = Convert.ToInt32(pc.Attributes["categoryId"].Value),
                });
            }

            return r;
        }

        public static List<Project> GetProject(Func<Project, bool> pr)
        {
            var res = new List<Project>();
            foreach(var project in GetAllProjects())
            {
                if(pr(project))
                {
                    res.Add(project);
                }
            }

            return res;
        }
    }
}
