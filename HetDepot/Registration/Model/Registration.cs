using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HetDepot.Registration.Model
{
	public class Registration
	{
		public Registration()
		{
			Admissions = new Admission();
			Reservations = new Reservation();
		}
		public Admission Admissions { get; set; }
		public Reservation Reservations { get; set; }
	}
}
