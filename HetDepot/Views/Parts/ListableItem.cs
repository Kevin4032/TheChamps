namespace HetDepot.Views.Parts;

public abstract class ListableItem
{
    public readonly object Value; // Returned after selected in View
    public readonly bool Disabled;
    public readonly int TextAlignment = 0;
    public abstract List<ListViewItemPart> GetTextParts();

    protected ListableItem(object value, bool disabled, int textAlignment)
    {
        Value = value;
        Disabled = disabled;
        TextAlignment = textAlignment;
    }
}