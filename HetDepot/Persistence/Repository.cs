using HetDepot.People.Model;
using HetDepot.Registration.Model;
using HetDepot.Settings;
using System.Buffers;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.XPath;

namespace HetDepot.Persistence
{
	public class Repository
	{
		private string _guidesPath;
		private string _managersPath;
		private string _visitorsPath;
		private string _settingsPath;
		private string _admissionsPath;
		private string _reservationsPath;
		private IDepotDataReadWrite _depotDataReadWrite;
		private IDepotLogger _depotLogger;
		private Regex _employeeCheckId;
		private Regex _visitorCheckId;

		public Repository(IDepotDataReadWrite dataReadWrite, IDepotLogger depotLogger)
		{ 
			_depotDataReadWrite = dataReadWrite;
			_depotLogger = depotLogger;
			_guidesPath = Path.Combine(Directory.GetCurrentDirectory(), "ExampleFile\\ExampleGuide.json");
			_managersPath = Path.Combine(Directory.GetCurrentDirectory(), "ExampleFile\\ExampleManager.json");
			_visitorsPath = Path.Combine(Directory.GetCurrentDirectory(), "ExampleFile\\ExampleVisitor.json");
			_settingsPath = Path.Combine(Directory.GetCurrentDirectory(), "ExampleFile\\ExampleSettings.json");
			_admissionsPath = Path.Combine(Directory.GetCurrentDirectory(), "ExampleFile\\ExampleTourAdmissions.json");
			_reservationsPath = Path.Combine(Directory.GetCurrentDirectory(), "ExampleFile\\ExampleTourReservations.json");
			_employeeCheckId = new Regex(@"^[dD]\d{10}");
			_visitorCheckId = new Regex(@"^[eE]\d{10}");
		}

		public void TestErrorlog()
		{
			_depotLogger.LogError("Test error in repository.");
		}

		public Admission GetAdmissions() => new Admission() { Admissions = _depotDataReadWrite.Read<HashSet<string>>(_admissionsPath) };
		public Reservation GetReservations() => new Reservation() { Reservations = _depotDataReadWrite.Read<HashSet<string>>(_admissionsPath) };
		public List<Person> GetPeople()
		{
			var result = new List<Person>();

			result.AddRange(GetPeople<Guide>(_guidesPath));
			result.AddRange(GetPeople<Manager>(_managersPath));
			result.AddRange(GetPeople<Visitor>(_visitorsPath));

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
			if (objectToWrite.GetType() == typeof(Admission))
				_depotDataReadWrite.Write<T>(_admissionsPath, objectToWrite);
			if (objectToWrite.GetType() == typeof(Reservation))
				_depotDataReadWrite.Write<T>(_reservationsPath, objectToWrite);
		}

		private List<T> GetPeople<T>(string path) where T : Person
		{
			var people = _depotDataReadWrite.Read<List<T>>(path);
			var result = new List<T>();

			foreach (var person in people)
			{
				if (ValidForAdministration(person))
					result.Add(person);
				else
					_depotLogger.LogError($"Onjuiste bezoekerdata - {person.Id}");
			}

			return result;
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

