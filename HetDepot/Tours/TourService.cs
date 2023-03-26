using HetDepot.Errorlogging;
using HetDepot.People;
using HetDepot.People.Model;
using HetDepot.Persistence;
using HetDepot.Settings;
using HetDepot.Tours.Model;
using System.Collections.ObjectModel;

namespace HetDepot.Tours
{
	public class TourService : ITourService
	{
		private List<Tour> _tours;
		private IRepository _repository;
		private IDepotErrorLogger _errorLogger;
		private IPeopleService _peopleService;
		private ISettingService _settingService;

		public TourService (IRepository repository, ISettingService settingService, IPeopleService peopleService, IDepotErrorLogger errorLogger) 
		{
			_repository = repository;
			_settingService = settingService;
			_errorLogger = errorLogger;
			_peopleService = peopleService;
			_tours = GetTours();
		}

		public ReadOnlyCollection<Tour> Tours { get { return _tours.AsReadOnly(); } }

		public void VoorTestEnDemoDoeleinden()
		{
			Console.WriteLine($"===========================================");
			Console.WriteLine($"==                 Tours                 ==");
			Console.WriteLine($"===========================================");

			foreach (var tour in _tours)
			{
				Console.WriteLine($"==         Tour: {tour.StartTime}        ==");
				Console.WriteLine($"===========================================");
				Console.WriteLine($"==              Reservations           ==");
				Console.WriteLine($"===========================================");
				foreach (var reservation in tour.Reservations)
				{
					Console.WriteLine($"{reservation.Id}");
				}

				Console.WriteLine("===========================================");
				Console.WriteLine($"==                Admissions            ==");
				Console.WriteLine("===========================================");
				foreach (var admission in tour.Admissions)
				{
					Console.WriteLine($"{admission.Id}");
				}
			}
		}

		public bool AddTourReservation(Tour tour, Visitor visitor)
		{
			var success = !HasAdmission(visitor);

			if (success)
			{
				_tours.FirstOrDefault(t => t.StartTime == tour.StartTime)?.AddReservation(visitor);
				WriteTourData(); //TODO: Nakijken of dit wat is.
			}
			else
				return false;

			return true;
		}

		public bool RemoveTourReservation(Tour tour, Visitor visitor)
		{
			var success = !HasAdmission(visitor);

			if (success)
			{
				_tours.FirstOrDefault(t => t.StartTime == tour.StartTime)?.RemoveReservation(visitor);
				WriteTourData();//TODO: Nakijken of dit wat is.
			}
			else
				return false;

			return true;
		}

		public bool AddTourAdmission(Tour tour, Visitor visitor)
		{
			var success = !HasAdmission(visitor);

			if (success)
			{
				_tours.FirstOrDefault(t => t.StartTime == tour.StartTime)?.AddAdmission(visitor);
				WriteTourData();
			}
			else
				return false;

			return true;
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

		private void WriteTourData()
		{
			_repository.Write(_tours);
		}

		private List<Tour> GetTours()
		{
			var tours = _repository.GetTours();

			if (tours.Count > 0)
				return tours;

			var tourTimes = _settingService.GetTourTimes();
			var maxReservations = _settingService.GetMaxTourReservations();
			var guide = _peopleService.GetGuide();

			foreach (var time in tourTimes)
			{
				tours.Add(new Tour(DateTime.Parse(time), guide, maxReservations, new List<Visitor>(), new List<Visitor>()));
			}

			return tours;
		}
	}
}
