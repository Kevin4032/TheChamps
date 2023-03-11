using HetDepot.People.Model;
using HetDepot.Settings;
using HetDepot.Validation;

namespace HetDepot.JsonReader
{
	public class Repository
	{
		private ValidationService _validationService;

		public Repository(ValidationService validationService)
		{
			_validationService = validationService;
		}

		public List<Person> GetPeople()
		{
			var guidesPath = Path.Combine(Directory.GetCurrentDirectory(), "ExampleFile\\ExampleGuide.json");
			var managersPath = Path.Combine(Directory.GetCurrentDirectory(), "ExampleFile\\ExampleManager.json");
			var visitorsPath = Path.Combine(Directory.GetCurrentDirectory(), "ExampleFIle\\ExampleVisitor.json");

			var result = new List<Person>();

			//TODO: Verkeerde type
			//TODO: Ombouwen lijst
			result.AddRange(AddToPeople<Manager>(managersPath));
			result.AddRange(AddToPeople<Guide>(guidesPath));
			result.AddRange(AddToPeople<Visitor>(visitorsPath));

			return result;
		}

		public Dictionary<string, string> GetSettings()
		{
			var settingsPath = Path.Combine(Directory.GetCurrentDirectory(), "ExampleFile\\ExampleSettings.json");

			//TODO: Lege settings
			var settings = JsonHelper.Read<List<Setting>>(settingsPath);

			var result = new Dictionary<string, string>();

			foreach (var setting in settings)
			{
				result[setting.Name] = setting.Value;
			}

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
