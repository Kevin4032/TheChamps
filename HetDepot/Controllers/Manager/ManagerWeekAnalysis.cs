// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HetDepot.Tours.Model;
using HetDepot.Views;
using HetDepot.Views.Parts;

namespace HetDepot.Controllers.Manager;

public class ManagerWeekAnalysis : Controller
{
    private readonly List<List<Tour>> _toursOfWeek;
    private readonly int minimalReservations;

    private const string RemoveFlag = "remove";
    private const string AddFlag = "add";
    private const int FlagsForRecommendation = 2;

    public ManagerWeekAnalysis(List<List<Tour>> toursOfWeek)
    {
        _toursOfWeek = toursOfWeek;
        minimalReservations = Program.SettingService.GetMaxTourReservations() / 2;
    }

    public override void Execute()
    {
        /*
         *  When the reservations are less then half (13 / 2 = 7) flag for removal
         *  When the reservations are at max (13) for at least 2 tours flag for more tours
         * in range from 1st full tour to last
         *
         *  If the the flags occur more than 2 times in a week give the recommendation for the flags
         *
         */

        Dictionary<TimeOnly, List<string>> flags = new();

        // Add the flags
        foreach (var weekTours in _toursOfWeek)
        {
            foreach (var tour in weekTours)
            {
                int reservationsForTour = tour.Reservations.Count;
                TimeOnly flagKey = TimeOnly.FromDateTime(tour.StartTime);
                flags.TryAdd(flagKey, new List<string>());

                if (reservationsForTour < minimalReservations)
                {
                    flags[flagKey].Add(RemoveFlag);
                }

                if (reservationsForTour == Program.SettingService.GetMaxTourReservations())
                {
                    flags[flagKey].Add(AddFlag);
                }
            }
        }

        var recommendations = new List<ListableItem<string>>();

        foreach (var flaggedTime in flags)
        {
            int removeFlags = flaggedTime.Value.Where(flag => flag == RemoveFlag).Count();
            int addFlags = flaggedTime.Value.Where(flag => flag == AddFlag).Count();

            if (removeFlags >= FlagsForRecommendation)
            {
                recommendations.Add(new ListViewItem<string>(
                    new List<ListViewItemPart>()
                    {
                        new(flaggedTime.Key.ToShortTimeString(), 8),
                        new("Rondleiding annuleren, lage bezettingsgraad")
                    }, ""));
                continue;
            }

            if (addFlags >= FlagsForRecommendation)
            {
                recommendations.Add(new ListViewItem<string>(
                    new List<ListViewItemPart>()
                    {
                        new(flaggedTime.Key.ToShortTimeString(), 8),
                        new("Rondleiding toevoegen rond deze tijd, hoge bezettingsgraad")
                    }, ""));
                continue;
            }

            recommendations.Add(new ListViewItem<string>(
                new List<ListViewItemPart>()
                {
                    new(flaggedTime.Key.ToShortTimeString(), 8), new("--"),
                }, ""));
        }

        ListView<string> weekAnalysis =
            new("Analyse " + _toursOfWeek[0][0].getYearAndWeek() + ", voorstellen per tijd op basis van bezettingsgraad",
                recommendations);
        weekAnalysis.Show();

        NextController = new ManagerWeeksOverview();
    }
}
