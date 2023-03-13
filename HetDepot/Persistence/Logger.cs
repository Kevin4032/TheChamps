namespace HetDepot.Persistence
{
	public class Logger : IDepotLogger
	{
		private IDepotDataReadWrite _depotDataReadWrite;
		private string _errorLog;

		public Logger(IDepotDataReadWrite depotDataReadWrite)
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
