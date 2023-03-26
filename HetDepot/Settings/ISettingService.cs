namespace HetDepot.Settings
{
	public interface ISettingService
	{
		HashSet<string> GetTourTimes();
		int GetMaxTourReservations();
		string GetSettingValue(string name);
		string GetConsoleText(string name);
	}
}
