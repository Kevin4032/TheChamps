using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseSql.Tables
{
    internal class DbPerson : ITableInit
    {
        internal DbPerson()
        {

        }

        public string InsertData { get { return @"";  } }

        public string CreateTable { get { return @" CREATE TABLE person (
                        id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        personType INTEGER NOT NULL,
                        dateAdded TEXT NOT NULL,
                        dateDeleted TEXT NULL
                            );"; } }

    }
}
