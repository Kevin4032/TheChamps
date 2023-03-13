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
