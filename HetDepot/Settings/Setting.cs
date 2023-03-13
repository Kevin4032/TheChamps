namespace HetDepot.Settings
{
	public class Setting
	{
		private string _name; 
		private string _value;
		public Setting(string name, string value)
		{
			_name = name;
			_value = value;
		}
		public string Name { get { return _name; }  }
		public string Value { get { return _value; } }
	}
}
