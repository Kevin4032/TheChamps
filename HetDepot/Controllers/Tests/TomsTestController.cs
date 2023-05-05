using HetDepot;
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
        /*
         * Hieronder ListView gemaakt. Deze ListView bied de mogelijkheid een selecteerbare lijst te
         * tonen in de console. ListView heeft 2 parameters string Title en listViewItems.
         * listViewItems moet een List zijn van classes die de IListableObject interface extenden of
         * een lijst van directe ListItems.
         * 
         * Wat IListableObject mogelijk maakt is dat een model, in dit geval Tour, zonder transformatie
         * gebruikt kan worden in een list. IListableObject verplicht de class een instance function
         * ToListableItem() te hebben de instance convert naar een ListableItem.
         */
        var tours = Program.TourService.Tours;
        ListView<Tour> tourOverviewVisitorWithInterface = new("Welkom bij het depot", tours.ToList<IListableObject<Tour>>());

        /*
         * Wanner de ListView is aangemaakt kan deze worden weergegeven en kan de keuze opgehaald worden
         * via ShowAndGetResult(). Hier word de Value die terug word gegeven als object terug omgezet naar
         * een Tour.
         *
         * De value is nooit iets anders dan Tour maar omdat er maar 1 return type kan zijn
         * is deze object in ShowAndGetResult. Maar om te zorgen dat de compiler het nog snapt worden de value
         * hier expliciet terug gezet naar Tour via de casting (Tour)
         */
        Tour? selectedTourListItem = tourOverviewVisitorWithInterface.ShowAndGetResult();

        if (selectedTourListItem == null)
            return; // Terug waar we vandaan kwamen

        /*
         * Hieronder word een InputView gemaakt deze lijkt op de list view alleen heeft deze in plaats van
         * een list een input veld. InputView wilt 2 parameters (weer) Title en Message. Title word boven
         * de input weergegeven en Message net voor het input veld. Bijvoorbeeld wanner Message "Uw code:" is.
         * Zal de console Uw Code: schrijven en gelijk achter : kan de user dan iets invoeren.
         * Er zit geen validatie in de inputview behalve het niet toelaten van lege inputs.
         */
        string? userCode =
            (new InputView("Reservering maken voor " + selectedTourListItem.GetTime(),
                "Uw code:")).ShowAndGetResult();

        /*
         * Hieronder weer de listview maar dan met een Subtitle en Directe ListViewItems 
         */
        ListView<bool> changeExisitingReservation = new("Er is al een reservering voor vandaag. Wilt u doorgaan?",
            "Als u doorgaat wordt de vorige reservering verwijderd.", new()
            {
                new ListViewItem<bool>("Ja", true, false, 1),
                new ListViewItem<bool>("Nee", false, false, 1)
            });

        /*
         * Hier word de Value die terug word gegeven als object terug omgezet naar een boolean
         */
        bool deletePrev = changeExisitingReservation.ShowAndGetResult();

        /*
         *
         * Daarna wordt en 1 van de AlertViews aangeroepen. In tegendeel tot de List en -InputView
         * geeft AlertView niks terug. Het enige dat deze doet is het weergeven van een message
         * in het midden van de console met de gekozen ConsoleColor. AlertView heeft 3 constants voor
         * kleuren maar in principe kan elke kleur gebruikt worden
         * 
         */
        if (deletePrev)
        {
            (new AlertView("Uw reservering is aangemaakt", AlertView.Success)).Show();
        }
        else
        {
            (new AlertView("Uw reservering wordt behouden", AlertView.Info)).Show();
        }

        Renderer.ResetConsole();
    }
}
