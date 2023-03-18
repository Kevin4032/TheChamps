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
		private RegistrationService _registrationService;

		public TourService (Repository repository, RegistrationService registrationService) 
		{
			_repository = repository;
			_registrationService = registrationService;
			_tours = _repository.GetTours();
		}

		public ReadOnlyCollection<Tour> Tours { get { return _tours.AsReadOnly(); } }

		public void AddTourReservation(DateTime time, Visitor visitor)
		{
			var tour = _tours.Where(t => t.StartTime == time).FirstOrDefault();
			tour.AddReservation(visitor);
			_registrationService.AddTourReservation(visitor.Id);
		}

		public void RemoveTourReservation(DateTime time, Visitor visitor)
		{
			var tour = _tours.Where(t => t.StartTime == time).FirstOrDefault();
			tour.RemoveReservation(visitor);
			_registrationService.RemoveTourReservation(visitor.Id);
		}

		public void AddTourAdmission(DateTime time, Visitor visitor)
		{
			var tour = _tours.Where(t => t.StartTime == time).FirstOrDefault();
			tour.AddAdmission(visitor);
			_registrationService.RemoveTourReservation(visitor.Id);
		}

		public void RemoveTourAdmission(DateTime time, Visitor visitor)
		{
			var tour = _tours.Where(t => t.StartTime == time).FirstOrDefault();
			tour.RemoveAdmission(visitor);
			_registrationService.RemoveTourReservation(visitor.Id);
		}


	}
}
