namespace HetDepot.Views.Parts;

class ListViewPartedItem : ListableItem
{
    public readonly List<ListViewItemPart> Parts;

    public ListViewPartedItem(List<ListViewItemPart> parts, object value) : base(value,false, 0)
    {
        Parts = parts;
    }

    public ListViewPartedItem(List<ListViewItemPart> parts, object value, bool disabled): base(value, disabled, 0)
    {
        Parts = parts;
    }

    public ListViewPartedItem(List<ListViewItemPart> parts, object value, bool disabled, int textAlignment) : base(value, disabled, textAlignment)
    {
        Parts = parts;
    }

    public override List<ListViewItemPart> GetTextParts()
    {
        return Parts;
    }
}