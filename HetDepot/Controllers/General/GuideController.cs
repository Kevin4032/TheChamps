using HetDepot.Tours.Model;
using HetDepot.Views;
using HetDepot.Views.Interface;
using HetDepot.Views.Parts;
using HetDepot.People;
using HetDepot.People.Model;




namespace HetDepot.Controllers.General;

public class GuideController : Controller
{
    public override void Execute()
    {
        string personnelCode = (new InputView(
            Program.SettingService.GetConsoleText("consoleGuideTitle"),
            Program.SettingService.GetConsoleText("consoleGuidePersonnelNumber"))
        ).ShowAndGetResult();

        //check Guide ID (password). From the exampleGuide.json, it is D0000000002
        //instance of Peopleservice:


 
        Guide guide = Program.PeopleService.GetGuide();
        bool isGuide = personnelCode == guide.Id;


        if (isGuide) // TODO Check if personnelCode is valid
        {
            // TODO: Create Tour overview
            //(new AlertView("TODO: Create Tour overview", AlertView.Info)).Show();
            NextController = new GuideShowAndSelectTourController();
            return;
        }
        else if (isGuide == false)
        {
            NextController = new StaffInvalidLoginController();
        }

    }
}
