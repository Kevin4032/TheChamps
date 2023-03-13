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
