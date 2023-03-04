using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseSql.Tables
{
    internal class DbSystemSetting : ITableInit
    {
        internal DbSystemSetting()
        {

        }

        public string CreateTable { get { return @"
            CREATE TABLE systemSetting (
                        id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        name TEXT NOT NULL,
                        setting TEXT NOT NULL,
                        dateAdded TEXT NOT NULL,
                        dateDeleted TEXT NULL
            );"; } }

        public string InsertData { get { return @""; } }
    }
}
