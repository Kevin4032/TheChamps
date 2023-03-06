using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DepotObjectMapper
{
	public interface IObjectMapper
	{
		T JsonToObject<T>(string json, T toSerialize);
	}


}
