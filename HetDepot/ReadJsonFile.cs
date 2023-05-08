namespace HetDepot;
using System.Text.Json;

class ReadJsonFile

{
    public static void JSONwrite(List<TimesSetting> settings)
    {
        string jsonString = JsonSerializer.Serialize(settings);
        Console.WriteLine(jsonString);

        File.WriteAllText("settings.json", jsonString);

    }
    public static List<TimesSetting> JSONread()
    {

        string text = File.ReadAllText(@"./settings.json");
        var import = JsonSerializer.Deserialize<List<TimesSetting>>(text) ?? new();

        foreach (var lijstNames in import)
        {
            Console.WriteLine(lijstNames.Name);
            foreach (var val in lijstNames.Value)
            {
                Console.WriteLine(val);
            }
        }
        return import;
    }

}
