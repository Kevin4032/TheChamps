namespace HetDepot;

using HetDepot.Controllers;

internal class Program
{
    public static Controller? CurrentController { get; private set; }

    public static void Main(string[] args)
    {
        // Start by creating a default controller (see _createDefaultController below)
        CurrentController = _createDefaultController();

        // Basic program loop
        // If there is a default controller, the program should always return to it and never end
        while (CurrentController != null)
        {
            // Execute the current controller
            CurrentController.Execute();
            
            // Set up the next controller (the "NextController" that was set by the controller that just executed, or else the default controller if NextController is null)
            CurrentController = Controller.NextController ?? _createDefaultController();
            Controller.ResetNextController(); // Reset NextController to null
        }
    }

    private static Controller? _createDefaultController()
    {
        // This creates an instance of the default controller (the "home screen")
        // It's the first screen to be shown and the program returns to it if a controller does not provide a different controller to run next
        return new ExampleController();
    }
}