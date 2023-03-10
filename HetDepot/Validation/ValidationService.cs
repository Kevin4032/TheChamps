using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;
using HetDepot.People.Model;
using HetDepot.Settings;

namespace HetDepot.Validation
{
    public class ValidationService
	{
		private Regex _employeeCheckId;
		private Regex _visitorCheckId;
		private SettingService _settingService;

		public ValidationService(SettingService settingservice)
		{
			_employeeCheckId = new Regex(@"^[dD]\d{10}");
			_visitorCheckId = new Regex(@"^[eE]\d{10}");
			_settingService = settingservice;
		}

		public bool ValidForAdministration<T>(T dataToValidate) where T : Person
		{
			var validVisitorId = _visitorCheckId.IsMatch(dataToValidate.Id);
			var validEmployeeId = _employeeCheckId.IsMatch(dataToValidate.Id);

			var isValidId = validEmployeeId || validVisitorId;
			Console.WriteLine($"{dataToValidate.Id} - Valid: {isValidId}");

			Console.WriteLine($"Was dees: {dataToValidate.GetType()}");

			var dataIsGuide = dataToValidate is Guide;
			var dataIsManager = dataToValidate is Manager;
			var dataIsVisitor = dataToValidate is Visitor;

			if ((dataIsGuide && validEmployeeId) || (dataIsManager && validEmployeeId) || (dataIsVisitor && validVisitorId))
			{
				return true;
			}

			return false;
		}

		public ValidationResult IsValid<T>(T dataToValidate) where T : Person
		{
			var result = new ValidationResult();
			var validVisitorId = _visitorCheckId.IsMatch(dataToValidate.Id);
			var validEmployeeId = _employeeCheckId.IsMatch(dataToValidate.Id);

			var isValidId = validEmployeeId || validVisitorId;
			Console.WriteLine($"{dataToValidate.Id} - Valid: {isValidId}");

			Console.WriteLine($"Was dees: {dataToValidate.GetType()}");

			var dataIsGuide = dataToValidate is Guide;
			var dataIsManager = dataToValidate is Manager;
			var dataIsVisitor = dataToValidate is Visitor;

			if ((dataIsGuide && validEmployeeId) || (dataIsManager && validEmployeeId) || (dataIsVisitor && validVisitorId))
			{
				Console.WriteLine($"{dataToValidate.Id} - {dataToValidate.GetType()} - Valid");
				result.IsValidPeopleAdministration = true;
			}

			return result;
		}


		public ValidationResult IsValid(Person dataToValidate)
		{
			var result = new ValidationResult();
			var validVisitorId = _visitorCheckId.IsMatch(dataToValidate.Id);
			var validEmployeeId = _employeeCheckId.IsMatch(dataToValidate.Id);

			var isValidId = validEmployeeId || validVisitorId;
			Console.WriteLine($"{dataToValidate.Id} - Valid: {isValidId}");

			Console.WriteLine($"Was dees: {dataToValidate.GetType()}");


			var dataIsGuide = dataToValidate is Guide;
			var dataIsManager = dataToValidate is Manager;
			var dataIsVisitor = dataToValidate is Visitor;

			if (dataIsGuide && validEmployeeId)
			{
				Console.WriteLine($"{dataToValidate.Id} - {dataToValidate.GetType()} - Valid");
				result.IsValidPeopleAdministration = true;
			}

			_settingService.GetSettingValue("ditte");


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


			return result;

		}

		private bool IsValidFormat(string numberToValidate)
		{
			

			return true;
		}
	}
}
