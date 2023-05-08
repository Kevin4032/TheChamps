namespace HetDepot.Persistence
{
    public interface IDepotWriter
    {
        void Write<T>(string filePath, T objectToWrite);
        void Append<T>(string filePath, T objectToWrite);
    }
}
