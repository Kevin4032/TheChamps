using HetDepot.Errorlogging;
using HetDepot.People;
using HetDepot.Persistence;
using HetDepot.Settings;
using HetDepot.Tours;

namespace HetDepot.Controllers;

public abstract class Controller
{
	/*
        Base Controller class. It is "abstract", meaning that it is incomplete and cannot be directly instantiated (you can't do "new Controller()").
        Instead, create a new class that extends this one for every controller action you want to support, and override the Execute() method:

        namespace HetDepot.Controllers;
        
        class MyNewController : Controller
        {
            public override void Execute()
            {
                // Your code here: Load some models, call up a view
                // Wait for the user to do something with the view
                
                // Decide what the next controller will be:
                NextController = new SomeOtherController());
                // If you don't do this (or use "null"), the next controller will be the default controller set in Program.cs (the "home screen")
            }
        }

        Feel free to add more properties, methods, constructors, etc.
    */

    // The controller to run after this (read only except for Controllers)
    public static Controller? NextController { get; protected set; }

    // Override this method to run your code (this is required because it is "abstract", there is no code to run in the class yet)
    public abstract void Execute();
    
    // This is used by Program.cs
    public static void ResetNextController() => NextController = null;
}