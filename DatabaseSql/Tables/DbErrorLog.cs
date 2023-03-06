using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseSql.Tables
{
	internal class DbErrorLog : ITableCreate
	{
		private const string _tableName = "errorlog";
		public DbErrorLog() { }
		public string TableName { get { return _tableName; } }
		public string CreateTable { get { return @"
                        create table errorLog (
                        id integer not null primary key,
                        source text not null,
						message text not null,
                        dateAdded text not null,
                        dateDeleted text null
            );"; } }

		public string InsertData { get { return @""; } }
	}
}
