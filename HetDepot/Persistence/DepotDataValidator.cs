using HetDepot.People.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HetDepot.Persistence
{
	public class DepotDataValidator : IDepotDataValidator
	{
		private Regex _employeeCheckId;
		private Regex _visitorCheckId;

		public DepotDataValidator()
        {
			_employeeCheckId = new Regex(@"^[dD]\d{10}");
			_visitorCheckId = new Regex(@"^[eE]\d{10}");
		}

		public bool ValidForAdministration<T>(T dataToValidate) where T : Person
		{
			var validVisitorId = _visitorCheckId.IsMatch(dataToValidate.Id);
			var validEmployeeId = _employeeCheckId.IsMatch(dataToValidate.Id);

			var dataIsGuide = dataToValidate is Guide;
			var dataIsManager = dataToValidate is Manager;
			var dataIsVisitor = dataToValidate is Visitor;

			if ((dataIsGuide && validEmployeeId) || (dataIsManager && validEmployeeId) || (dataIsVisitor && validVisitorId))
			{
				return true;
			}

			return false;
		}
	}
}
