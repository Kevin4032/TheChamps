using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.FileReader
{
	public interface IJsonReader
	{
		string ReadJson(string path);
	}
}
