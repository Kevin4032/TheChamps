using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseSql.Tables
{
    internal class DbTour : ITableInit
    {
        internal DbTour()
        {
        }
        public string CreateTable { get { return @"
            CREATE TABLE tour (
                id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                timeStart TEXT NULL,
                timeEnd TEXT NULL
            );"; } }
        public string InsertData { get { return @""; } }
    }
}
