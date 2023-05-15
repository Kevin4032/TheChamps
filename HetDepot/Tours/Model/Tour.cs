using System.Collections.ObjectModel;
using System.Globalization;
using System.Text.Json.Serialization;
using HetDepot.People.Model;
using HetDepot.Views.Interface;
using HetDepot.Views.Parts;

namespace HetDepot.Tours.Model
{
    public class Tour : IListableObject<Tour>
    {
        private List<Visitor> _reservations;
        private List<Visitor> _admissions;
        private int _maxReservations;

        [JsonConstructor]
        public Tour(DateTime startTime, int maxReservations, List<Visitor> reservations, List<Visitor> admissions)
        {
            StartTime = startTime;
            _reservations = reservations;
            _admissions = admissions;
            _maxReservations = maxReservations;
		}

        public DateTime StartTime { get; private set; }
        public int MaxReservations { get { return _maxReservations; } }
        public ReadOnlyCollection<Visitor> Reservations
        {
            get { return _reservations.AsReadOnly(); }
        }

        public ReadOnlyCollection<Visitor> Admissions
        {
            get { return _admissions.AsReadOnly(); }
        }

        public string GetTime() => StartTime.ToString("H:mm");
        public int FreeSpaces() => Math.Max(0, _maxReservations - Reservations.Count);
        public override string ToString() => GetTime();

        public bool AddReservation(Visitor visitor)
        {
            _reservations.Add(visitor);
            return true;
        }

        public bool RemoveReservation(Visitor visitor)
        {
            var reservationToRemove = _reservations.FirstOrDefault(v => v.Id == visitor.Id);
            if (reservationToRemove == null)
                return false;
            return _reservations.Remove(reservationToRemove);
        }

        public bool AddAdmission(Visitor visitor)
        {
            _admissions.Add(visitor);
            return true;
        }

        public bool RemoveAdmission(Visitor visitor)
        {
            var admissionToRemove = _admissions.FirstOrDefault(v => v.Id == visitor.Id);
            if (admissionToRemove == null)
                return false;
            return _admissions.Remove(admissionToRemove);
        }

        public ListableItem<Tour> ToListableItem()
        {
            /*
             * Geef een ListViewPartedItem terug met tijd en aantal plaatsen
             * Wanneer er geen vrije plaatsen zijn zet Disabled op true. Dit zorgt ervoor dat de optie niet gekozen mag
             * worden.
             */

            var settingService = Program.SettingService;
            var freeSpaces = FreeSpaces();
            var spacesString = freeSpaces <= 0 ? "consoleTourNoFreeSpaces" : (freeSpaces == 1 ? "consoleTourOneFreeSpace" : "consoleTourFreeSpaces");

            return new ListViewItem<Tour>(
                new List<ListViewItemPart>()
                {
                    new (GetTime(), 10),
                    new (Program.SettingService.GetConsoleText(spacesString, new ()
                    {
                        ["count"] = freeSpaces.ToString(),
                    }))
                },
                this,
                freeSpaces <= 0
            );
        }

        public string getYearAndWeek()
        {
            /* Used for manager statistics */

            return "Week " + GetIso8601WeekOfYear(StartTime) + " " + StartTime.Year;
        }

        // This presumes that weeks start with Monday.
// Week 1 is the 1st week of the year with a Thursday in it.
        private static int GetIso8601WeekOfYear(DateTime time)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

    }
}
