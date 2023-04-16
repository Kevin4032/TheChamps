namespace HetDepot.Settings.Model
{
	public class ConsoleText
	{
		private string _name;
		private string _value;
		public ConsoleText(string name, string value)
		{
			_name = name;
			_value = value;
		}
		public string Name { get { return _name; } }
		public string Value { get { return _value; } }
	}
}
