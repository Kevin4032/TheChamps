using HetDepot.People.Model;

namespace HetDepot.Persistence
{
	public interface IDepotDataValidator
	{
		bool ValidForAdministration<T>(T dataToValidate) where T : Person;
	}
}
