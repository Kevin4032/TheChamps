using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HetDepot.JsonReader
{
	public static class JsonHelper
	{

		public static T ReadJson<T>(string filePath)
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
