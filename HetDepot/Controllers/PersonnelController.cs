using DatabaseSql;
using Service.DepotObjectMapper;
using Service.DepotObjectMapper.ViewModel;
using Service.FileReader;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HetDepot.Controllers
{
	public class PersonnelController
	{
		private IJsonReader _reader;
		private IPersistence _database;
		private IObjectMapper _objectMapper;

		public PersonnelController(IJsonReader reader, IPersistence database, IObjectMapper objectMapper)
		{
			_reader = reader;
			_database = database;
			_objectMapper = objectMapper;
		}

		public bool LoadPersonnel()
		{
			var personnelFile = "C:\\Data\\000_Projecten\\HogeschoolRotterdam\\ProjectB\\Personnel.json";
			var allPersonnelData = _reader.ReadJson(personnelFile);
			Console.WriteLine(allPersonnelData);

			var allPersonnel = _objectMapper.JsonToObject(allPersonnelData, new List<PersonnelFileLoading>());

			foreach (var personnel in allPersonnel)
			{
				Console.WriteLine(personnel.Name);
				Console.WriteLine(personnel.Id);
				Console.WriteLine(personnel.Job);
			}

			Console.WriteLine($" DIt is status van job: {_database.ExistsObjectPersonnelType(allPersonnel[0].Job)} -- NB job = {allPersonnel[0].Job}");

			return true;
		}
	}
}
