using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HetDepot.Settings
{
	public interface ISettingService
	{
		HashSet<string> GetTourTimes();
		string GetSettingValue(string name);
		string GetConsoleText(string name);
	}
}
