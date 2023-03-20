using HetDepot.Errorlogging;
using HetDepot.People.Model;
using HetDepot.Settings;
using HetDepot.Tours.Model;

namespace HetDepot.Persistence
{
	public class Repository
	{
		private string _guidesPath;
		private string _managersPath;
		private string _visitorsPath;
		private string _settingsPath;
		private string _toursPath;
		private IDepotDataReadWrite _depotDataReadWrite;
		private IDepotErrorLogger _errorLogger;
		private IDepotDataValidator _validator;

		public Repository(IDepotDataReadWrite dataReadWrite, IDepotErrorLogger errorLogger, IDepotDataValidator validator)
		{ 
			_depotDataReadWrite = dataReadWrite;
			_errorLogger = errorLogger;
			_validator = validator;
			_guidesPath = Path.Combine(Directory.GetCurrentDirectory(), "ExampleFile\\ExampleGuide.json");
			_managersPath = Path.Combine(Directory.GetCurrentDirectory(), "ExampleFile\\ExampleManager.json");
			_visitorsPath = Path.Combine(Directory.GetCurrentDirectory(), "ExampleFile\\ExampleVisitor.json");
			_settingsPath = Path.Combine(Directory.GetCurrentDirectory(), "ExampleFile\\ExampleSettings.json");
			_toursPath = Path.Combine(Directory.GetCurrentDirectory(), "ExampleFile\\ExampleTours.json");
		}

		public void TestErrorlog()
		{
			_errorLogger.LogError("Test error in repository.");
		}

		public List<Person> GetPeople()
		{
			var result = new List<Person>();

			result.AddRange(GetPeople<Guide>(_guidesPath));
			result.AddRange(GetPeople<Manager>(_managersPath));
			result.AddRange(GetPeople<Visitor>(_visitorsPath));

			return result;
		}
		public List<Tour> GetTours()
		{
			var result = new List<Tour>();
			var tours = _depotDataReadWrite.Read<List<TourJsonModel>>(_toursPath);
			//Toelichting Kevin:
			//Als het geen JsonModel is, lopen we tegen issues aan met
			//  1. Geen lege constructor
			//  2. De readOnlyList public attribute werkt niet goed
			//  3. Andere private set attributen werken niet goed (bv starttime en guide)
			//Daar zijn vast fixes voor, ik heb even geen zin om het uit te zoeken, dus zet ik het om.

			foreach (var tour in tours)
			{
				var tourGoed = new Tour(tour.StartTime, tour.Guide!, tour.MaxReservations, tour.Reservations!, tour.Admissions!);
				result.Add(tourGoed);

				//if (tour.Reservations?.Count > 0)
				//{
				//	foreach (var visitor in tour.Reservations)
				//	{
				//		visitor.TourReservation(tourGoed);
				//	}
				//}

				//if (tour.Admissions?.Count > 0)
				//{
				//	foreach (var visitor in tour.Admissions)
				//	{
				//		visitor.TourAdmission(tourGoed);
				//	}
				//}
			}

			return result;
		}

		public Dictionary<string, string> GetSettings()
		{
			//TODO: Lege settings
			var settings = _depotDataReadWrite.Read<List<Setting>>(_settingsPath);

			var result = new Dictionary<string, string>();

			foreach (var setting in settings)
			{
				result[setting.Name] = setting.Value;
			}

			return result;
		}
		public void Write<T>(T objectToWrite)
		{
			if (objectToWrite == null)
				throw new NullReferenceException("No object to write");

			if (objectToWrite.GetType() == typeof(List<Tour>))
				_depotDataReadWrite.Write(_toursPath, objectToWrite);
		}

		private List<T> GetPeople<T>(string path) where T : Person
		{
			var people = _depotDataReadWrite.Read<List<T>>(path);
			var result = new List<T>();

			foreach (var person in people)
			{
				if (_validator.ValidForAdministration(person))
					result.Add(person);
				else
					_errorLogger.LogError($"Onjuiste bezoekerdata - {person.Id}");
			}

			return result;
		}
	}
}

