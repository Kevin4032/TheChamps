using HetDepot.People.Model;
using HetDepot.Persistence;
using HetDepot.Validation;

namespace HetDepot.Registration
{
	public class RegistrationService
	{
		private HashSet<string> _admissions;
		private HashSet<string> _reservations;

		public RegistrationService(Repository repository) 
		{ 
			_admissions = new HashSet<string>();
			_reservations = new HashSet<string>();
		}

		public void AddTourReservation(string visitor)
		{
			_reservations.Add(visitor);
		}

		public void AddTourAdmission(string visitor)
		{ 
			_admissions.Add(visitor);
		}

		public void RemoveTourReservation(string visitor)
		{ 
			_reservations.Remove(visitor);
		}

		public bool HasTourAdmission(string visitor)
		{ 
			return _admissions.Contains(visitor);
		}

		public bool HasTourReservation(string visitor)
		{
			return _reservations.Contains(visitor);
		}
	}
}
