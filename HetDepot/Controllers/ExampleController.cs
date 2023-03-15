namespace HetDepot.Controllers;

class ExampleController : Controller
{
    public override void Execute()
    {
        // Very simple example
        // Console methods like this should actually go in a Views, but for now this will do

        Console.WriteLine("Dit is een simpel voorbeeld voor een controller.");
        Console.WriteLine("Druk op een toets om door te gaan.");
        Console.ReadKey(true);

        // NextController = new SomeOtherController();
    }
}