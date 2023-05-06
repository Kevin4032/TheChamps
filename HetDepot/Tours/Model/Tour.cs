using System.Collections.ObjectModel;
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
        public int MaxReservations { get => _maxReservations; }
        public int FreeSpaces { get => Math.Max(0, _maxReservations - Reservations.Count); }

        public ReadOnlyCollection<Visitor> Reservations
        {
            get { return _reservations.AsReadOnly(); }
        }

        public ReadOnlyCollection<Visitor> Admissions
        {
            get { return _admissions.AsReadOnly(); }
        }

        public string GetTime() => StartTime.ToString("H:mm");

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

        public ListableItem<Tour> ToListableItem() => ToListableItem("", false);
        public ListableItem<Tour> ToListableItem(string info) => ToListableItem(info, false);
        public ListableItem<Tour> ToListableItem(string info, bool disabled)
        {
            /*
             * Geef een ListViewPartedItem terug met tijd en info. Info is standaard leeg maar kan een string of callback zijn zoals
             * "x beschikbare plaatsen" of "x reserveringen" (de controller bepaalt dat)
             * Als disabled true is (bool of callback) kan het item niet geselecteerd worden
             */

            //Reservations.Count <= 0 ? "consoleTourNoReservations" : (Reservations.Count == 1 ? "consoleTourOneReservation" : "consoleTourRervations");
            //


            return new ListViewItem<Tour>(
                new List<ListViewItemPart>()
                {
                    new (GetTime(), 10),
                    new (info),
                },
                this,
                disabled
            );
        }
    }
}
