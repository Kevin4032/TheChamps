namespace HetDepot.People.Model
{
    public abstract class Person : IEquatable<Person>, IComparable<Person>
    {

        public Person(string id)
        {
            Id = id;
        }

        public string Id { get; private set; }

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;

            if (obj.GetType().BaseType?.BaseType != typeof(Person) || obj.GetType().BaseType != typeof(Person))
                return Equals((Person)obj);

            return false;
        }

        public bool Equals(Person other)
        {
            if (other == null)
                return false;

            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public int CompareTo(Person other)
        {
            if (other == null)
                return 1;

            return this.Id.CompareTo(other.Id);
        }
    }
}
