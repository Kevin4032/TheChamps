using HetDepot.Tours.Model;
using HetDepot.Views;
using HetDepot.Views.Interface;
using HetDepot.Views.Parts;

namespace HetDepot.Controllers;

public class GuideController : Controller
{
    public override void Execute()
    {
        string personnelCode = 
            (new InputView("Gids | Voer uw personeels nummer in", "Personeels nummer:")).ShowAndGetResult();
        
        if (true) // TODO Check if personnelCode is valid
        {
            // TODO: Create Tour overview
            (new AlertView("TODO: Create Tour overview", AlertView.Info)).Show();
            return;
        }
        
        /*** UNREACHABLE:

        (new AlertView("Personeels nummer ongeldig", AlertView.Error)).Show();
        NextController = new GuideController();
        
        ***/
    }
}