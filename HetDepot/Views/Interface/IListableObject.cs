using HetDepot.Views.Parts;

namespace HetDepot.Views.Interface;

public interface IListableObject<T>
{
    public ListableItem<T> ToListableItem();

}