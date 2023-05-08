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
        List<ListableItem<List<Tour>>> tourList = new();

        foreach (var tours in allTours)
        {
            if (tours.Count == 0)
                continue;

            tourList.Add(new ListViewItem<List<Tour>>(tours[0].StartTime.ToString("dd/MM/yyyy"), tours));
        }

        ListView<List<Tour>> daysOverview =
            new(Program.SettingService.GetConsoleText("managerSelectDayQuestion"),
                tourList,
                new List<ListableItem<List<Tour>>>()
                {
                    new ListViewExtraItem<List<Tour>, Controller>(
                        Program.SettingService.GetConsoleText("backToHome"),
                        () => new ShowToursController())
                });

        Controller? otherController;
        List<Tour>? selectedTours = daysOverview.ShowAndGetResult<Controller>(out otherController);
        NextController = otherController; // Alleen als extra optie gekozen is

        if(selectedTours != null)
            NextController = new ManagerTypeQuestion(selectedTours);
    }
}
