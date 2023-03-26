using HetDepot.People;
using HetDepot.People.Model;
using HetDepot.Settings;
using HetDepot.Tours;
using HetDepot.Tours.Model;

namespace HetDepot.Controllers
{
	public class CreateReservationController : Controller
	{
		private Visitor _visitor;
		private Tour _tour;

		public CreateReservationController(Visitor visitor, Tour tour) : base()
		{
			_visitor = visitor;
			_tour = tour;
		}

		public override void Execute()
		{
			//"E0000000009"
			//"2023-03-18T11:00:00.0000000+01:00"
			//var visitor = _peopleService.GetVisitorById(_visitor.Id);
			_tourService.AddTourReservation(_tour, _visitor);
			
			foreach (var bliebla in _tour.Reservations)
			{
				Console.WriteLine($"In de tour zit: {bliebla.Id}");
			}

			NextController = new ShowToursController();

		}
	}
}
