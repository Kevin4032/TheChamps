using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HetDepot.People.Model
{
    public abstract class Person
    {

        public Person(string id)
        {
            Id = id;
        }

        public string Id { get; private set; }
    }
}
