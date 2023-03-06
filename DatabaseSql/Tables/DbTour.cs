using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseSql.Tables
{
    internal class DbTour : ITableCreate
    {
        internal DbTour()
        {
        }
        public string CreateTable { get { return @"
                create table tour (
                id integer not null primary key autoincrement,
                timeStart text null,
                guide integer null
            );"; } }
        public string InsertData { get { return @""; } }
    }
}
