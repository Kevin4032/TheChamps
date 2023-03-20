using HetDepot.People.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HetDepot.People
{
	public interface IPeopleService
	{
		IReadOnlyCollection<Visitor> GetVisitors();
		Person GetById(string id);
		Visitor GetVisitorById(string id);
		Guide GetGuide();
		Manager GetManager();
	}
}
