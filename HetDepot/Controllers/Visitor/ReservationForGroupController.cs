﻿using HetDepot.People.Model;
using HetDepot.Tours.Model;
using HetDepot.Views;
using HetDepot.Views.Parts;

namespace HetDepot.Controllers;

public class ReservationForGroupController : Controller
{
    private Tour _tour;

    public ReservationForGroupController(Tour tour) : base()
    {
        _tour = tour;
    }

    public override void Execute()
    {
        // Update tour to have the latest data
        _tour = Program.TourService.GetTourByStartTime(_tour.StartTime);

        int freeTourSpaces = _tour.FreeSpaces();

        if (freeTourSpaces <= 0)
        {
            NextController = new ShowToursController();
            return;
        }

        var groupReservationQuestion =
            new ListView(
                Program.SettingService.GetConsoleText("consoleVisitorReservationForGroupQuestion"),
                Program.SettingService.GetConsoleText("consoleVisitorReservationForGroupSubquestion", new() {
                    ["FreeSpaces"] = freeTourSpaces.ToString(),
                }),
                new List<ListableItem>()
                {
                    new ListViewItem(new List<ListViewItemPart>()
                    {
                        new ("Nee", 10)
                    }, false, false, 1),
                    new ListViewItem(new List<ListViewItemPart>()
                    {
                        new ("Ja", 10)
                    }, true, false, 1),
                }
            );
        bool anotherReservation = (bool)groupReservationQuestion.ShowAndGetResult();

        if (!anotherReservation)
        {
            NextController = new ShowToursController();
            return;
        }

        NextController = new RequestAuthenticationController(_tour, true);
    }
}
