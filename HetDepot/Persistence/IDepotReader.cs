namespace HetDepot.Persistence
{
	public interface IDepotReader
	{
		T Read<T>(string path);
	}
}
