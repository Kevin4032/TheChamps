using System.Text.RegularExpressions;
using System.Text.Json;
using HetDepot.Errorlogging;
using HetDepot.Persistence;
using HetDepot.Settings.Model;
using System.Text.RegularExpressions;
using HetDepot.People.Model;
using HetDepot.Tours.Model;


namespace HetDepot.Settings
{
    public class SettingService : ISettingService
    {
        private Setting _settings;
        private IRepository _repository;
        private IDepotErrorLogger _errorLogger;
        public const string TourFilePrefix = "tours_";

        public SettingService(IRepository repository, IDepotErrorLogger errorLogger)
        {
            _repository = repository;
            _errorLogger = errorLogger;
            _settings = _repository.GetSettings();
            _initializeTourStorage();
        }

        public HashSet<String> GetTourTimes()
        {
            return _settings.TourTimes;
        }

        public int GetMaxTourReservations() => _settings.MaxReservationsPerTour;

        public string GetConsoleText(string name, Dictionary<string, string>? components = null)
        {
            /*
            Get console text from settings. This may contain {components}, which will be replaced with their content in the "components" dictionary, if provided
            */
            try
            {
                if (components == null)
                    return _settings.ConsoleText[name];
                return (new Regex(@"\{(\w+)\}")).Replace(_settings.ConsoleText[name],
                    match => components.ContainsKey(match.Groups[1].Captures[0].ToString())
                        ? components[match.Groups[1].Captures[0].ToString()]
                        : match.Value);
            }
            catch (Exception e)
            {
                _errorLogger.LogError($"{this.GetType()} - Input [{name}] - {e.Message}");
            }

            // return string.Empty; //TODO: nadenken of dit eigen erro moet worden
            return
                name; // Liever gewoon fallback naar de "name", dan staat er tenminste iets toch? (Idem in de regex hierboven)
        }

        private void _initializeTourStorage()
        {
            var settingsDir = GetSettingDir();
            string path = GetToursPath();

            if (!Directory.Exists(settingsDir))
            {
                Directory.CreateDirectory(settingsDir);
            }

            if (File.Exists(path))
            {
                // Do not generate tours
                return;
            }

            var createdFile = File.Create(path);
            createdFile.Close();

            var tourTimes = GetTourTimes();

            var tours = tourTimes.Select((string time) =>
            {
                DateTime today = DateTime.Today;
                int hours = Int32.Parse(time.Split(':')[0]);
                int minutes = Int32.Parse(time.Split(':')[1]);

                return new Tour(
                    new DateTime(today.Year, today.Month, today.Day, hours, minutes, 0),
                    GetMaxTourReservations(),
                    new List<Visitor>(),
                    new List<Visitor>());
            }).ToList();

            StreamWriter writer = new StreamWriter(path);
            writer.Write(JsonSerializer.Serialize(tours));
            writer.Flush();
            writer.Close();

            return;
        }

        public static string GetSettingDir()
        {
            // Create the tours file for today is it does not exist
            string workingDir = Directory.GetCurrentDirectory();
            string settingsDir = workingDir + "/WorkingFiles/";

            return settingsDir;
        }

        private static string GetToursFilename()
        {
            DateTime today = DateTime.Today;
            return $"{TourFilePrefix}{today.Day}_{today.Month}_{today.Year}.json";
        }

        public static string GetToursPath()
        {
            return GetSettingDir() + GetToursFilename();
        }
    }
}
