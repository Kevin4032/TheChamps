using HetDepot.Persistence;
using HetDepot.People.Model;
using System.Text.RegularExpressions;

namespace HetDepot.People
{
    public class PeopleService
	{
		private string _guidesPath;
		private string _managersPath;
		private string _visitorsPath;

		private Repository _repository;
		private List<Person> _people;

		private Regex _employeeCheckId;
		private Regex _visitorCheckId;

		public PeopleService(Repository repository)
		{
			_repository = repository;
			_guidesPath = Path.Combine(Directory.GetCurrentDirectory(), "ExampleFile\\ExampleGuide.json");
			_managersPath = Path.Combine(Directory.GetCurrentDirectory(), "ExampleFile\\ExampleManager.json");
			_visitorsPath = Path.Combine(Directory.GetCurrentDirectory(), "ExampleFIle\\ExampleVisitor.json");
			_employeeCheckId = new Regex(@"^[dD]\d{10}");
			_visitorCheckId = new Regex(@"^[eE]\d{10}");
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

		private void InitializePeopleData()
		{
			var visitors = _repository.GetPeople<Visitor>(_visitorsPath);
			var guides = _repository.GetPeople<Visitor>(_guidesPath);
			var managers = _repository.GetPeople<Visitor>(_managersPath);
		}

		private bool ValidForAdministration<T>(T dataToValidate) where T : Person
		{
			var validVisitorId = _visitorCheckId.IsMatch(dataToValidate.Id);
			var validEmployeeId = _employeeCheckId.IsMatch(dataToValidate.Id);

			var dataIsGuide = dataToValidate is Guide;
			var dataIsManager = dataToValidate is Manager;
			var dataIsVisitor = dataToValidate is Visitor;

			if ((dataIsGuide && validEmployeeId) || (dataIsManager && validEmployeeId) || (dataIsVisitor && validVisitorId))
			{
				return true;
			}

			return false;
		}
	}
}
