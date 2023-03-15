namespace HetDepot.Controllers.Tests;

class TedsTestController : Controller
{
    /*
        Ted's code from the old Program.cs
    */

    public override void Execute()
    {
        List<TimesSetting> settings = new() {
            new("tourTimes", new () {"11:00","11:20","11:40","12:00","12:20","12:40","13:00","13:20","13:40","14:00"})
        };
        ReadJsonFile.JSONread();
        ReadJsonFile.JSONwrite(settings);
    }
}