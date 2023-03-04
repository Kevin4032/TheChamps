using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseSql.Tables
{
    internal interface ITableInit
    {
        string CreateTable { get; }
        string InsertData { get; }

    }
}
