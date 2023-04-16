using HetDepot.Tours.Model;
using HetDepot.Views.Interface;
using HetDepot.Views;

namespace HetDepot.Controllers
{
	public class ShowToursController : Controller
	{
		public ShowToursController() : base()
		{
		}

		public override void Execute()
		{
			var tours = _tourService.Tours;

			//TODO: Opmerking Kevin: Als alle rondleidingen vol zitten, 'hangt' de interface
			ListView tourOverviewVisitorWithInterface = new ListView("Welkom bij het depot", tours.ToList<IListableObject>());
			Tour selectedTour = (Tour)tourOverviewVisitorWithInterface.ShowAndGetResult();

			NextController = new RequestAuthenticationController(selectedTour);
		}
	}
}
