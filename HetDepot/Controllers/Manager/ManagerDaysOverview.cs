using HetDepot.Tours;
using HetDepot.Tours.Model;
using HetDepot.Views;
using HetDepot.Views.Parts;

namespace HetDepot.Controllers.Manager;

public class ManagerDaysOverview : Controller
{
    public override void Execute()
    {
        var allTours = Program.TourService.GetAllTours().Where(tours => tours.Count > 0).ToList();
        var dayList = allTours.Select(
            dayToursList => new ListViewItem<List<Tour>>(dayToursList[0].StartTime.ToString("dd/MM/yyyy"), dayToursList)
        ).ToList<ListableItem<List<Tour>>>();

        dayList.Add(new ListViewExtraItem<List<Tour>, Controller>(Program.SettingService.GetConsoleText("backToHome"), () => new ShowToursController()));

        var daysOverview = new ListView<List<Tour>>(Program.SettingService.GetConsoleText("managerSelectDayQuestion"), dayList);

        Controller? otherController;
        List<Tour>? selectedTours = daysOverview.ShowAndGetResult<Controller>(out otherController);
        NextController = otherController; // Alleen als extra optie gekozen is

        if (selectedTours != null)
            NextController = new ManagerTypeQuestion(selectedTours);
    }
}
