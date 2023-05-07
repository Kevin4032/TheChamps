using HetDepot.Tours.Model;
using HetDepot.Views;
using HetDepot.Views.Interface;
using HetDepot.Views.Parts;

namespace HetDepot.Controllers.General;

public class ManagerController : Controller
{
    public override void Execute()
    {
        string personnelCode = (new InputView(
            Program.SettingService.GetConsoleText("consoleEnterPersonnelCode"),
            Program.SettingService.GetConsoleText("consoleGuidePersonnelNumber"))
        ).ShowAndGetResult();
        
        if (true) // TODO Check if personnelCode is valid
        {
            
            
            
        }
    }
}