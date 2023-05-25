namespace HetDepot.Errorlogging
{
    public class DepotErrorLogger : IDepotErrorLogger
    {
        private DepotErrorJson _depotDataReadWrite;
        private string _errorLog;

        public DepotErrorLogger(DepotErrorJson depotDataReadWrite)
        {
            _depotDataReadWrite = depotDataReadWrite;
            _errorLog = Path.Combine(Directory.GetCurrentDirectory(), "ExampleFile", "ExampleErrorlog.txt");
        }

        public void LogError(string message)
        {
            try
            {
                var errorMessage = DateTime.UtcNow.ToString();
                errorMessage += " - " + message;
                _depotDataReadWrite.Append<string>(_errorLog, errorMessage);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
