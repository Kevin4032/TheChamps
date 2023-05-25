// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HetDepot.People.Model;
using HetDepot.Tours.Model;
using HetDepot.Views;

namespace HetDepot.Controllers;

public class CancelReservationController : Controller
{
    private Visitor _visitor = default!;

    public override void Execute()
    {

        string visitorCode = (new InputView("Reservering annuleren", "Vul uw code in:")).ShowAndGetResult();

        try
        {
            _visitor = Program.PeopleService.GetVisitorById(visitorCode);
        }
        catch (NullReferenceException)
        {
            // Visitor not found
            (new AlertView("Code ongeldig", AlertView.Error)).Show();
            NextController = new ShowToursController();
            return;
        }

        if (!Program.TourService.HasReservation(_visitor))
        {
            (new AlertView("Code ongeldig", AlertView.Error)).Show();
            NextController = new ShowToursController();
            return;
        }

        Tour tour = Program.TourService.GetReservation(_visitor)!; // Can't be null, or "HasReservation" above would have returned false 

        if (tour.StartedAt != null)
        {
            (new AlertView("Tour is al gestart reservering kan niet geannuleerd worden", AlertView.Info)).Show();
            return;
        }

        Program.TourService.RemoveTourReservation(tour, _visitor);
        (new AlertView("Reservering geannuleerd", AlertView.Info)).Show();
    }
}
