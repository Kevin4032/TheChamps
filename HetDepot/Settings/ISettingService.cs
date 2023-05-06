namespace HetDepot.Settings
{
    public interface ISettingService
    {
        HashSet<string> GetTourTimes();
        int GetMaxTourReservations();
        string GetConsoleText(string name, Dictionary<string, string>? components = null);
    }
}
