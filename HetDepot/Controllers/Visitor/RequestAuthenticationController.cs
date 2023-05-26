using HetDepot.People.Model;
using HetDepot.Tours.Model;
using HetDepot.Views;

namespace HetDepot.Controllers
{
    public class RequestAuthenticationController : Controller
    {
        private Tour _tour;
        private bool _forGroup;

        public RequestAuthenticationController(Tour tour, bool forGroup = false) : base()
        {
            _tour = tour;
            _forGroup = forGroup;
        }

        public override void Execute()
        {
            var title = Program.SettingService.GetConsoleText("visitorRequestCodeSelectedTour", new()
            {
                ["time"] = _tour.GetTime(),
            });
            var textToUser = Program.SettingService.GetConsoleText("logonOpeningWelcome");

            if (_forGroup)
            {
                title = Program.SettingService.GetConsoleText("visitorRequestCodeSelectedTourForGroup", new Dictionary<string, string>()
                {
                    ["time"] = _tour.GetTime()
                });
                textToUser = Program.SettingService.GetConsoleText("logonOpeningWelcomeForGroup");
            }

            Person? person = null;
            string userCode;

            while (person == null)
            {
                userCode = (new InputView(title, textToUser)).ShowAndGetResult() ?? "";

                if (userCode.ToLower() == "q")
                    break;

                person = userCode == "" ? null : GetPerson(userCode);

                if (person == null || !(person is Visitor))
                {
                    var errorMessage = Program.SettingService.GetConsoleText("visitorLogonCodeInvalid");
                    new AlertView(errorMessage, AlertView.Error).Show();
                }
            }

            NextController = person is Visitor ? new ValidateTourPickController(_tour, person, _forGroup) :
                new ShowToursController();
        }

        private Person? GetPerson(string userCode)
        {
            Person? person = null;

            try
            {
                person = Program.PeopleService.GetById(userCode);
            }
            catch (Exception ex)
            {
                Program.ErrorLogger.LogError($"{this.GetType()} - {ex.Message}");
            }

            return person;
        }
    }
}
