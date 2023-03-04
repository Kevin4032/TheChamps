using HetDepot.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HetDepot.Service
{
    public class TourService
    {
        private SettingService _settingService;
        public TourService(SettingService settingService)
        {
            _settingService = settingService;
        }

        public IEnumerable<Tour> GetTours()
        {         
            var tussen = _settingService.GetTours();
            
            foreach (var t in tussen)
            {
                Console.WriteLine($"Tour: {t.StartTime}");
            }

            return tussen;
        }
    }
}
