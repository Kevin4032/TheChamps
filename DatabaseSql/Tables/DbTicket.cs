using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseSql.Tables
{
    internal class DbTicket : ITableCreate
    {
		private const string _tableName = "ticket";
		public DbTicket() { }
		public string TableName { get { return _tableName; } }

		public string CreateTable { get { return @"
                    create table ticket (
                        id integer not null primary key,
                        ticket text not null,
                        datePurchased text not null,
                        dateVisit text not null,
                        dateAdded text not null,
                        dateDeleted text null
                    );"; } }

        public string InsertData { get { return @"";  } }
    }
}
