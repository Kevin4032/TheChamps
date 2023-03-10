using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HetDepot.Validation
{
	public class ValidationService
	{
		public ValidationService() { }

		public ValidationResult IsValid()
		{
			/*
			1. Begint met E of D, gevolgd door 10 cijfers 
			2. Een bezoeker mag aanmelden voor 1 rondleiding per dag.
			3. Uiterlijk de dag voor bezoek online een entreebewijs kopen.
			4. Heeft reservering
			5. Een bezoeker moet uiterlijk de dag voor bezoek online een entreebewijs kopen

			a. Console – Aanmelden
			b. Gids - Aanmelden reserveringen
			*/


			// Vergelijken met tourData


			return new ValidationResult();

		}

		private bool IsValidFormat(string numberToValidate)
		{
			

			return true;
		}
	}
}
