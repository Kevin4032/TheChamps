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
        // var typeQuestion = new ListView<int>(
        //     Program.SettingService.GetConsoleText("managerTypeQuestion"), new List<ListableItem>()
        // {
        //     new ListViewItem(Program.SettingService.GetConsoleText("reservations"), 0),
        //     new ListViewItem(Program.SettingService.GetConsoleText("admissions"), 1),
        //     new ListViewItem(Program.SettingService.GetConsoleText("backToOverview"), "back"),
        // });


        var typeQuestion = new ListView<int>(Program.SettingService.GetConsoleText("managerTypeQuestion"), new()
        {
            new ListViewItem<int>(Program.SettingService.GetConsoleText("reservations"), 0),
            new ListViewItem<int>(Program.SettingService.GetConsoleText("admissions"), 1),
            new ListViewExtraItem<int, Controller>(Program.SettingService.GetConsoleText("backToOverview"), () => new ManagerDaysOverview()),
        });

        Controller? otherController;
        int? type = typeQuestion.ShowAndGetResult<Controller>(out otherController);
        NextController = otherController; // Alleen als extra optie gekozen is

        if (otherController == null)
            NextController = new ManagerDayOccupancyOverview(tours, (int)type);
    }
}
