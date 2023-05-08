namespace HetDepot.Views.Parts;

public class BarGraphPart
{

    public string Label;
    public int value;

    public BarGraphPart(string label, int value)
    {
        Label = label;
        this.value = value;
    }
}