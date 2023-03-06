using System.Text.Json;

namespace Service.DepotObjectMapper
{
	public class DepotObjectMapper : IObjectMapper
	{
		public DepotObjectMapper() { }

		public T JsonToObject<T>(string json, T toSerialize)
		{
			return JsonSerializer.Deserialize<T>(json);
		}
	}
}