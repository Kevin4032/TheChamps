namespace HetDepot;

class TimesSetting
{
    public string Name { get; set; }
    public List<string> Value { get; set; }

    public TimesSetting(string name, List<string> value)
    {
        Name = name;
        Value = value;
    }
}
