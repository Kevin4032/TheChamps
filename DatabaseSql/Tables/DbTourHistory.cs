using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseSql.Tables
{
	internal class DbTourHistory : ITableCreate
	{
		internal DbTourHistory()
		{
		}
		public string CreateTable { get { return @"
                create table tourHistory (
                id integer not null primary key,
                timeStart text null,
                guide integer null
            );"; } }
		public string InsertData { get { return @""; } }
	}
}