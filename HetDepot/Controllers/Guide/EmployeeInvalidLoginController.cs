using HetDepot.Views;

namespace HetDepot.Controllers;

class EmployeeInvalidLoginController : Controller
{
    /*
        Very simple example of a controller. All it does is show a message and ask the user to press any key.
        After that, the code ends and the program will return to the default controller.
    */

    public override void Execute()
    {
        // Console methods like this should actually go in a View, but for now this will do

        var message = Program.SettingService.GetConsoleText("staffInvalidCode");

        new AlertView(message, ConsoleColor.Red).Show();

        NextController = new ShowToursController();

    }
}
