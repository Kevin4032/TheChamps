using HetDepot.People;
using HetDepot.People.Model;
using HetDepot.Tours.Model;
using HetDepot.Views;
using HetDepot.Views.Interface;
using HetDepot.Views.Parts;




namespace HetDepot.Controllers.General;

public class GuideController : Controller
{
    public override void Execute()
    {
        string personnelCode = (new InputView(
            Program.SettingService.GetConsoleText("guideTitle"),
            Program.SettingService.GetConsoleText("guidePersonnelNumber"))
        ).ShowAndGetResult();

        if (personnelCode == "Q" || personnelCode == "q")
        {
            NextController = new ShowToursController();
            return;
        }

        // Check Guide ID (password). From the exampleGuide.json, it is D0000000002

        Guide? guide = Program.PeopleService.GetGuide()!;
        bool isGuide = guide != null && personnelCode == guide.Id;



        if (isGuide)
        {
            NextController = new GuideShowAndSelectTourController();
            return;
        }
        else
        {
            NextController = new EmployeeInvalidLoginController();
        }

    }
}
