using HetDepot.People.Model;
using HetDepot.Persistence;
using HetDepot.Registration;
using HetDepot.Tours.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HetDepot.Tours
{
	public class TourService
	{
		private List<Tour> _tours;
		private Repository _repository;
		private TourValidationService _tourValidationService;

		public TourService (Repository repository, TourValidationService tourValidation) 
		{
			_repository = repository;
			_tours = _repository.GetTours();
			_tourValidationService = tourValidation;
		}

		public ReadOnlyCollection<Tour> Tours { get { return _tours.AsReadOnly(); } }

		public TourServiceResult AddTourReservation(DateTime time, Visitor visitor)
		{
			var result = _tourValidationService.VisitorAllowedToMakeReservation(visitor);

			if (result.Success)
			{
				var tour = GetTour(time);
				tour.AddReservation(visitor);
			}

			return result;
		}

		public void RemoveTourReservation(DateTime time, Visitor visitor)
		{
			var tour = GetTour(time);
			tour.RemoveReservation(visitor);
		}

		public void AddTourAdmission(DateTime time, Visitor visitor)
		{
			var tour = GetTour(time);
			tour.AddAdmission(visitor);
		}


		private Tour GetTour(DateTime tourStart)
		{
			return _tours.Where(t => t.StartTime == tourStart).FirstOrDefault() ?? throw new NullReferenceException("Tour Null"); ;
		}
	}
}
