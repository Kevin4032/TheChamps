namespace HetDepot.Settings.Model
{
    public class Setting
    {
        private Dictionary<string, string> _consoleText;
        private HashSet<string> _tourTime;
        private int _maxReservationsPerTour;

        public Setting(Dictionary<string, string> ConsoleText, HashSet<string> TourTimes, int MaxReservationsPerTour)
        {
            _consoleText = ConsoleText;
            _tourTime = TourTimes;
            _maxReservationsPerTour = MaxReservationsPerTour;
        }

        public Dictionary<string, string> ConsoleText { get { return _consoleText; } }
        public HashSet<string> TourTimes { get { return _tourTime; } }
        public int MaxReservationsPerTour { get { return _maxReservationsPerTour; } }
    }


}
