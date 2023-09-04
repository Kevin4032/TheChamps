using DatabaseSql;

namespace HetDepot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var dbController = new DbController();
            dbController.KevinInit();
        }
    }
}