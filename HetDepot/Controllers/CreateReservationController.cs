using HetDepot.People;
using HetDepot.People.Model;
using HetDepot.Settings;
using HetDepot.Tours;
using HetDepot.Views;
using HetDepot.Views.Parts;

namespace HetDepot.Controllers
{
	public class CreateReservationController : Controller
	{
		private TourService _tourService;
		private PeopleService _peopleService;
		private SettingService _settingService;

		public CreateReservationController(
			TourService tourService
		,	PeopleService peopleService
		,	SettingService settingService
		)
		{
			_tourService = tourService;
			_peopleService = peopleService;
			_settingService = settingService;
			NextController = new RequestAuthenticationController();
		}

		public override void Execute()
		{
			
			var visitorZonderReservering = _peopleService.GetVisitorById("E0000000009"); // Waar gaat deze vandaan komen
			var visitorMetReservering = _peopleService.GetVisitorById("E1234567890"); // Waar gaat deze vandaan komen
			var tourtje = DateTime.Parse("2023-03-18T11:00:00.0000000+01:00"); // Waar gaat deze vandaan komen

			//Extra check
			var tourEgt = _tourService.Tours.Where(t => t.StartTime == tourtje).FirstOrDefault();

			Console.WriteLine($"Controller: CreateReservationController - visitorzonder {visitorZonderReservering.Id}");
			Console.WriteLine($"Controller: CreateReservationController - visitormet {visitorMetReservering.Id}");
			Console.WriteLine($"Controller: CreateReservationController - tourtje (dataum) {tourtje}");
			Console.WriteLine($"Controller: CreateReservationController - tour ogpehad {tourEgt.StartTime}");
			Console.WriteLine($"Controller: CreateReservationController - visitor heeft tour? {visitorMetReservering.Id} - {visitorMetReservering.Tour?.StartTime}");
			Console.WriteLine($"Controller: CreateReservationController - visitor heeft tour? {visitorZonderReservering.Id} - {visitorZonderReservering.Tour?.StartTime}");
			//Console.WriteLine($"Controller: CreateReservationController - ");
			//Console.WriteLine($"Controller: CreateReservationController - ");

			//foreach (var bliebla in tourEgt.Reservations)
			//{
			//	Console.WriteLine($"In de tour zit: {bliebla.Id}");
			//}

			var tourtonen = new List<ListableItem>() { tourEgt.ToListableItem() };

			var tonenVan = new ListView("tour rserveren", tourtonen );
			tonenVan.ShowAndGetResult();

		}
	}
}
