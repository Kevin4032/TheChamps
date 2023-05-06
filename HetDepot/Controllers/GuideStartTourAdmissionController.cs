//GuideStartTourAdmissionController.cs
using HetDepot.Views;
using HetDepot.People.Model;
using HetDepot.Views.Interface;
using HetDepot.Views.Parts;


        namespace HetDepot.Controllers;
        
        class GuideStartTourAdmissionController : Controller
        {
            public override void Execute()
            {
                // Your code here: Load some models, call up a view
                // Wait for the user to do something with the view
                
                // Decide what the next controller will be:
                //NextController = new SomeOtherController());
                // If you don't do this (or use "null"), the next controller will be the default controller set in Program.cs (the "home screen")
            
            //Ik hoop dat de nextTour van de vorige controller nog steeds dezelfde is. Als dat zo is, kunnen wij hier nog een instance hiervan maken:
            var nextTour = Program.TourService.GetNextTour(); 
            //check of PersonIDToverify een reservering heeft:
            var personIDToVerify = new InputView($"{nextTour.Admissions.Count()} bezoekers hebben zich aangemeld."," Voer jouw unieke code in, of typ \"start\" om de rondleiding te starten zonder anderen aan te melden ").ShowAndGetResult();
            

/*             var guideChoicesList = new List<string>() {
                "1. Volgende aanmelding",
                "2. Aanmelden zonder reservering",
                "3. Starten rondleiding"
            }; */

			/* List<ListViewItem> guideChoicesList = new List<ListViewItem>
            {
                new ListViewItem("1. Volgende aanmelding",1),
                new ListViewItem("2. Aanmelden zonder reservering",2),
                new ListViewItem("3. Starten rondleiding",3)
            }; */
/*             var GuideChoices = new ListView("kies wat: ",new List<ListableItem>
            {
                new ListViewItem("1. Volgende aanmelding",1),
                new ListViewItem("2. Aanmelden zonder reservering",2),
                new ListViewItem("3. Starten rondleiding",3)
            }
                ).ShowAndGetResult; */
            
            
            //var GuideChoices =  new List<ListableItem>(guideChoicesList);
            
            

            
            
     
			
            if (nextTour.Reservations.Count == nextTour.MaxReservations)
            {
                /*if maxreservations reached or if user chooses start tour anyway while not everyone checked in, 
                next controller default voor volgende tour of bezoeker aanmelding.
                Remove tour from list? */
            }

            if (Program.TourService.HasReservation(new Visitor(personIDToVerify)))
            {
                nextTour.AddAdmission(new Visitor(personIDToVerify));
                //voer piepgeluid uit
                //Moet deze reservering gemarkeerd worden als gebruikt?

            }
            else
            {
                var message = $"De code {personIDToVerify} heeft geen reservering.";
                //var message = _settingService.GetConsoleText("");
                // in de toekomst vervangen door: var message = _settingService.GetConsoleText("DeCodeIsOngeldig");

                // Gids ziet pop-up met de melding hierboven. Keert terug naar nieuwe instance van deze controller
			    new AlertView(message, AlertView.Info).Show();

			    NextController = new GuideStartTourAdmissionController();
            }
            }


        }