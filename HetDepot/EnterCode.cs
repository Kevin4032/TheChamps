using System;

public class EnterCode
{
    static string Code = "";

    public static void AskforCode()
    {
        Console.WriteLine("Voer uw code in");
        Code = Console.ReadLine() ?? "";
        
    }
}
//  Als input niet het juiste format heeft, dan foutmelding.
// if statement of try catch?

// int input = Convert.ToInt32 (Console.ReadLine()); 

// if (input !=)


// {
//   Console.WriteLine(input + "is niet correct.");
