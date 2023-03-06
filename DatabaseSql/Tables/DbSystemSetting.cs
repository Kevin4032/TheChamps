using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseSql.Tables
{
    internal class DbSystemSetting : ITableCreate
    {
		private const string _tableName = "systemSetting";
		public DbSystemSetting() { }
		public string TableName { get { return _tableName; } }

		public string CreateTable { get { return @"
                        create table systemSetting (
                        id integer not null primary key autoincrement,
                        name text not null,
                        value text not null,
                        dateAdded text not null,
                        dateDeleted text null
            );"; } }

        public string InsertData { get { return @""; } }
    }
}
