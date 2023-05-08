using HetDepot.Tours;
using HetDepot.Tours.Model;
using HetDepot.Views;
using HetDepot.Views.Parts;

namespace HetDepot.Controllers.Manager;

public class ManagerDaysOverview : Controller
{
    public override void Execute()
    {
        var allTours = Program.TourService.GetAllTours();
        List<ListableItem> tourList = new List<ListableItem>();

        foreach (var tours in allTours)
        {
            if (tours.Count == 0)
                continue;

            tourList.Add(new ListViewItem(tours[0].StartTime.ToString("dd/MM/yyyy"), tours));
        }

        tourList.Add(new ListViewItem(Program.SettingService.GetConsoleText("backToHome"), "back"));

        ListView daysOverview = new ListView(Program.SettingService.GetConsoleText("managerSelectDayQuestion"), tourList);
        var selectedTours = daysOverview.ShowAndGetResult();

        if (selectedTours == "back")
        {
            return;
        }

        NextController = new ManagerTypeQuestion((List<Tour>)selectedTours);

    }
}