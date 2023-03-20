namespace HetDepot;

using HetDepot.Controllers;
using HetDepot.Controllers.Tests;

internal class Program
{
    /*
        Main Program class. This should only serve as a bootstrap to a controller, and be changed as little as possible.
        When creating new functionality, create new controllers instead and modify existing ones to point to the new code.
    */

    public static Controller? CurrentController { get; private set; }

    private static Controller? _createDefaultController()
    {
        // This creates an instance of the default controller (the "home screen")
        // It's the first screen to be shown and the program returns to it if a controller does not provide a different controller to run next
        return new TestController();
    }

    public static void Main(string[] args)
    {
        // Start by creating a default controller (see _createDefaultController above)
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
}