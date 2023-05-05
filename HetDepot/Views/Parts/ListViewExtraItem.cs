namespace HetDepot.Views.Parts;

using HetDepot.Views.Interface;

class ListViewExtraItem<T, TExtra> : ListViewItem<T>, IListableExtraItem<TExtra>
{

    /*
     * Special extra item that can be listed in ListView. Unlike a regular item, it is not used to select a model of type T but rather an extra item of
     * type TExtra. Constructors take a lambda Func<TExtra>: () => new WhateverTExtraIs(), to allow the object to be created only when necessary
     */
    private Func<TExtra>? _extra;

    public ListViewExtraItem(string text, Func<TExtra>? extra, bool disabled = false, int textAlignment = 0) : base(text, default, disabled, textAlignment)
    {
        _extra = extra;
    }

    public ListViewExtraItem(List<ListViewItemPart> parts, Func<TExtra>? extra, bool disabled = false, int textAlignment = 0) : base(parts, default, disabled, textAlignment)
    {
        _extra = extra;
    }

    public TExtra? GetExtraItem() => _extra != null ? _extra() : default(TExtra);
}
