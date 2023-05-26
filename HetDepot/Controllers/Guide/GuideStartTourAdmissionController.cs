//GuideStartTourAdmissionController.cs
using System.Media;
using HetDepot.People.Model;
using HetDepot.Tours;
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
        string title = Program.SettingService.GetConsoleText("guideTourAdmissionTitle");
        string message = Program.SettingService.GetConsoleText("guideTourAdmissionEnterVisitorCode");

        string countZero = Program.SettingService.GetConsoleText("guideTourAdmissionCountZero");
        string countOne = Program.SettingService.GetConsoleText("guideTourAdmissionCountOne");
        string countMul = Program.SettingService.GetConsoleText("guideTourAdmissionCountMultiple", new()
        {
            ["count"] = _tour.Reservations.Count.ToString(),
        });

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


        if (_tour.Reservations.Count == _tour.MaxReservations || personIDToVerify == "s" || personIDToVerify == "S")
        {
            /*if maxreservations reached or if user chooses start tour anyway while not everyone checked in,
            next controller default voor volgende tour of bezoeker aanmelding.
            TODO: Remove tour from list? */

            Program.TourService.StartTour(_tour);

            //start the tour:
            var message_Tour_Starts = Program.SettingService.GetConsoleText("guideTourAllReservationsValidated");
            new AlertView(message_Tour_Starts, ConsoleColor.Blue).Show();
            NextController = new ShowToursController();
            return;
        }


        Visitor verified_ID;
        string message_problem_a;
        string message_problem_b;
        string message_problem_c;
        try
        {
            verified_ID = Program.PeopleService.GetVisitorById(personIDToVerify);
        }
        catch (System.Exception)
        {
            message_problem_a = Program.SettingService.GetConsoleText("guideAdmissionCodeNotValid");
            message_problem_b = Program.SettingService.GetConsoleText("visitorLogonCodeInvalid");
            message_problem_c = message_problem_a + ". " + message_problem_b;
            new AlertView(message_problem_c.ToString(), ConsoleColor.Red).Show();
            //Doorgaan met volgende aanmelding:
            NextController = this;
            return;
        }


        //Om toegelaten te worden tot de tour, moet visitor een reservering hebben, en nog geen admission hebben gehad:

        //Getvisitor aanroepen met PersonIDtoVerify geeft een Null Reference exception.
        //Daarom verander ik het weer naar method aanroepen met nieuwe instance van visitor.
        //else if (Program.TourService.HasReservation(Program.PeopleService.GetVisitorById(personIDToVerify)) && (Program.TourService.HasAdmission(Program.PeopleService.GetVisitorById(personIDToVerify)) == false))
        if (Program.TourService.HasReservation(verified_ID) && (Program.TourService.HasAdmission(verified_ID) == false))
        {
            //the tour person now has admission:
            Program.TourService.AddTourAdmission(_tour, verified_ID);

            //a console beep is played as confirmation:
            Console.Beep();

            //Moet deze reservering gemarkeerd worden als gebruikt?
            // Aanmelden voor deze rondleiding
            var message_success = Program.SettingService.GetConsoleText("guideAdmissionCodeValid");

            //var message = _settingService.GetConsoleText("");
            // in de toekomst vervangen door: var message = _settingService.GetConsoleText("DeCodeIsOngeldig");

            // Gids ziet pop-up met de melding hierboven. Keert terug naar nieuwe instance van deze controller
            new AlertView(message_success, ConsoleColor.Green).Show();
            //Doorgaan met volgende aanmelding:
            NextController = this;
            return;

        }
        //Als code niet geldig is:
        //var check_id = Program.PeopleService.GetVisitorById(personIDToVerify);

        if (verified_ID == null)
        {
            try
            {
                message_problem_a = Program.SettingService.GetConsoleText("guideAdmissionCodeNotValid" + " " + "visitorLogonCodeInvalid");
                new AlertView(message, ConsoleColor.Red).Show();
                //Doorgaan met volgende aanmelding:
                NextController = this;
            }
            catch (System.Exception)
            {


            }
            //print: Je bent niet aangemeld. De code is niet geldig. Controleer uw code en probeer het nog eens

            /*             var message_problem = Program.SettingService.GetConsoleText("guideAdmissionCodeNotValid" + " " +"visitorLogonCodeInvalid");
                        new AlertView(message, ConsoleColor.Red).Show();
                        //Doorgaan met volgende aanmelding:
                        NextController = this; */
            return;
        }
        //als visitor ID geldig is, maar er geen reservering is:

        if (Program.PeopleService.GetVisitorById(personIDToVerify) != null)
        {

            /* TODO: Nogmaal invoeren personeelscode en vragen om handmatig aanmelden, RequestAuthenticationController kan hier niet gebruikt worden omdat er dan naar de bezoeker flow gegaan word */

            // //Alert: handmatig aanmelden:
            var message_manual_add = Program.SettingService.GetConsoleText("guideTourVisitorAddWithoutReservationOption");
            new AlertView(message_manual_add, ConsoleColor.Blue).Show();
            // //handmarig aanmelden:
            NextController = new GuideReservationCreateController(_tour, new Visitor(personIDToVerify));
            return;

        }

        //Als niet alles afgevangen kan worden door de twee hierboven, bijvoorbeeld als code al gebruikt is, of code is geldig maar heeft geen
        //Reservering, dan moet hieronder de reden aangepast worden.
        //Of we kunnen ervoor kiezen om de geldige code hierna naar de controller 'Persoon handmatig toevoegen' te sturen.

        message_problem_a = Program.SettingService.GetConsoleText("guideAdmissionCodeNotValid");
        message_problem_b = Program.SettingService.GetConsoleText("visitorLogonCodeInvalid");
        message_problem_c = message_problem_a + ". " + message_problem_b;
        new AlertView(message_problem_c.ToString(), ConsoleColor.Red).Show();
        //Doorgaan met volgende aanmelding:
        NextController = this;
    }


}
