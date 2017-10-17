using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLibrary.ConceptSamples
{
    //https://github.com/bchavez/Bogus/tree/master/Source/Bogus
    //This contains the generic implementation for generating test data
    class GenericMockDataGenerator<T>
    {
        class Rule
        {
            public string PropertyName { get; set; }

            public Func<object> Action { get; set; }
        }

        private MemberInfo[] MembersInfo { get; set; }
        private List<Rule> Rules = new List<Rule>();

        public GenericMockDataGenerator()
        {
            MembersInfo = typeof(T).GetMembers();
        }
        private IEnumerable<T> GenerateMockData(int noOfItems)
        {
            var t = typeof(T);
            var data = new List<T>();
            if (t.IsClass)
            {
                //Yet to implement completely
                for (var i = 0; i < noOfItems; i++)
                {
                    data.Add(Activator.CreateInstance<T>());
                }
            }
            else
            {
                if (t == typeof(int))
                {
                    var r = new Random();
                    for (var i = 0; i < noOfItems; i++)
                    {
                        data.Add((dynamic)r.Next());
                    }
                }
            }

            return data;
        }

        public GenericMockDataGenerator<T> AddRule<TP>(Expression<Func<T, TP>> property, Func<TP> inv)
        {
            Rules.Add(new Rule() { Action = () => inv(), PropertyName = (property.Body as MemberExpression).Member.Name });
            return this;
        }

        public List<T> Generate(int noOfInstances)
        {
            if(noOfInstances < 1)
            {
                return null;
            }

            var d = new List<T>();
            for(var i = 1; i<=noOfInstances; i++)
            {
                d.Add(CreateAndPopulate());
            }

            return d;
        }

        public T Generate()
        {
            return CreateAndPopulate();
        }

        private T CreateAndPopulate()
        {
            var instance = Activator.CreateInstance<T>();
            foreach (var r in Rules)
            {
                foreach (var member in MembersInfo)
                {
                    if (member.Name == r.PropertyName)
                    {
                        (member as PropertyInfo).SetValue(instance, r.Action(), null);
                        break;
                    }
                }
            }

            return instance;
        }
        public List<T> Plinq(int noOfItems)
        {
            return GenerateMockData(noOfItems).ToList();
        }
    }
}
