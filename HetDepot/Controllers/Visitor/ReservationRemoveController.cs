using HetDepot.People.Model;
using HetDepot.Tours.Model;
using HetDepot.Views;
using HetDepot.Views.Parts;

namespace HetDepot.Controllers
{
	public class ReservationRemoveController : Controller
	{
		private Tour _tour;
		private Visitor _visitor;

		public ReservationRemoveController(Tour tour, Visitor visitor)
		{
			_tour = tour;
			_visitor = visitor;
		}

		public override void Execute()
		{
			NextController = new ShowToursController();

			var question = Program.SettingService.GetConsoleText("consoleVisitorReservationAlreadyHavingOne");
			
			ListView replaceCurrentQuestion = new ListView(question, new List<ListableItem>()
			{
				new ListViewItem("Ja", true),
				new ListViewItem("Nee", false),
			});
			bool cancelReservartion = (bool)replaceCurrentQuestion.ShowAndGetResult();

			if (!cancelReservartion)
			{
				var messageNotCancled = Program.SettingService
					.GetConsoleText("consoleVisitorReservationChangeNotChanges", new()
					{
						["time"] = _tour.GetTime(),
					});
				
				new AlertView(messageNotCancled, AlertView.Info).Show();

				return;
			}
			
			Program.TourService.RemoveTourReservation(_tour, _visitor);
			
			var messageCancled = Program.SettingService.GetConsoleText("consoleVisitorReservationCancellationConfirmation");

			new AlertView(messageCancled, AlertView.Info).Show();
		}
	}
}
