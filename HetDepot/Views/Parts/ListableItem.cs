namespace HetDepot.Views.Parts;

public abstract class ListableItem<T>
{
    /*
     * This is the base class that is use for listable items in the ListView.
     */
    
    public readonly T? Value; // Returned after selected in View
    public readonly bool Disabled;
    public readonly int TextAlignment = 0;
    public abstract List<ListViewItemPart> GetTextParts();

    protected ListableItem(T? value, bool disabled, int textAlignment)
    {
        Value = value;
        Disabled = disabled;
        TextAlignment = textAlignment;
    }
}