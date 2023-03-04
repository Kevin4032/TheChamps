using HetDepot.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HetDepot.Service
{
    public class SettingService
    {
        public SettingService()
        {

        }

        public IEnumerable<Tour> GetTours()
        {           
            string exampleFile = Path.Combine(Directory.GetCurrentDirectory(), "ExampleFile.json");
            var getSettings = JsonFile.JsonHelper.ReadJson<Setting>(exampleFile);

            var tours = new List<Tour>();

            foreach (var setting in getSettings)
            {
                //Console.WriteLine($"Name: {setting.Name}, Value: {setting.Value}");
                if (setting.Name.Contains("startTimeTour"))
                    tours.Add(new Tour(DateTime.Parse(setting.Value)));

            }

            return tours;
        }
    }
}
