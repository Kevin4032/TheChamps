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
		private Repository _repository;

		public PeopleService(Repository repository)
		{
			//TODO: ioc
			_repository = repository;
			_people = new List<Person>();
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

		public void WritePeopleToConsole()
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

			//TODO: Verkeerde type
			foreach (var person in _peopleDefinition)
			{
				if (person.Key == typeof(Manager))
					AddToPeople<Manager>(person.Value);
				if (person.Key == typeof(Guide))
					AddToPeople<Guide>(person.Value);
				if (person.Key == typeof(Visitor))
					AddToPeople<Visitor>(person.Value);
			}

			WritePeopleToConsole();
		}

		private void AddToPeople<T>(string path) where T : Person
		{
			var people = JsonHelper.Read<List<T>>(path);

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
