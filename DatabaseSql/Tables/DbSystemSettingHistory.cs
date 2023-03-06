﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseSql.Tables
{
	internal class DbSystemSettingHistory : ITableCreate
	{
		internal DbSystemSettingHistory()
		{

		}

		public string CreateTable { get { return @"
                        create table systemSettingHistory (
                        id integer not null primary key,
                        name text not null,
                        value text not null,
                        dateAdded text not null,
                        dateDeleted text null
            );"; } }

		public string InsertData { get { return @""; } }
	}
}
