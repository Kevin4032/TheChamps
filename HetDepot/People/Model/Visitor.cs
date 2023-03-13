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
