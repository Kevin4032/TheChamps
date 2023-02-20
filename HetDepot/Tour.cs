namespace HetDepot;

public class Tour
{
    public readonly DateTime StartsAt;
    public int availableSpots = 13;

    public Tour(DateTime startsAt)
    {
        StartsAt = startsAt;
    }

    public string GetTime()
    {
        return StartsAt.ToString("H:mm");
    }

    public override string ToString() => GetTime();
}