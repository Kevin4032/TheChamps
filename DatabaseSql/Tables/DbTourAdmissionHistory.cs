using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseSql.Tables
{
	internal class DbTourAdmissionHistory : ITableCreate
	{
		private const string _tableName = "tourAdmissionHistory";
		public DbTourAdmissionHistory() { }
		public string TableName { get { return _tableName; } }
		public string CreateTable { get { return @"
                        create table tourAdmissionHistory (
                        id integer not null primary key,
                        person integer not null,
                        reservation integer not null,
                        tour integer not null,
                        timeAdmission text not null,
                        dateAdded text not null,
                        dateDeleted text null
            );"; } }

		public string InsertData { get { return @""; } }
	}
}
