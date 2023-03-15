namespace HetDepot.Views.Parts;

public class ListViewItem : ListableItem
{
    public readonly string Text;
    
    public ListViewItem(string text, object value) : base(value, false, 0)
    {
        Text = text;
    }

    public ListViewItem(string text, object value, bool disabled) : base(value, disabled, 0)
    {
        Text = text;
    }

    public ListViewItem(string text, object value, bool disabled, int textAlignment) : base(value, disabled, textAlignment)
    {
        Text = text;
    }

    public override List<ListViewItemPart> GetTextParts()
    {
        return new List<ListViewItemPart> { new(Text, Renderer.ConsoleWidth) };
    }
}