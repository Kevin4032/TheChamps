namespace HetDepot.Controllers.Tests;

class TestController : Controller
{
    /*
        All Controllers in this folder are used for testing, and should be removed in the definitive version.
        This asks to input a name, and runs the appropriate test controller next. This is based on the old contents of Program.cs
        Individual people's test code have been moved to seperate controllers
    */

    public override void Execute()
    {
        Console.WriteLine("Hello, World!");

        Console.WriteLine("Typ een naam om functies te testen (Kevin/Tom/Ruben/Ted/Karlijn), of iets anders om de ExampleController uit te voeren:");
        string testName = Console.ReadLine() ?? "";

        NextController = testName.ToLower() switch
        {
            "kevin" => new KevinsTestController(),
            "tom" => new TomsTestController(),
            "ruben" => new RubensTestController(),
            "ted" => new TedsTestController(),
            "karlijn" => new KarlijnsTestController(),
            _ => new ExampleController(),
        };
    }
}