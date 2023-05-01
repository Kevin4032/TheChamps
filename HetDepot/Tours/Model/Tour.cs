using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using HetDepot.People.Model;
using HetDepot.Views.Interface;
using HetDepot.Views.Parts;

namespace HetDepot.Tours.Model
{
    public class Tour : IListableObject
    {
        private List<Visitor> _reservations;
        private List<Visitor> _admissions;
        private int _maxReservations;

        [JsonConstructor]
        public Tour(DateTime startTime, Guide guide, int maxReservations, List<Visitor> reservations, List<Visitor> admissions)
        {
            StartTime = startTime;
            _reservations = reservations;
            _admissions = admissions;
            _maxReservations = maxReservations;
            Guide = guide;
        }

        public DateTime StartTime { get; private set; }
        public Guide Guide { get; set; }
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

        public ListableItem ToListableItem()
        {
            /*
             * Geef een ListViewPartedItem terug met tijd en aantal plaatsen
             * Wanneer er geen vrije plaatsen zijn zet Disabled op true. Dit zorgt ervoor dat de optie niet gekozen mag
             * worden.
             */

            var settingService = Program.SettingService;
            var freeSpaces = FreeSpaces();
            var spacesString = freeSpaces <= 0 ? "consoleTourNoFreeSpaces" : (freeSpaces == 1 ? "consoleTourOneFreeSpace" : "consoleTourFreeSpaces");

            return new ListViewItem(
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
    }
}
