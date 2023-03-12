namespace HetDepot
{
    internal class Program
    {
        static void Main(string[] args)

        
        {
            Console.WriteLine ("Voer uw code in:");
            var CodeNumber = Console.ReadLine();

            int code;
            if (!int.TryParse(CodeNumber, out code))

            {
                Console.WriteLine ("Deze code is niet juist, probeer het nog een keer");
                CodeNumber = Console.ReadLine();
            }
            else

        Console.WriteLine ("Bedankt!");
            
            
            
        }
        
    }   }