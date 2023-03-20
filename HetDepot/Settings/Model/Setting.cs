namespace HetDepot.Settings.Model
{
    public class Setting
    {
        private List<ConsoleText> _consoleText;
        private List<string> _tourTime;
        private int _maxReservationsPerTour;

        public Setting()
        {
			_consoleText = ExampleConsole();
			_tourTime = ExampleTourTime();
            _maxReservationsPerTour = 13;
        }

		public List<ConsoleText> ConsoleText { get { return _consoleText; } }
        public List<string> TourTimes { get { return _tourTime; } }
        public int MaxReservationsPerTour { get {  return _maxReservationsPerTour; } }

		private List<ConsoleText> ExampleConsole()
		{
            return new List<ConsoleText>
            {
                new ConsoleText("consoleLogonOpeningWelcome", "Meld u aan op de console")
            ,   new ConsoleText("consoleVisitorReservationMaking", "U kunt een reservering maken door een tijdstip te selecteren.")
            ,   new ConsoleText("consoleVisitorReservationAlreadyHavingOne", "U heeft al een reservering. Als u een ander tijdstip selecteert, wordt uw reservering gewijzigd. Als u uw huidige reservering selecteert, wordt deze geannuleerd.")
			,   new ConsoleText("consoleVisitorLogonCodeInvalid", "De code is niet geldig. Controleer uw code en probeer het nog eens.")
			,   new ConsoleText("consoleVisitorReservationGroupOption", "Aanmelden groep")
			,   new ConsoleText("consoleVisitorReservationNoMoreTours", "Er zijn geen rondleidingen meer beschikbaar")
			,   new ConsoleText("consoleVisitorReservationConfirmation", "Uw inschrijving is bevestigd")
			,   new ConsoleText("consoleVisitorReservationGroupStart", "U wordt gevraagd om alle unieke codes in te voeren. De maximale grootte is {instelling maximale grootte rondleiding}. Als u klaar bent, voert u in \u201Cgereed\u201D")
			,   new ConsoleText("consoleVisitorReservationGroupEnd", "De grootte van uw gezelschap is {groepsgrootte}")
			,   new ConsoleText("consoleVisitorReservationMaximumForTour", "Het maximum aantal deelnemers is bereikt.")
			,   new ConsoleText("consoleVisitorReservationCancellationRequestionConfirmation", "Wilt u deze rondleiding annuleren? Ja/Nee.")
			,   new ConsoleText("consoleVisitorReservationCancellationConfirmation", "Reservering geannuleerd")
			,   new ConsoleText("consoleVisitorReservationChangeTourConfirmation", "U bent uitgeschreven voor {tijdstip}\u201D. Uw nieuwe rondleiding start om {tijdstip}")
			,   new ConsoleText("consoleGuideTourVisitorValidationStart", "U kunt de rondleiding starten.")
			,   new ConsoleText("consoleGuideLogonCodeInvalid", "Uw code is niet geldig. Werkt u hier wel?")
			,   new ConsoleText("consoleGuideTourCurrent", "Huidige rondleiding {tijdstip n}")
			,   new ConsoleText("consoleGuideTourStart", "Rondleiding starten?")
			,   new ConsoleText("consoleGuideTourNoToursAvailable", "Er zijn geen rondleidingen meer beschikbaar")
			,   new ConsoleText("consoleGuideTourVisitorValidationStarted", "Er zijn { aantal reserveringen}")
			,   new ConsoleText("consoleGuideTourVisitorValidationVisitorValidated", "{aantal} van {reserveringen} aangemeld.")
			,   new ConsoleText("consoleGuideTourVisitorValidationVisitorNextVisitor", "Volgende aanmelding")
			,   new ConsoleText("consoleGuideTourVisitorAddWithoutReservationOption", "Aanmelden zonder reservering")
			,   new ConsoleText("consoleGuideTourVisitorTourStartOption", "Starten rondleiding")
			,   new ConsoleText("consoleGuideTourAllReservationsValidated", "Alle deelnemers zijn aangemeld.")
			,   new ConsoleText("consoleGuideTourVisitorAddWithoutReservationConfirmation", "Bezoeker toegevoegd. {aantal} van {reserveringen} aangemeld.")
			,   new ConsoleText("consoleManagerTicketsLoaded", "Met succes entreebewijzen geladen")
			,   new ConsoleText("consoleManagerShowOptions", "Laad entreebewijzen voor de dag\nLaad instellingen\nBekijk rondleidinggegevens.")
			,   new ConsoleText("consoleManagerLogonCodeInvalid", "Uw code is niet geldig.")
			,   new ConsoleText("consoleVisitorAlreadyHasTourAdmission", "U heeft al deelgenomen aan een rondleiding vandaag. Morgen weer een kans.")
			};
		}
		private List<string> ExampleTourTime()
		{
			return new List<string> { "11:00", "11:20", "11:40", "12:00", "12:20", "12:40", "13:00", "13:20", "13:40", "14:00" } ;
		}
	}


}
