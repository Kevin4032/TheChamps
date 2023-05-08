using System.Text.Json;
using HetDepot.Errorlogging;
using HetDepot.People.Model;
using HetDepot.Settings;
using HetDepot.Settings.Model;
using HetDepot.Tours.Model;

namespace HetDepot.Persistence
{
    public class Repository : IRepository
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
            _guidesPath = Path.Combine(Directory.GetCurrentDirectory(), "ExampleFile", "ExampleGuide.json");
            _managersPath = Path.Combine(Directory.GetCurrentDirectory(), "ExampleFile", "ExampleManager.json");
            _visitorsPath = Path.Combine(Directory.GetCurrentDirectory(), "ExampleFile", "ExampleVisitor.json");
            _settingsPath = Path.Combine(Directory.GetCurrentDirectory(), "ExampleFile", "ExampleSettings.json");
            _toursPath = SettingService.GetToursPath();
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
        public List<Tour> GetTours(string alternativeTourPath = null)
        {
            var result = new List<Tour>();

            var tours = _depotDataReadWrite.Read<List<TourJsonModel>>(alternativeTourPath ?? _toursPath);

            if (tours == null)
                return result;
            else
            {
                foreach (var tour in tours)
                {
                    if (tour != null)
                        result.Add(new Tour(tour.StartTime, tour.MaxReservations, tour.Reservations ?? new(), tour.Admissions ?? new()));
                }
            }

            return result;
        }

        public List<List<Tour>> GetAllTours()
        {
            var workingDir = SettingService.GetSettingDir();
            var allTourFiles =
                Directory.GetFiles(workingDir, $"{SettingService.TourFilePrefix}*.json").ToList();

            allTourFiles.Reverse(); // Newest first

            return allTourFiles.Select(tourPath => GetTours(tourPath)).ToList();
        }

        public Setting GetSettings()
        {
            return _depotDataReadWrite.Read<Setting>(_settingsPath);
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

