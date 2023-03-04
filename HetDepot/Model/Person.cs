using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HetDepot.Model
{
    public abstract class Person
    {

        public Person(long id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Name { get; private set; }
        public long Id { get; private set; }
    }
}
