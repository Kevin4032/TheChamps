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
            //db.Test();
            //db.CheckData();

            var fc = new FileController();
            var om = new DepotObjectMapper();

            var pc = new PersonnelController(fc, db, om);
            var gaatDitgoed = pc.LoadPersonnel();
            Console.WriteLine(gaatDitgoed);

            

            //var hoi = fc.ReadJson("C:\\Data\\000_Projecten\\HogeschoolRotterdam\\ProjectB\\Personnel.json");
            //Console.WriteLine(hoi);
            
        }
    }
}