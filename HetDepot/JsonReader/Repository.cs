using HetDepot.People.Model;
using HetDepot.Settings;
using HetDepot.Validation;

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
			var guides = Path.Combine(Directory.GetCurrentDirectory(), _settingService.GetSettingValue("FileGuides"));
			var managers = Path.Combine(Directory.GetCurrentDirectory(), _settingService.GetSettingValue("FileManagers"));
			var visitors = Path.Combine(Directory.GetCurrentDirectory(), _settingService.GetSettingValue("FileVisitors"));

			var result = new List<Person>();

			//TODO: Verkeerde type
			//TODO: Ombouwen lijst
			result.AddRange(AddToPeople<Manager>(managers));
			result.AddRange(AddToPeople<Guide>(guides));
			result.AddRange(AddToPeople<Visitor>(visitors));

			return result;
		}

		private List<T> AddToPeople<T>(string path) where T : Person
		{
			var result = new List<T>();
			var people = JsonHelper.Read<List<T>>(path);

			foreach (var person in people)
			{
				if (_validationService.ValidForAdministration(person))
					result.Add(person);
				else
					Console.WriteLine($"Lekker bezig {person}");
				//TODO: errors teruggeven / verschil in aangeleverd en ingelezen
			}

			return result;
		}
	}
}
