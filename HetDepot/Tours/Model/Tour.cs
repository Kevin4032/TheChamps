using HetDepot.People.Model;
using HetDepot.Views.Interface;
using HetDepot.Views.Parts;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

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
			return _admissions.Remove(admissionToRemove);
        }

        public ListableItem ToListableItem()
        {
            /*
             * Geef een ListViewPartedItem terug met tijd en aantal plaatsen
             * Wanneer er geen vrije plaatsen zijn zet Disabled op true. Dit zorgt ervoor dat de optie niet gekozen mag
             * worden.
             */
            return new ListViewItem(new List<ListViewItemPart>
                {
                    new (GetTime(), 10),
                    new (FreeSpaces() > 0 ? FreeSpaces() + " plaatsen" : "Vol")
                },
                this, FreeSpaces() <= 0);
        }
        
        public Tour GetNextTour(List<Tour> tours)
        {
            /* Geeft de volgende tour-tijd voor gebruik in de GuideShowAndSelectTour Controller */
            DateTime currentTime = DateTime.Now;

            // Filter tours that have not yet started
            var upcomingTours = tours.Where(tour => tour.StartTime > currentTime).ToList();

            // Sort tours by start time in ascending order
            upcomingTours.Sort((tour1, tour2) => tour1.StartTime.CompareTo(tour2.StartTime));

            // Get the first tour in the sorted list, which will be the next upcoming tour
            Tour nextTour = upcomingTours.FirstOrDefault();

            return nextTour;
        }

    }
}