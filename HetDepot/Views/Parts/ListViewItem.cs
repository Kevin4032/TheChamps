namespace HetDepot.Views.Parts;

class ListViewItem<T> : ListableItem<T>
{

    /*
     * Item that can be listed in ListView. It can take a string as its text or a list of ListViewItemParts.
     * With the ListViewItemPart there is more control over the rendering and just the string is more simple.
     */

    public readonly List<ListViewItemPart> Parts;

    public ListViewItem(string text, T? value, bool disabled = false, int textAlignment = 0) : base(value, disabled, textAlignment)
    {
        Parts = new List<ListViewItemPart> { new(text) };
    }

    public ListViewItem(List<ListViewItemPart> parts, T? value, bool disabled = false, int textAlignment = 0) : base(value, disabled, textAlignment)
    {
        Parts = parts;
    }

    public override List<ListViewItemPart> GetTextParts()
    {
        return Parts;
    }
}
