using HetDepot.Tours;
using HetDepot.Tours.Model;
using HetDepot.Views;
using HetDepot.Views.Parts;

namespace HetDepot.Controllers.Manager;

public class ManagerWeeksOverview : Controller
{
    public override void Execute()
    {
        var allTours = Program.TourService.GetAllTours();
        List<ListableItem<List<List<Tour>>>> weekList = new();

        var weeks = allTours.GroupBy(t => t[0].getYearAndWeek()).ToList();

        foreach (var week in weeks)
        {
            List<List<Tour>> tours = week.ToList();

            weekList.Add(new ListViewItem<List<List<Tour>>>(tours[0][0].getYearAndWeek(), tours));
        }

        weekList.Add(new ListViewExtraItem<List<List<Tour>>, Controller>(
            Program.SettingService.GetConsoleText("back"),
            () => new ManagerPeriodQuestion())
        );

        ListView<List<List<Tour>>> weeksOverview = new(
            Program.SettingService.GetConsoleText("managerSelectWeekQuestion"),
            weekList
        );

        Controller? otherController;
        List<List<Tour>>? selectedWeek = weeksOverview.ShowAndGetResult<Controller>(out otherController);
        NextController = otherController; // Alleen als extra optie gekozen is

        if (otherController == null && selectedWeek != null)
            NextController = new ManagerWeekAnalysis(selectedWeek);

    }
}
