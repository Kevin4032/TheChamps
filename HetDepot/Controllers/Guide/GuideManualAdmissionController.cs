//GuideStartTourAdmissionController.cs
using System.Diagnostics.Metrics;
using System.Media;
using HetDepot.People.Model;
using HetDepot.Tours;
using HetDepot.Tours.Model;
using HetDepot.Views;
using HetDepot.Views.Interface;
using HetDepot.Views.Parts;


namespace HetDepot.Controllers;

class GuideManualAdmissionController : Controller
{
    private Tour _tour;

    public GuideManualAdmissionController(Tour tour) : base()
    {
        _tour = tour;
    }


    public override void Execute()
    {
        string title = Program.SettingService.GetConsoleText("guideTourManuallyCreateReservation");
        string body = Program.SettingService.GetConsoleText("visitorEnterCode");
        string goBack = Program.SettingService.GetConsoleText("goBack");
        string message_green = Program.SettingService.GetConsoleText("guideManualAdmissionSuccess");
        string message_red = Program.SettingService.GetConsoleText("GuideManualAdmissionInvalid");



        var personIDToVerify = string.Empty;
        


        personIDToVerify = new InputView(title, body+goBack).ShowAndGetResult();
        
        if (personIDToVerify == "Q" || personIDToVerify == "q")
        {
            NextController = new GuideStartTourAdmissionController(_tour);
            return;
        } 
        Visitor verified_ID = Program.PeopleService.GetVisitorById(personIDToVerify);
        string message_problem_a;
        string message_problem_b;
        string message_problem_c;
        try
        {
            verified_ID = Program.PeopleService.GetVisitorById(personIDToVerify);
            return;
        }
        catch (System.Exception)
        {
            message_problem_a = Program.SettingService.GetConsoleText("guideAdmissionCodeNotValid");
            message_problem_b = Program.SettingService.GetConsoleText("visitorLogonCodeInvalid");
            message_problem_c = message_problem_a + ". " + message_problem_b;
            new AlertView(message_problem_c.ToString(), ConsoleColor.Red).Show();
            //Nogmaals proberen met deze controller:
            NextController = this;
        }


        if (verified_ID == null)
        {
            try
            {
                
                new AlertView(message_red, ConsoleColor.Red).Show();
                //Nogmaals proberen met deze controller:
                NextController = this;
            }
            catch (System.Exception)
            {


            }

            return;
        }

        if (verified_ID != null)
        {
            try
            {
                Program.TourService.AddTourAdmission(_tour,verified_ID);
                new AlertView(message_green, ConsoleColor.Green).Show();
                //Doorgaan met volgende aanmelding:
                NextController = new GuideShowAndSelectTourController();
            }
            catch (System.Exception)
            {
            }

            return;
        }

        {



        }


    }


}
