using DatabaseSql;
using HetDepot.Controllers;
using Service.DepotObjectMapper;
using Service.FileReader;

namespace TestKevin
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var db = new DbController();
            var fc = new FileController();
            var om = new DepotObjectMapper();

            //var pc = new PersonnelController(fc, db, om);
            //var gaatDitgoed = pc.LoadPersonnel();
            //Console.WriteLine(gaatDitgoed);

            Console.WriteLine("Kevin init start");
            db.KevinInit();
			Console.WriteLine("Kevin init eind");

		}
    }
}