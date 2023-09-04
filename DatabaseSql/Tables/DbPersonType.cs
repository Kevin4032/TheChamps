using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DatabaseSql.Tables
{
    internal class DbPersonType : ITableCreate
    {
		private const string _tableName = "personType";
		public DbPersonType() { }
		public string TableName { get { return _tableName; } }

		public string CreateTable { get { return @"
                        create table personType
                        (
                            id integer not null primary key,
                            name text not null,
                            dateAdded text not null,
                            dateDeleted text null
                        );"; } }

        public string InsertData { get { return @$"
                    INSERT INTO personType(name, dateAdded)
                    VALUES ('Gids', '{DateTime.UtcNow}');
                    INSERT INTO personType(name, dateAdded)
                    VALUES ('Bezoeker','{DateTime.UtcNow}');
                    INSERT INTO personType(name, dateAdded)
                    VALUES ('Afdelingshoofd', '{DateTime.UtcNow}');
                    "; } }
    }
}
