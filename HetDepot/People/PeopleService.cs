using HetDepot.Errorlogging;
using HetDepot.People.Model;
using HetDepot.Persistence;

namespace HetDepot.People
{
    public class PeopleService : IPeopleService
    {
        private List<Person> _people;
        private IRepository _repository;
        private IDepotErrorLogger _errorLogger;

        public PeopleService(IRepository repository, IDepotErrorLogger errorLogger)
        {
            _repository = repository;
            _errorLogger = errorLogger;
            _people = _repository.GetPeople();
        }

        /// <exception cref="NullReferenceException">
        /// Thrown when no visitors found
        /// </exception>
        public IReadOnlyCollection<Visitor> GetVisitors()
        {
            var visitors = new List<Visitor>();
            var visitorsInPeople = _people.FindAll(p => p.GetType() == typeof(Visitor)) ?? throw new NullReferenceException("GetVisitors - No Visitors found");
            return visitorsInPeople.Select(visitor => (Visitor)visitor).ToList().AsReadOnly();
        }

        /// <exception cref="NullReferenceException">
        /// Thrown when no id found
        /// </exception>
        public Person GetById(string id) => _people.FirstOrDefault(p => p.Id == id) ?? throw new NullReferenceException("No Person Found");
        /// <exception cref="NullReferenceException">
        /// Thrown when no id found for visitor
        /// </exception>
        public Visitor GetVisitorById(string id) => _people.FirstOrDefault(p => p.Id == id) as Visitor ?? throw new NullReferenceException("No Visitor Found");
        /// <exception cref="NullReferenceException">
        /// Thrown when no id found
        /// </exception>
        public Guide GetGuide()
        {
            var guides = _people.Where(p => p.GetType() == typeof(Guide));

            //if (guides.Count() > 1)
            //    throw new NullReferenceException("Te veel Gidsen opgevoerd in ExampleGuide.json, er mag maar 1 Gids zijn opgevoerd.");

            var hoi = guides.GroupBy(p => p.Id);
            foreach (var q in hoi)
                Console.WriteLine(q);

            Console.ReadLine();

            return _people.FirstOrDefault(p => p.GetType() == typeof(Guide)) as Guide ?? throw new NullReferenceException("No Guide Found");
        }
        /// <exception cref="NullReferenceException">
        /// Thrown when no id found
        /// </exception>
        public Manager GetManager() => _people.FirstOrDefault(p => p.GetType() == typeof(Manager)) as Manager ?? throw new NullReferenceException("No Manager Found");
    }
}
