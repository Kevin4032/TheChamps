using HetDepot.People.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HetDepot.Views;
using HetDepot.Views.Interface;
using HetDepot.Views.Parts;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace HetDepot.Tours.Model
{
    public class Tour : IListableObject
    {
        private List<Visitor> _reservations;
        private List<Visitor> _admissions;

        [JsonConstructor]
        public Tour()
        {

        }

        public Tour(DateTime startTime)
        {
            StartTime = startTime;
            _reservations = new List<Visitor>();
            _admissions = new List<Visitor>();
        }

		public Tour(DateTime startTime, Guide guide, int maxReservations)
		{
			StartTime = startTime;
			_reservations = new List<Visitor>();
			_admissions = new List<Visitor>();
            MaxReservations = maxReservations;
            Guide = guide;
		}

		public Tour(DateTime startTime, int maxReservations) : this(startTime)
        {
            _reservations = new List<Visitor>();
			_admissions = new List<Visitor>();
			MaxReservations = maxReservations;
		}

        public long Id { get; private set; }
        public DateTime StartTime { get; private set; }
        public Guide Guide { get; private set; }
        public ReadOnlyCollection<Visitor> Reservations
        { 
            get { return _reservations.AsReadOnly(); }
        } // TODO REPLACE WITH STORAGE RESERVATIONS

        public ReadOnlyCollection<Visitor> Admissions
        {
            get { return _admissions.AsReadOnly(); }
        }

        public static int MaxReservations { get; private set; } // TODO REPLACE WITH SETTING

        public string GetTime() => StartTime.ToString("H:mm");
        public int FreeSpaces() => Math.Max(0, MaxReservations - Reservations.Count);
        public override string ToString() => GetTime();

        public bool AddReservation(Visitor visitor)
        {
            _reservations.Add(visitor);
            return true;
        }

        public bool RemoveReservation(Visitor visitor)
        { 
            return _reservations.Remove(visitor);
        }

        public bool AddAdmission(Visitor visitor)
        {
            _admissions.Add(visitor);
            return true;
        }

        public bool RemoveAdmission(Visitor visitor)
        {
            return _admissions.Remove(visitor);
        }

        public ListableItem ToListableItem()
        {
            /*
             * Geef een ListViewPartedItem terug met tijd en aantal plaatsen
             * Wanneer er geen vrije plaatsen zijn zet Disabled op true. Dit zorgt ervoor dat de optie niet gekozen mag
             * worden.
             */
            return new ListViewItem(new List<ListViewItemPart>
                {
                    new (GetTime(), 10),
                    new (FreeSpaces() > 0 ? FreeSpaces() + " plaatsen" : "Vol")
                },
                this, FreeSpaces() <= 0);
        }
    }
}