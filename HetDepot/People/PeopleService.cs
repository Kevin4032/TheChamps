using HetDepot.JsonReader;
using HetDepot.People.Model;
using HetDepot.Settings;
using HetDepot.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace HetDepot.People
{
    public class PeopleService
	{
		private List<Person> _people;
		private ValidationService _validationService;
		private SettingService _settingService;

		private string _guideFile;
		private string _managerFile;
		private string _visitorFile;

		private Dictionary<Type, string> _peopleDefinition;

		public PeopleService(SettingService settingsService, ValidationService validationService)
		{
			//TODO: ioc
			_settingService = settingsService;
			_validationService = validationService;
			_people = new List<Person>();
			_guideFile = Path.Combine(Directory.GetCurrentDirectory(), _settingService.GetSettingValue("FileGuides"));
			_managerFile = Path.Combine(Directory.GetCurrentDirectory(), _settingService.GetSettingValue("FileManagers"));
			_visitorFile = Path.Combine(Directory.GetCurrentDirectory(), _settingService.GetSettingValue("FileVisitors"));

			_peopleDefinition = new Dictionary<Type, string>(3) {
				{ typeof(Guide), Path.Combine(Directory.GetCurrentDirectory(), _settingService.GetSettingValue("FileGuides")) }
				, { typeof(Manager), Path.Combine(Directory.GetCurrentDirectory(), _settingService.GetSettingValue("FileManagers")) }
				, { typeof(Visitor), Path.Combine(Directory.GetCurrentDirectory(), _settingService.GetSettingValue("FileVisitors")) }
				};

			Init();



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

		public Person GetById(string id)
		{
			return _people.FirstOrDefault(p => p.Id == id);
		}

		public Guide GetGuide()
		{
			return _people.FirstOrDefault(p => p.GetType() == typeof(Guide)) as Guide;
		}

		public Manager GetManager()
		{
			return _people.FirstOrDefault(p => p.GetType() == typeof(Manager)) as Manager;
		}

		public void WritePeople()
		{
			Console.WriteLine("People Start");
			foreach (var person in _people)
			{ 
				Console.WriteLine($"{person.GetType()} - {person.Id}");
			}
			Console.WriteLine("People Start");
		}


		private void Init()
		{
			
			var errorList = new List<Person>();

			var types = new List<Type>() { typeof(Manager), typeof(Guide), typeof(Visitor) };

			foreach (var type in types)
			{
				Console.WriteLine($"ditte: {type}");

				if (type == typeof(Manager))
					Console.WriteLine("lekker man");
			}

			//TODO: Verkeerde type
			//var guides = JsonHelper.ReadJson<List<Manager>>(_guideFile);
			//var managers = JsonHelper.ReadJson<List<Manager>>(_managerFile);
			//var visitors = JsonHelper.ReadJson<List<Visitor>>(_visitorFile);

			//AddToPeople(guides);
			//AddToPeople(managers);
			//AddToPeople(visitors);

			foreach (var person in _peopleDefinition)
			{
				if (person.Key == typeof(Manager))
					AddToPeople<Manager>(person.Value);
				if (person.Key == typeof(Guide))
					AddToPeople<Guide>(person.Value);
				if (person.Key == typeof(Visitor))
					AddToPeople<Visitor>(person.Value);
			}


			WritePeople();

			//foreach (var guide in guides)
			//{
			//	var result = _validationService.IsValid(guide);

			//	if (result.IsValidPeopleAdministration)
			//		_people.Add(guide);
			//	else
			//		errorList.Add(guide);
			//}

			//foreach (var manager in managers)
			//{
			//	var result = _validationService.IsValid(manager);

			//	if (result.IsValidPeopleAdministration)
			//		_people.Add(manager);
			//	else
			//		errorList.Add(manager);
			//}

			//foreach (var visitor in visitors)
			//{
			//	var result = _validationService.IsValid(visitor);

			//	if (result.IsValidPeopleAdministration)
			//		_people.Add(visitor);
			//	else
			//		errorList.Add(visitor);
			//}
		}

		private void AddToPeople<T>(IEnumerable<T> people) where T : Person
		{


			foreach (var person in people)
			{
				if (_validationService.ValidForAdministration(person))
					_people.Add(person);
				else
					Console.WriteLine($"Lekker bezig {person}");
			}
		}

		private void AddToPeople<T>(string path) where T : Person
		{
			var people = JsonHelper.ReadJson<List<T>>(path);

			foreach (var person in people)
			{
				if (_validationService.ValidForAdministration(person))
					_people.Add(person);
				else
					Console.WriteLine($"Lekker bezig {person}");
			}
		}


		private void AddToPeople<T>(T type, string path) where T : Person
		{
			var people = JsonHelper.ReadJson<List<T>>(path);

			foreach (var person in people)
			{
				if (_validationService.ValidForAdministration(person))
					_people.Add(person);
				else
					Console.WriteLine($"Lekker bezig {person}");
			}
		}

	}
}
