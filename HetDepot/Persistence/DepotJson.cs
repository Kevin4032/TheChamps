using System.Text.Json;
using System.Text.Json.Serialization;

namespace HetDepot.Persistence
{
	public class DepotJson : IDepotDataReadWrite
	{
		public DepotJson() { }

		public T Read<T>(string filePath)
		{
			T result;

			try
			{
				var rawJson = File.ReadAllText(filePath);
				result = JsonSerializer.Deserialize<T>(rawJson);
			}
			catch (JsonException ex)
			{
				Console.WriteLine($"JSON - {ex.Message}");
				result = default;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Andere - {ex.Message}");
				result = default;
			}

			return result;
		}
	}
}
