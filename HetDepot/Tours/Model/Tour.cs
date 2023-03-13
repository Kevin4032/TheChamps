using HetDepot.People.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HetDepot.Tours.Model
{
	public class Tour
	{
		public Tour(DateTime startTime) => StartTime = startTime;
		public Tour(DateTime startTime, int reservations): this(startTime) => Reservations = reservations;

		public long Id { get; private set; }
		public DateTime StartTime { get; private set; }
		public Guide Guide { get; private set; }
		public int Reservations = 0;
    	public static int MaxReservations = 13;
		
		public string GetTime() => StartTime.ToString("H:mm");
    	public override string ToString() => GetTime();
	}
}
