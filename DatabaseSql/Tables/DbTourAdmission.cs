using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseSql.Tables
{
	internal class DbTourAdmission : ITableCreate
	{
		private const string _tableName = "tourAdmission";
		public DbTourAdmission() { }
		public string TableName { get { return _tableName; } }
		public string CreateTable { get { return @"
                        create table tourAdmission (
                        id integer not null primary key autoincrement,
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
