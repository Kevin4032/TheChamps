namespace HetDepot.Controllers;

public class DefaultController : Controller
{
    public override void Execute()
    {

        NextController = new ShowToursController();

    }
}
