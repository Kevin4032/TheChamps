using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseSql.Tables
{
    internal class DbTour : ITableCreate
    {
		private const string _tableName = "tour";
		public DbTour() { }
		public string TableName { get { return _tableName; } }
		public string CreateTable { get { return @"
                create table tour (
                id integer not null primary key autoincrement,
                timeStart text null,
                guide integer null
            );"; } }
        public string InsertData { get { return @""; } }
    }
}
