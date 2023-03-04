using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseSql.Tables
{
    internal class DbTicket : ITableInit
    {
        internal DbTicket()
        {

        }

        public string CreateTable { get { return @"
                    CREATE TABLE ticket (
                        id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        ticket TEXT NULL,
                        issueDate TEXT NULL,
                        dateAdded TEXT NOT NULL,
                        dateDeleted TEXT NULL
                    );"; } }

        public string InsertData { get { return @"";  } }
    }
}
