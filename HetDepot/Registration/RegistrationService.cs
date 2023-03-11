using HetDepot.People.Model;
using HetDepot.Persistence;
using HetDepot.Registration.Model;

namespace HetDepot.Registration
{
	public class RegistrationService
	{
		private Reservation _reservation;
		private Admission _admission;
		private Repository _repository;

		public RegistrationService(Repository repository) 
		{
			_repository = repository;
			_reservation = _repository.GetReservations();
			_admission = _repository.GetAdmissions();
		}

		//TODO: verwijderen. Is voor ons team ter illustratie
		public void TestShowAllRegistrations()
		{
			foreach (var admission in _admission.Admissions)
			{
				Console.WriteLine($"Admission - {admission}");
			}
			foreach (var reservation in _reservation.Reservations)
			{
				Console.WriteLine($"Reservation - {reservation}");
			}
		}

		public void AddTourReservation(string visitor)
		{
			_reservation.Reservations.Add(visitor);
			_repository.Write(_reservation);
		}

		public void AddTourAdmission(string visitor)
		{
			_admission.Admissions.Add(visitor);
			_repository.Write(_admission);
		}

		public void RemoveTourReservation(string visitor)
		{
			_reservation.Reservations.Remove(visitor);
			_repository.Write(_reservation);
		}

		public bool HasTourAdmission(string visitor)
		{ 
			return _admission.Admissions.Contains(visitor);
		}

		public bool HasTourReservation(string visitor)
		{
			return _reservation.Reservations.Contains(visitor);
		}
	}
}
