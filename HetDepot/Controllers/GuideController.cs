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
        
        //_peopleService.GetGuide().Id
      
        bool isGuide = personnelCode == _peopleService.GetGuide().Id;
        if (isGuide || personnelCode == _peopleService.GetManager().Id) // TODO Check if personnelCode is valid
        {            
            NextController = new GuideShowAndSelectTourController();
            return;
        }
        //else if list[X][0] == "D":
        
        (new AlertView("Personeels nummer ongeldig", AlertView.Error)).Show();
        NextController = new GuideController();
    }
}

