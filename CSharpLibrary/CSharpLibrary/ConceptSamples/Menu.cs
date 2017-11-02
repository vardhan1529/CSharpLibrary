using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLibrary.ConceptSamples
{
    class Menu
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public int? PId { get; set; }

        public List<Menu> ChildMenu { get; set; }

        //For testing use this data
        /*Menu.GroupMenu(new List<Menu>() { new Menu() { Id = 1, Name = "P1" }, new Menu() { Id = 2, PId = 1, Name = "C1P1"  }
            , new Menu() { Id = 3, PId = 1, Name = "C2P1"  },  new Menu() { Id = 4,  Name = "P2"  },new Menu() { Id = 5, PId = 4, Name = "C1P2"  }
    });*/
        public static List<Menu> GroupMenu(List<Menu> items)
        {
            var res = new List<Menu>();

            foreach(var item in items.Where(m => !m.PId.HasValue))
            {
                var i = new Menu() { Id = item.Id, Name = item.Name, PId = item.PId };
                AddChildItems(i, items);
                res.Add(i);
            }

            return res;
        }

        public static void AddChildItems(Menu r, List<Menu> items)
        {
            r.ChildMenu = new List<Menu>();
            var citems = items.Where(n => n.PId == r.Id);

            foreach(var item in citems)
            {
                var m = new Menu() { Id = item.Id, Name = item.Name, PId = item.PId };
                r.ChildMenu.Add(m);
                AddChildItems(m, items);
            }
        }
    }

}
