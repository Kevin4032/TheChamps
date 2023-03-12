namespace HetDepot
{
    internal class Program
    {
        static void Main(string[] args)
        {

            try
            {
                Console.GetCursorPosition();
            }
            catch (System.IO.IOException)
            {
                // Voor degenen die Visual Studio Code niet goed ingesteld hebben!
                Console.WriteLine("No interactive console available");
                Console.WriteLine("If you see this in Visual Studio Code, set \"console\" to \"integratedTerminal\" and \"internalConsoleOptions\" to \"neverOpen\" in .vscode/launch.json");
                return;
            }

            TourList tours = new();
            tours.ShowScreen();

            TourList.ResetConsole();
            Console.WriteLine($"U heeft gekozen voor de rondleiding om {tours.Tours[tours.SelectedTour]} uur");
            Console.WriteLine($"Bedankt en tot ziens bij Het Depot!");
        }
    }
}