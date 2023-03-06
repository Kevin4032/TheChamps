using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseSql.Tables
{
    internal class DbPersonTicket : ITableCreate
    {
		private const string _tableName = "personTicket";
		public DbPersonTicket() { }
		public string TableName { get { return _tableName; } }

		public string CreateTable { get { return @"
                    create table personTicket (
                        person integer not null,
                        ticket integer not null
                    );"; } }

        public string InsertData => throw new NotImplementedException();
    }
}
