using System.Collections.Generic;
using Microsoft.VisualBasic;

namespace Core.Database
{
    public class CharList
    {
        private List<string> names;

        public CharList()
        {
            names = new List<string>();
        }

        public bool Find(string Name)
        {
            return names.Contains(Strings.LCase(Name));
        }

        public CharList Find(string Name, ref bool RetValue)
        {
            RetValue = names.Contains(Strings.LCase(Name));
            return this;
        }

        public CharList Add(string Name)
        {
            if (Find(Name))
                return this;

            names.Add(Strings.LCase(Name));

            return this;
        }

        public CharList Remove(string Name)
        {
            if (!Find(Name))
                return this;

            names.Remove(Strings.LCase(Name));

            return this;
        }

        public CharList Clear()
        {
            if (names is null)
                names = new List<string>();
            if (names.Count <= 0)
                return this;

            names.Clear();

            return this;
        }

    }
}