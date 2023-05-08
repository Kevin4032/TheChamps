using HetDepot.Tours.Model;
using HetDepot.Views;
using HetDepot.Views.Parts;

namespace HetDepot.Controllers.Manager;

public class ManagerDayOccupancyOverview : Controller
{

    private readonly List<Tour> _tours;
    private readonly int _type;

    public ManagerDayOccupancyOverview(List<Tour> tours, int type)
    {
        _tours = tours;
        _type = type;
    }

    public override void Execute()
    {
        var graphValues = new List<BarGraphPart>();

        foreach (var tour in _tours)
        {
            graphValues.Add(new BarGraphPart(tour.GetTime(),
                _type == 0 ? tour.Reservations.Count : tour.Admissions.Count));
        }

        BarGraphView occupancyRate = new BarGraphView(
            Program.SettingService.GetConsoleText("managerOccupancyForDayTitle", new Dictionary<string, string>()
            {
                {"date", _tours[0].StartTime.ToString("dd/MM/yyyy")}
            }),
            Program.SettingService.GetMaxTourReservations(),
            graphValues);
        occupancyRate.Show();

        NextController = new ManagerTypeQuestion(_tours);

    }

}