using HetDepot.People.Model;
using HetDepot.Settings;
using HetDepot.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HetDepot.JsonReader
{
	public class Repository
	{
		private SettingService _settingService;
		private ValidationService _validationService;

		public Repository(SettingService settingService, ValidationService validationService)
		{
			_settingService = settingService;
			_validationService = validationService;
		}

		public List<Person> GetPeople()
		{
			var peopleDefinition = new Dictionary<Type, string>(3) {
				{ typeof(Guide), Path.Combine(Directory.GetCurrentDirectory(), _settingService.GetSettingValue("FileGuides")) }
				, { typeof(Manager), Path.Combine(Directory.GetCurrentDirectory(), _settingService.GetSettingValue("FileManagers")) }
				, { typeof(Visitor), Path.Combine(Directory.GetCurrentDirectory(), _settingService.GetSettingValue("FileVisitors")) }
				};

			var errorList = new List<Person>();

			//TODO: Verkeerde type
			foreach (var person in peopleDefinition)
			{
				if (person.Key == typeof(Manager))
					AddToPeople<Manager>(person.Value);
				if (person.Key == typeof(Guide))
					AddToPeople<Guide>(person.Value);
				if (person.Key == typeof(Visitor))
					AddToPeople<Visitor>(person.Value);
			}

			return new List<Person>();
		}

		private void Init()
		{
			
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
