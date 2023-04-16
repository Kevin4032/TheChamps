using HetDepot.Tours.Model;
using HetDepot.Views.Interface;
using HetDepot.Views.Parts;

namespace HetDepot.Controllers.Tests;

using Views;

class TomsTestController : Controller
{
    /*
        Tom's code from the old Program.cs
    */

    public override void Execute()
    {
		NextController = new ShowToursController();
    }
}