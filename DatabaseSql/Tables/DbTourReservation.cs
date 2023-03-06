using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseSql.Tables
{
	internal class DbTourReservation : ITableCreate
	{
		private const string _tableName = "tourReservation";
		public DbTourReservation() { }
		public string TableName { get { return _tableName; } }
		public string CreateTable { get { return @"
                        create table tourReservation (
                        id integer not null primary key autoincrement,
                        timeReservation text not null,
                        tour integer not null,
                        person integer not null,
                        dateAdded text not null,
                        dateDeleted text null
            );"; } }

		public string InsertData { get { return @""; } }
	}
}
