namespace HetDepot.Controllers;

class ExampleController : Controller
{
    /*
        Very simple example of a controller. All it does is show a message and ask the user to press any key.
        After that, the code ends and the program will return to the default controller.
    */

    public override void Execute()
    {
        // Console methods like this should actually go in a View, but for now this will do

        Console.WriteLine("Dit is een simpel voorbeeld voor een controller.");
        Console.WriteLine("Druk op een toets om door te gaan.");
        Console.ReadKey(true);

        // NextController = new SomeOtherController();
    }
}