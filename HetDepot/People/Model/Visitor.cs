using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HetDepot.People.Model
{
    public class Visitor : Person
    {
        public Visitor(string id) : base(id)
        {
        }

        public string Tour { get; set; }
        public bool TourTaken { get; set; } //TODO: Verplaatsen naar file.
    }
}
