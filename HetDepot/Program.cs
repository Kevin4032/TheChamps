using HetDepot.Service;

namespace HetDepot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var setting = new SettingService();

            var tours = new TourService(setting);
            tours.GetTours();
        }
    }
}