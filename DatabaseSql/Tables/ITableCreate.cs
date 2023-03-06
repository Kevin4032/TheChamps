using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseSql.Tables
{
    internal interface ITableCreate
    {
        string TableName { get; }
        string CreateTable { get; }
        string InsertData { get; }

    }
}
