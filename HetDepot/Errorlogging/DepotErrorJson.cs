using System.Text.Json;

namespace HetDepot.Errorlogging
{
    public class DepotErrorJson
    {
        public DepotErrorJson() { }

        public void Append<T>(string filePath, T objectToWrite)
        {
            var rawJson = JsonSerializer.Serialize(objectToWrite);
            File.AppendAllLines(filePath, new List<string>() { rawJson });
        }
    }
}
