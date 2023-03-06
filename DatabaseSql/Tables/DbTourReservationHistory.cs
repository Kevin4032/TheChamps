using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseSql.Tables
{
	internal class DbTourReservationHistory : ITableCreate
	{
		private const string _tableName = "tourReservationHistory";
		public DbTourReservationHistory() { }
		public string TableName { get { return _tableName; } }
		public string CreateTable { get { return @"
                        create table tourReservationHistory (
                        id integer not null primary key,
                        timeReservation text not null,
                        tour integer not null,
                        person integer not null,
                        dateAdded text not null,
                        dateDeleted text null
            );"; } }

		public string InsertData { get { return @""; } }
	}
}
