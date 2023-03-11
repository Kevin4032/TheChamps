using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HetDepot.Persistence
{
	public interface IDepotWriter
	{
		void Write<T>(string filePath, T objectToWrite);
		void Append<T>(string filePath, T objectToWrite);
	}
}
