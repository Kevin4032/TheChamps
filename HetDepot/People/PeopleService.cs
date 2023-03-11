using HetDepot.Persistence;
using HetDepot.People.Model;
using System.Text.RegularExpressions;

namespace HetDepot.People
{
    public class PeopleService
	{
		private Repository _repository;
		private List<Person> _people;

		public PeopleService(Repository repository)
		{
			_repository = repository;
			_people = _repository.GetPeople();
		}

		public IEnumerable<Visitor> GetVisitors()
		{
			var visitors = new List<Visitor>();
			
			foreach (var visitor in  _people.FindAll(p => p.GetType() == typeof(Visitor)))
			{
				visitors.Add(visitor as Visitor);
			}

			return visitors;
		}

		public Person GetById(string id) => _people.FirstOrDefault(p => p.Id == id);
		public Guide GetGuide() => _people.FirstOrDefault(p => p.GetType() == typeof(Guide)) as Guide;
		public Manager GetManager() => _people.FirstOrDefault(p => p.GetType() == typeof(Manager)) as Manager;


	}
}
