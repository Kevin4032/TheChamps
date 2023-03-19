using HetDepot.Tours.Model;

namespace HetDepot.People.Model
{
    public class Visitor : Person
    {
        public Visitor(string id) : base(id)
        {
        }

        public Tour? Tour { get; private set; }
        public bool TourTaken { get; private set; } //TODO: Verplaatsen naar file.

        public void TourReservation(Tour tour)
        {
            if (!TourTaken)
                Tour = tour;
        }

        public void TourAdmission(Tour tour)
        {
            if (!TourTaken)
            {
                Tour = tour;
                TourTaken = true;
            }
        }
    }
}
