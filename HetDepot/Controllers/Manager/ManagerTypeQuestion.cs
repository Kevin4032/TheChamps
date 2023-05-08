using HetDepot.Tours.Model;
using HetDepot.Views;
using HetDepot.Views.Parts;

namespace HetDepot.Controllers.Manager;

public class ManagerTypeQuestion : Controller
{
    private readonly List<Tour> tours;

    public ManagerTypeQuestion(List<Tour> tours)
    {
        this.tours = tours;
    }

    public override void Execute()  
    {
        var typeQuestion = new ListView(Program.SettingService.GetConsoleText("managerTypeQuestion"), new List<ListableItem>()
        {
            new ListViewItem(Program.SettingService.GetConsoleText("reservations"), 0),
            new ListViewItem(Program.SettingService.GetConsoleText("admissions"), 1),
            new ListViewItem(Program.SettingService.GetConsoleText("backToOverview"), "back"),
        });
        var res = typeQuestion.ShowAndGetResult();

        if (res == "back")
        {
            NextController = new ManagerDaysOverview();
            return;
        }

        NextController = new ManagerDayOccupancyOverview(tours, (int)res);
    }
}