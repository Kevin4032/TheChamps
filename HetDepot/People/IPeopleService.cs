using HetDepot.People.Model;

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
