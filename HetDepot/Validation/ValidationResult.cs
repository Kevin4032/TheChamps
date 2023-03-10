using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HetDepot.Validation
{
	public class ValidationResult
	{
		public ValidationResult() { }

		public bool IsSuccess { get; set; }
		public bool IsValidForTourAdmission { get; set; }
		public bool IsValidForTourReservation { get; set; }
		public string Message { get; set; }
		//public Person PersonType { get; set; }

	}
}
