using HetDepot.People.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HetDepot.Persistence
{
	public interface IDepotDataValidator
	{
		bool ValidForAdministration<T>(T dataToValidate) where T : Person;
	}
}
