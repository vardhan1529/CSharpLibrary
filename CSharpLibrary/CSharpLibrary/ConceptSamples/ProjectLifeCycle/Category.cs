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
    class Category
    {
        public string Name { get; set; }
        public int Id { get; set; }
        private List<WorkFlowItem> WorkFlowItems { get; set; }
        public WorkFlowItem AddWorkFlowItem(WorkFlowItem item)
        {
            WorkFlowItems.Add(item);
            return item;
        }
        public List<WorkFlowItem> GetWorkFlowItems()
        {
            return WorkFlowItems;
        }
        public bool DeleteWorkFlowItem(int id)
        {
            var index = WorkFlowItems.FindIndex(m => m.Id == id);
            WorkFlowItems.RemoveAt(index);
            return true;
        }
        public static Category GetCategory(int id)
        {
            var data = new XmlDocument();
            data.Load(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\AppData\\ProjectLifeCycle\\Categories.xml");
            var category = data.SelectSingleNode(string.Format("descendant::Category[attribute::id =\"{0}\"]", id));
            var c = new Category() { WorkFlowItems = new List<WorkFlowItem>() };
            if (category != null)
            {
                var workFlowItems = new XmlDocument();
                workFlowItems.Load(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\AppData\\ProjectLifeCycle\\WorkFlowItems.xml");
                c.Id = id;
                c.Name = category.Attributes["name"].Value;
                var items = workFlowItems.SelectNodes(string.Format("descendant::WorkFlowItem[attribute::categoryId=\"{0}\"]", id));
                foreach(XmlNode item in items)
                {
                    c.AddWorkFlowItem(new WorkFlowItem()
                    {
                        Id = Convert.ToInt32(item.Attributes["id"].Value),
                        Name = item.Attributes["name"].Value,
                        Order = Convert.ToInt32(item.Attributes["order"].Value)
                    });
                }
            }
            return c;
        }

        public static List<Category> GetCategories()
        {
            var data = new XmlDocument();
            data.Load(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\AppData\\ProjectLifeCycle\\Categories.xml");
            var categories = data.SelectNodes("descendant::Category");
            var workFlowItems = new XmlDocument();
            workFlowItems.Load(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\AppData\\ProjectLifeCycle\\WorkFlowItems.xml");
            var cData = new List<Category>();
            if (categories != null)
            {
                foreach (XmlNode category in categories)
                {
                    var c = new Category() { WorkFlowItems = new List<WorkFlowItem>() };
                    c.Id = Convert.ToInt32(category.Attributes["id"].Value);
                    c.Name = category.Attributes["name"].Value;
                    var items = workFlowItems.SelectNodes(string.Format("descendant::WorkFlowItem[attribute::categoryId=\"{0}\"]", c.Id));
                    foreach (XmlNode item in items)
                    {
                        c.AddWorkFlowItem(new WorkFlowItem()
                        {
                            Id = Convert.ToInt32(item.Attributes["id"].Value),
                            Name = item.Attributes["name"].Value,
                            Order = Convert.ToInt32(item.Attributes["order"].Value)
                        });
                    }
                    cData.Add(c);
                }
            }

            return cData;
        }
    }
}
