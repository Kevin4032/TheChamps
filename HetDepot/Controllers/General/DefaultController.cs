using HetDepot.Tours.Model;
using HetDepot.Views;
using HetDepot.Views.Interface;
using HetDepot.Views.Parts;

namespace HetDepot.Controllers;

public class DefaultController : Controller
{
    public override void Execute()
    {

		NextController = new ShowToursController();

	}
    
    
    
}