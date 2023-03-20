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
		private SettingService _settingService;

		public TourService (Repository repository, SettingService settingService) 
		{
			_repository = repository;
			_tours = _repository.GetTours();
			_settingService = settingService;
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

		public TourServiceResult AddTourReservation(DateTime time, Visitor visitor)
		{
			var success = !visitor.TourTaken;
			var message = _settingService.GetSettingValue("consoleVisitorReservationConfirmation");

			if (success)
			{
				var tour = GetTour(time);
				tour.AddReservation(visitor);
				visitor.TourReservation(tour);
			}

			return new TourServiceResult() { Success = success, Message = message };
		}

		public TourServiceResult RemoveTourReservation(DateTime time, Visitor visitor)
		{
			var success = !visitor.TourTaken;
			var message = _settingService.GetSettingValue("consoleVisitorReservationChangeTourConfirmation");

			if (success )
			{ 
				var tour = GetTour(time);
				tour.RemoveReservation(visitor);
				visitor.TourReservation(null!); //Leegmaken tour
			}

			return new TourServiceResult() { Success = success, Message = message };
		}

		public TourServiceResult AddTourAdmission(DateTime time, Visitor visitor)
		{
			var success = !visitor.TourTaken;
			var message = _settingService.GetSettingValue("consoleVisitorReservationConfirmation");

			if (success)
			{
				var tour = GetTour(time);
				tour.AddAdmission(visitor);
				visitor.TourAdmission(tour);
			}

			return new TourServiceResult() { Success = success, Message = message };
		}


		private Tour GetTour(DateTime tourStart)
		{
			return _tours.Where(t => t.StartTime == tourStart).FirstOrDefault() ?? throw new NullReferenceException("Tour Null"); ;
		}

		// TOOD: naar private zetten. Voor test even public.
		public void WriteTourData()
		{
			_repository.Write(_tours);
		}
	}
}
