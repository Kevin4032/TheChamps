using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HetDepot.Model
{
    public class Tour
    {
        public Tour(DateTime startTime)
        {
            StartTime = startTime;
            //Guide = guide;
        }

        public long Id { get; private set; }
        public DateTime StartTime { get; private set; }
        public Guide Guide { get; private set; }
    }
}
