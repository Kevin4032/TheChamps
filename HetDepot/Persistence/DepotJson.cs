using System.Text.Json;

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

		public void Write<T>(string filePath, T objectToWrite)
		{
			//TODO: niet altijd alles schrijven, kijken met json.
			var rawJson = JsonSerializer.Serialize(objectToWrite);
			File.WriteAllText(filePath, rawJson);
		}
		public void Append<T>(string filePath, T objectToWrite)
		{
			//TODO: opleuken
			var rawJson = JsonSerializer.Serialize(objectToWrite);			
			File.AppendAllLines(filePath, new List<string>() { rawJson });
		}

	}
}
