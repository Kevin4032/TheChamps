using HetDepot.Controllers.Manager;
using HetDepot.Views;

namespace HetDepot.Controllers.General;

public class ManagerController : Controller
{
    public override void Execute()
    {
        string personnelCode = (new InputView(
            Program.SettingService.GetConsoleText("consoleEnterPersonnelCode"),
            Program.SettingService.GetConsoleText("guidePersonnelNumber"))
        ).ShowAndGetResult();

        if (Program.PeopleService.GetManager().Equals(new People.Model.Manager(personnelCode)))
        {
            NextController = new ManagerPeriodQuestion();
        }
        else
        {
            NextController = new EmployeeInvalidLoginController();

        }
    }
}
