using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseSql
{
	public interface IPersistence
	{
		bool ExistsObjectPersonnelType(string name);
		bool ExistsObjectPersonnelType(int id);
	}
}
