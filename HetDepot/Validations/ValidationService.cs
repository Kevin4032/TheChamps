using HetDepot.People.Model;
using HetDepot.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HetDepot.Validations
{
	public class ValidationService
	{
		private RegistrationService _registrationService;
		public ValidationService(RegistrationService registrationService) 
		{ 
			_registrationService = registrationService;
		}

		public bool VisitorAllowedToMakeReservation(Visitor visitor)
		{
			return !_registrationService.HasTourAdmission(visitor.Id);
	
		}

	}
}
