namespace HetDepot.Views.Parts;

public class ListViewItemPart
{

    /*
     * This is a Part used in the ListViewItem. Each part has a text and width
     */

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
