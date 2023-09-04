using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseSql.Tables
{
    internal class DbTourRejectReason : ITableCreate
    {
		private const string _tableName = "tourRejectReasons";
		public DbTourRejectReason() { }
		public string TableName { get { return _tableName; } }

		public string CreateTable { get { return @"
                    CREATE TABLE tourRejectReasons (
                        id INTEGER NOT NULL PRIMARY KEY,
                        reason TEXT NOT NULL,
                        dateAdded TEXT NOT NULL,
                        dateDeleted TEXT NULL
                    );"; } }

        public string InsertData => throw new NotImplementedException();
    }
}
