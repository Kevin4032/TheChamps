using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HetDepot.Registration.Model
{
	public class Admission
	{
		public Admission()
		{
			Admissions = new HashSet<string>();
		}
		public HashSet<string> Admissions { get; set; }
	}
}
