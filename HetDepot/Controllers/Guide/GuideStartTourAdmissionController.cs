//GuideStartTourAdmissionController.cs
using HetDepot.People.Model;
using HetDepot.Tours.Model;
using HetDepot.Views;
using HetDepot.Views.Interface;
using HetDepot.Views.Parts;


namespace HetDepot.Controllers;

class GuideStartTourAdmissionController : Controller
{
    private Tour _tour;

    public GuideStartTourAdmissionController(Tour tour) : base()
    {
        _tour = tour;
    }
    

    public override void Execute()
    {
        // Willen wij niet het aantal aanmeldingen in de titel laten zien? Dat kan met deze code:
        string title = Program.SettingService.GetConsoleText("ConsoleGuideTourAdmissionTitle");
        string message = Program.SettingService.GetConsoleText("consoleGuideTourAdmissionEnterVisitorCode");

        string countZero = Program.SettingService.GetConsoleText("consoleGuideTourAdmissionCountZero");
        string countOne = Program.SettingService.GetConsoleText("consoleGuideTourAdmissionCountOne");
        string countMulA = Program.SettingService.GetConsoleText("consoleGuideTourAdmissionCountMultiple");
        string countMul = $"{_tour.Admissions.Count} {countMulA}";
        //string countMul = Program.SettingService.GetConsoleText("consoleGuideTourAdmissionCountMultiple");

        var personIDToVerify = string.Empty;
        
        
        if (_tour.Admissions.Count == 0)
        {
            personIDToVerify = new InputView(countZero, message).ShowAndGetResult();
        }
        
        else if (_tour.Admissions.Count == 1)
        {
            personIDToVerify = new InputView(countOne, message).ShowAndGetResult();
        }
        else
        {
            personIDToVerify = new InputView(countMul, message).ShowAndGetResult();
        }
        // var personIDToVerify = new InputView($"{_tour.Admissions.Count} bezoekers hebben zich aangemeld.", message).ShowAndGetResult();
        //Print: Aanmelden voor deze reservering. Voer jouw unieke code in:

        //var AdmissionOptions = new List<string> {title,message};
    
        //var personIDToVerify = new InputView(title,message).ShowAndGetResult();
        

         
        
        if (_tour.Reservations.Count == _tour.MaxReservations || personIDToVerify == "s" || personIDToVerify == "S")
        {
            /*if maxreservations reached or if user chooses start tour anyway while not everyone checked in, 
            next controller default voor volgende tour of bezoeker aanmelding.
            TODO: Remove tour from list? */
            //start the tour:
            var message_Tour_Starts = Program.SettingService.GetConsoleText("consoleGuideTourAllReservationsValidated");
            new AlertView(message_Tour_Starts, ConsoleColor.Blue).Show();
            NextController = new ShowToursController();

        }
        //check of PersonIDToverify een reservering heeft:
        else if (Program.TourService.HasReservation(new Visitor(personIDToVerify)))
        {
            _tour.AddAdmission(new Visitor(personIDToVerify));
            //TODO: voer piepgeluid uit
            //Moet deze reservering gemarkeerd worden als gebruikt?
            // Aanmelden voor deze rondleiding
            var message_success = Program.SettingService.GetConsoleText("consoleGuideAdmissionCodeValid");

            //var message = _settingService.GetConsoleText("");
            // in de toekomst vervangen door: var message = _settingService.GetConsoleText("DeCodeIsOngeldig");

            // Gids ziet pop-up met de melding hierboven. Keert terug naar nieuwe instance van deze controller
            new AlertView(message_success, ConsoleColor.Green).Show();
            //Doorgaan met volgende aanmelding:
            NextController = this;

        }
        //Als code niet geldig is:
        else if (Program.PeopleService.GetVisitorById == null)     
        {
            //print: Je bent niet aangemeld. De code is niet geldig. Controleer uw code en probeer het nog eens

            var message_problem = Program.SettingService.GetConsoleText("consoleGuideAdmissionCodeNotValid" + " " +"consoleVisitorLogonCodeInvalid");
            new AlertView(message, ConsoleColor.Red).Show();
            //Doorgaan met volgende aanmelding:
            NextController = this;
        }
        else 
        //Als niet alles afgevangen kan worden door de twee hierboven, bijvoorbeeld als code al gebruikt is, of code is geldig maar heeft geen 
        //Reservering, dan moet hieronder de reden aangepast worden.
        //Of we kunnen ervoor kiezen om de geldige code hierna naar de controller 'Persoon handmatig toevoegen' te sturen.
        {
            string message_problem_a = Program.SettingService.GetConsoleText("consoleGuideAdmissionCodeNotValid");
            string message_problem_b =  Program.SettingService.GetConsoleText("consoleVisitorLogonCodeInvalid");
            string message_problem_c = message_problem_a + message_problem_b;
            new AlertView(message_problem_c.ToString(), ConsoleColor.Red).Show();
            //Doorgaan met volgende aanmelding:
            NextController = this;
        }

        

        
    }


}
