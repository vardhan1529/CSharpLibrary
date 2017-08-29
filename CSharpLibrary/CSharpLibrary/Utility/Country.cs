using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLibrary.Utility
{
    public class Country : CountryBase
    {
        public Country()
        {
        }

        public Country(string name, int id, int states) : base(name,id,states)
        {
        }

        public static readonly Country India = new Country("India", 1,  29 );

        public static readonly Country USA = new Country("United States of America",2, 50 );
    }

    public abstract class CountryBase
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public int States { get; set; }

        public CountryBase()
        {
        }

        protected CountryBase(string name, int id, int states)
        {
            Name = name;
            Id = id;
            States = states;
        }

        public static string GetNameFromId<T>(int id) where T:CountryBase
        {
            var info = typeof(T);
            var fields = info.GetFields(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public);
            foreach (var field in fields)
            {
                var country = field.GetValue(null) as T;
                if (country != null)
                {
                    if(country.Id == id)
                    {
                        return country.Name;
                    }
                }
            }

            return null;
        }
    }
}
