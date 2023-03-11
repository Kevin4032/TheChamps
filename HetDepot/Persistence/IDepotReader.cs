using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HetDepot.Persistence
{
	public interface IDepotReader
	{
		T Read<T>(string path);
	}
}
