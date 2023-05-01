using HetDepot.People.Model;

namespace HetDepot.Tours.Model
{
    public class TourJsonModel
    {
        public DateTime StartTime { get; set; }
        public Guide? Guide { get; set; }
        public List<Visitor>? Reservations { get; set; }
        public List<Visitor>? Admissions { get; set; }
        public int MaxReservations { get; set; }

    }
}
