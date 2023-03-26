using HetDepot.Errorlogging;
using HetDepot.People.Model;
using HetDepot.Persistence;
using HetDepot.Settings;
using HetDepot.Tours.Model;
using System.Collections.ObjectModel;

namespace HetDepot.Tours
{
	public class TourService
	{
		private List<Tour> _tours;
		private Repository _repository;
		private IDepotErrorLogger _errorLogger;
		private SettingService _settingService;

		public TourService (Repository repository, SettingService settingService, IDepotErrorLogger errorLogger) 
		{
			_repository = repository;
			_settingService = settingService;
			_errorLogger = errorLogger;
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

		public TourServiceResult AddTourReservation(Tour tour, Visitor visitor)
		{
			var success = !HasAdmission(visitor);
			var message = _settingService.GetSettingValue("consoleVisitorReservationConfirmation");

			if (success)
			{
				_tours.FirstOrDefault(t => t.StartTime == tour.StartTime)?.AddReservation(visitor);
				WriteTourData(); //TODO: Nakijken of dit wat is.
			}

			return new TourServiceResult() { Success = success, Message = message };
		}

		public TourServiceResult RemoveTourReservation(Tour tour, Visitor visitor)
		{
			var success = !HasAdmission(visitor);
			var message = _settingService.GetSettingValue("consoleVisitorReservationChangeTourConfirmation");

			if (success )
			{
				_tours.FirstOrDefault(t => t.StartTime == tour.StartTime)?.RemoveReservation(visitor);
				//tour.RemoveReservation(visitor);
				WriteTourData();//TODO: Nakijken of dit wat is.
			}

			return new TourServiceResult() { Success = success, Message = message };
		}

		public TourServiceResult AddTourAdmission(DateTime time, Visitor visitor)
		{
			var success = !HasAdmission(visitor);
			var message = _settingService.GetSettingValue("consoleVisitorReservationConfirmation");

			if (success)
			{
				var tour = GetTour(time);
				tour.AddAdmission(visitor);
			}

			return new TourServiceResult() { Success = success, Message = message };
		}

		private Tour GetTour(DateTime tourStart)
		{
			return _tours.Where(t => t.StartTime == tourStart).FirstOrDefault() ?? throw new NullReferenceException("Tour Null"); ;
		}
		private List<Tour> GetTours()
		{
			var tours = _repository.GetTours();

			if (tours.Count > 0)
				return tours;

			var tourTimes = _settingService.GetTourTimes();
			var maxReservations = _settingService.GetMaxTourReservations();
			var guide = new Guide("zit scheef");

			foreach (var time in tourTimes)
			{
				tours.Add(new Tour(DateTime.Parse(time), guide, maxReservations, new List<Visitor>(), new List<Visitor>()));
			}

			return tours;
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

		// TOOD: naar private zetten. Voor test even public.
		public void WriteTourData()
		{
			_repository.Write(_tours);
		}
	}
}
