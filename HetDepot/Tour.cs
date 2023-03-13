namespace HetDepot;

public class Tour
{

    public DateTime StartsAt;
    public int Reservations = 0;
    
    public static int MaxReservations = 13;

    public Tour(DateTime startsAt)
    {
        StartsAt = startsAt;
    }

    public Tour(DateTime startsAt, int reservations)
    {
        StartsAt = startsAt;
        Reservations = reservations;
    }

    public string GetTime()
    {
        return StartsAt.ToString("H:mm");
    }

    public override string ToString()
    {
        return GetTime();
    }
}
