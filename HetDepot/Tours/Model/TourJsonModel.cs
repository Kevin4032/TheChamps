using HetDepot.People.Model;
using HetDepot.Views.Parts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HetDepot.Tours.Model
{
	public class TourJsonModel
	{
		public long Id { get; private set; }
		public DateTime StartTime { get; set; }
		public Guide? Guide { get; set; }
		public List<Visitor>? Reservations { get; set; } // TODO REPLACE WITH STORAGE RESERVATION
		public List<Visitor>? Admissions { get; set; }
		public int MaxReservations { get; set; } // TODO REPLACE WITH SETTING

	}
}
