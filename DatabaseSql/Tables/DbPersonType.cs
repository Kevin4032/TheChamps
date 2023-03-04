using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DatabaseSql.Tables
{
    internal class DbPersonType : ITableInit
    {
        internal DbPersonType()
        {

        }

        public string CreateTable { get { return @"
                        CREATE TABLE personType
                        (
                            id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                            name TEXT NOT NULL
                            dateAdded TEXT NOT NULL,
                            dateDeleted TEXT NULL
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
