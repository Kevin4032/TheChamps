namespace HetDepot.Views.Parts;

public class ListViewItemPart
{

    public string Text;
    public int? Width;

    public ListViewItemPart(string text)
    {
        Text = text;
    }

    public ListViewItemPart(string text, int width)
    {
        Text = text;
        Width = width;
    }
}