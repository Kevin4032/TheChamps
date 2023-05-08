﻿using HetDepot.People.Model;
using HetDepot.Tours.Model;
using HetDepot.Views;

namespace HetDepot.Controllers
{
	public class ReservationCreateController : Controller
	{
		private Visitor _visitor;
		private Tour _tour;

		public ReservationCreateController(Tour tour, Visitor visitor) : base()
		{
			_tour = tour;
			_visitor = visitor;
		}

		public override void Execute()
		{
			Program.TourService.AddTourReservation(_tour, _visitor);

			var message = Program.SettingService.GetConsoleText("consoleVisitorReservationConfirmation", new() {
				["time"] = _tour.StartTime.ToString(),
			});

			new AlertView(message, AlertView.Info).Show();

			NextController = new ReservationForGroupController(_tour);
		}
	}
}