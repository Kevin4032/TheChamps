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
        internal DbPersonType()
        {

        }

        public string CreateTable { get { return @"
                        create table personType
                        (
                            id integer not null primary key autoincrement,
                            name text not null
                            dateAdded text not null,
                            dateDeleted text null
                        );"; } }

        public string InsertData { get { return @"
                    INSERT INTO personType(name)
                    VALUES('Tourguide');
                    INSERT INTO personType(name)
                    VALUES('Visitor');
                    INSERT INTO personType(name)
                    VALUES('Manager');
                    "; } }
    }
}
