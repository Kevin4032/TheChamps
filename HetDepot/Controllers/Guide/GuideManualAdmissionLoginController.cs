using HetDepot.People.Model;
using HetDepot.Tours.Model;
using HetDepot.Views;

namespace HetDepot.Controllers.General;

public class GuideManualAdmissionLoginController : Controller
{
    private Tour _tour;

    public GuideManualAdmissionLoginController(Tour tour) : base()
    {
        _tour = tour;
    }


    public override void Execute()
    {
        string personnelCode = (new InputView(
            Program.SettingService.GetConsoleText("guideTitle"),
            Program.SettingService.GetConsoleText("guidePersonnelNumber"))
        ).ShowAndGetResult();

        // Check Guide ID (password). From the exampleGuide.json, it is D0000000002

        Guide? guide = Program.PeopleService.GetGuide()!;
        bool isGuide = guide != null && personnelCode == guide.Id;


        if (isGuide)
        {
            NextController = new GuideManualAdmissionController(_tour);
            return;
        }
        else
        {
            NextController = new EmployeeInvalidLoginController();
        }

    }
}
