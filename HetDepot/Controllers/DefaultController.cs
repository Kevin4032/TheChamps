using HetDepot.Tours.Model;
using HetDepot.Views;
using HetDepot.Views.Interface;
using HetDepot.Views.Parts;

namespace HetDepot.Controllers;

public class DefaultController : Controller
{
    public override void Execute()
    {
        
        // Create Tour overview TODO: Replace with storage call
        object selection = (new ListView("Welkom bij het depot", new List<IListableObject>()
        {
            new Tour(new DateTime(2023, 3, 8, 10, 00, 00), 3),
            new Tour(new DateTime(2023, 3, 8, 10, 15, 00), 6),
            new Tour(new DateTime(2023, 3, 8, 10, 30, 00), 0),
            new Tour(new DateTime(2023, 3, 8, 10, 45, 00), 13),
            new Tour(new DateTime(2023, 3, 8, 11, 00, 00), 0),
            new Tour(new DateTime(2023, 3, 8, 11, 15, 00), 10),
            new Tour(new DateTime(2023, 3, 8, 11, 30, 00), 1),
            new Tour(new DateTime(2023, 3, 8, 12, 00, 00), 8),
        }, new List<ListableItem>()
        {
            new ListViewItem("Gids overzicht", "toGuide")
        })).ShowAndGetResult();

        if (ReferenceEquals(selection, "toGuide"))
        {
            NextController = new GuideController();
            return;
        }

        selection = (selection as Tour)!;

    }
    
    
    
}