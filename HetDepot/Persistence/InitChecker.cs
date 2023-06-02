namespace HetDepot.Persistence;

using HetDepot;

public static class InitChecker
{
    // Helper class for testing if data loaded from json files can be loaded correctly; used when program first runs

    public static void CheckProperInit()
    {
        var visitorCount = Program.PeopleService.GetVisitors().Count;
        if (visitorCount == 0)
        {
            throw new Exception("Geen bezoekers gevonden, zie ExampleVisitors.json");
        }

        var guide = Program.PeopleService.GetGuide();
        var manager = Program.PeopleService.GetManager();

        if (guide.Equals(manager))
        {
            throw new Exception("ID van Gids en Afdelingshoofd is gelijk. Zie ExampleManager.json / ExampleGuide.json");
        }
    }
}
