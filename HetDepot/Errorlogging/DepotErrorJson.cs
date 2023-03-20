using HetDepot.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HetDepot.Errorlogging
{
	public class DepotErrorJson 
	{
		public DepotErrorJson() { }

		public void Append<T>(string filePath, T objectToWrite)
		{
			//TODO: opleuken
			var rawJson = JsonSerializer.Serialize(objectToWrite);
			File.AppendAllLines(filePath, new List<string>() { rawJson });
		}
	}
}
