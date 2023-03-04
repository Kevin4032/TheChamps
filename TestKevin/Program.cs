using DatabaseSql;

namespace TestKevin
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var db = new DbController();
            //db.Test();

            db.CheckData();
        }
    }
}