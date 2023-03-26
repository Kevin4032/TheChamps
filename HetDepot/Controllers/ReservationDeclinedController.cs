using HetDepot.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HetDepot.Controllers
{
	public class ReservationDeclinedController : Controller
	{
		public ReservationDeclinedController() : base() 
		{ 
		}

		public override void Execute()
		{
			var message = _settingService.GetConsoleText("consoleVisitorAlreadyHasTourAdmission");

			new AlertView(message, AlertView.Info).Show();

			NextController = new ShowToursController();
		}
	}
}
