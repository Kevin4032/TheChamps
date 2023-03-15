using HetDepot.People.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HetDepot.Views;
using HetDepot.Views.Interface;
using HetDepot.Views.Parts;

namespace HetDepot.Tours.Model
{
    public class Tour : IListableObject
    {
        public Tour(DateTime startTime) => StartTime = startTime;
        public Tour(DateTime startTime, int reservations) : this(startTime) => Reservations = reservations;

        public long Id { get; private set; }
        public DateTime StartTime { get; private set; }
        public Guide Guide { get; private set; }
        public int Reservations = 0; // TODO REPLACE WITH STORAGE RESERVATIONS
        public static int MaxReservations = 13; // TODO REPLACE WITH SETTING

        public string GetTime() => StartTime.ToString("H:mm");
        public int FreeSpaces() => Math.Max(0, MaxReservations - Reservations);
        public override string ToString() => GetTime();

        public ListableItem ToListableItem()
        {
            /*
             * Geef een ListViewPartedItem terug met tijd en aantal plaatsen
             * Wanneer er geen vrije plaatsen zijn zet Disabled op true. Dit zorgt ervoor dat de optie niet gekozen mag
             * worden.
             */
            return new ListViewPartedItem(new List<ListViewItemPart>
                {
                    new (GetTime(), 10),
                    new (FreeSpaces() > 0 ? FreeSpaces() + " plaatsen" : "Vol")
                },
                this, FreeSpaces() <= 0);
        }
    }
}