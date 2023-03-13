using HetDepot.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HetDepot.Errorlogging
{
	public class DepotErrorLogger : IDepotErrorLogger
	{
		private DepotErrorJson _depotDataReadWrite;
		private string _errorLog;

		public DepotErrorLogger(DepotErrorJson depotDataReadWrite)
		{
			_errorLog = Path.Combine(Directory.GetCurrentDirectory(), "ExampleFile\\ExampleErrorlog.txt");
			_depotDataReadWrite = depotDataReadWrite;
		}

		public void LogError(string message)
		{
			_depotDataReadWrite.Append<string>(_errorLog, message);
		}
	}
}
