using System.Collections.ObjectModel;
using HetDepot.Errorlogging;
using HetDepot.People.Model;
using HetDepot.Persistence;
using HetDepot.Tours.Model;

namespace HetDepot.Tours
{
    public class TourService : ITourService
    {
        private List<Tour> _tours;
        private IRepository _repository;
        private IDepotErrorLogger _errorLogger;

        public TourService(IRepository repository, IDepotErrorLogger errorLogger)
        {
            _repository = repository;
            _errorLogger = errorLogger;
            _tours = GetTours();
        }

        public ReadOnlyCollection<Tour> Tours { get { return _tours.AsReadOnly(); } }

        public bool AddTourReservation(Tour tour, Visitor visitor)
        {
            return ToursUpdateInvokeMethod(tour, visitor, "AddReservation");
        }

        public bool RemoveTourReservation(Tour tour, Visitor visitor)
        {
            return ToursUpdateInvokeMethod(tour, visitor, "RemoveReservation");
        }

        public bool AddTourAdmission(Tour tour, Visitor visitor)
        {
            return ToursUpdateInvokeMethod(tour, visitor, "AddAdmission");
        }

        public Tour? GetReservation(Visitor visitor)
        {
            foreach (var tour in _tours)
            {
                if (tour.Reservations.Where(v => v.Id == visitor.Id).Any())
                {
                    return tour;
                }
            }

            return null;
        }

        public bool HasReservation(Visitor visitor)
        {
            foreach (var tour in _tours)
            {
                if (tour.Reservations.Where(v => v.Id == visitor.Id).Any())
                    return true;
            }

            return false;
        }

        public bool HasAdmission(Visitor visitor)
        {
            foreach (var tour in _tours)
            {
                if (tour.Admissions.Where(v => v.Id == visitor.Id).Any())
                    return true;
            }

            return false;
        }

        public Tour? getTourByStartTime(DateTime startTime)
        {
            _tours = GetTours();
            return _tours.Single(t => t.StartTime == startTime);
        }

        private bool ToursUpdateInvokeMethod(Tour tour, Visitor visitor, string method)
        {
            try
            {
                var listInstance = _tours.FirstOrDefault(t => t.StartTime == tour.StartTime);

                if (listInstance == null)
                    throw new NullReferenceException("Geen Tour gevonden");
                else
                {

                    var uitvoeren = listInstance.GetType().GetMethod(method);
                    var paramz = new List<object>() { visitor };
                    var resultOk = uitvoeren?.Invoke(listInstance, paramz.ToArray());
                    WriteTourData();
                    return (bool)(resultOk ?? false);
                }

            }
            catch (Exception e)
            {
                _errorLogger.LogError($"{this.GetType()} - Input [Tour:{tour?.StartTime}, Visitor:{visitor?.Id}, Method:{method}] - {e.Message}");
                return false;
            }
        }

        private void WriteTourData()
        {
            _repository.Write(_tours);
        }

        private List<Tour> GetTours()
        {
            var tours = _repository.GetTours();

            if (tours.Count > 0)
                return tours;

            var tourTimes = Program.SettingService.GetTourTimes();
            var maxReservations = Program.SettingService.GetMaxTourReservations();
            var guide = Program.PeopleService.GetGuide();

            foreach (var time in tourTimes)
            {
                tours.Add(new Tour(DateTime.Parse(time), guide, maxReservations, new List<Visitor>(), new List<Visitor>()));
            }

            return tours;
        }
    }
}
