using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseSql.Tables
{
    internal class DbPersonTicket : ITableInit
    {
        internal DbPersonTicket()
        {

        }

        public string CreateTable { get { return @"
                     CREATE TABLE personTicket (
                        person INTEGER NOT NULL,
                        ticket INTEGER NOT NULL,
                        dateAdded TEXT NOT NULL,
                        dateDeleted TEXT NULL
                    );"; } }

        public string InsertData => throw new NotImplementedException();
    }
}
