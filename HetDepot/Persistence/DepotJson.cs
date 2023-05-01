using System.Text.Json;
using HetDepot.Errorlogging;

namespace HetDepot.Persistence
{
    public class DepotJson : IDepotDataReadWrite
    {
        private IDepotErrorLogger _errorLogger;

        public DepotJson(IDepotErrorLogger errorLogger)
        {
            _errorLogger = errorLogger;
        }

        public T Read<T>(string filePath)
        {
            T result;

            try
            {
                var rawJson = File.ReadAllText(filePath);
                result = JsonSerializer.Deserialize<T>(rawJson) ?? throw new NullReferenceException("Geen json");
            }
            catch (JsonException ex)
            {
                _errorLogger.LogError(ex.Message);
                result = default!;
            }
            catch (Exception ex)
            {
                _errorLogger.LogError(ex.Message);
                result = default!;
            }

            return result!;
        }

        public void Write<T>(string filePath, T objectToWrite)
        {
            var rawJson = JsonSerializer.Serialize<T>(objectToWrite);
            File.WriteAllText(filePath, rawJson);
        }
        public void Append<T>(string filePath, T objectToWrite)
        {
            var rawJson = JsonSerializer.Serialize(objectToWrite);
            File.AppendAllLines(filePath, new List<string>() { rawJson });
        }

    }
}
