using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseSql.Tables
{
    internal class DbPerson : ITableCreate
    {
		private const string _tableName = "person";
		public DbPerson() { }
		public string TableName { get { return _tableName; } }

		public string InsertData { get { return @"";  } }

        public string CreateTable { get { return @"
                        create table person (
                        id integer not null primary key autoincrement,
                        personId text not null,
                        personType integer not null,
                        name text not null,
                        postalCode text null,
                        houseNumber integer null,
                        emailAddress text null,
                        dateAdded text not null,
                        dateDeleted text null
                            );"; } }

    }
}
