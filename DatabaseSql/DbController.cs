using DatabaseSql.Tables;
using Microsoft.Data.Sqlite;
using Npgsql;
using SQLitePCL;
using System.Reflection.Metadata;

namespace DatabaseSql
{
    public class DbController : IPersistence
    {
        //private const string _databasePath = "Data Source=postgresql://localhost:5432";
        private const string _databasePath = "Server=127.0.0.1;Port=5432;Database=postgres;User Id=postgres;";

		private List<ITableCreate> _tables;
			


		public DbController()
        {
            _tables = new List<ITableCreate>{
			  new DbErrorLog()
			, new DbPerson()
			, new DbPersonTicket()
			, new DbPersonType()
			, new DbSystemSetting()
			, new DbSystemSettingHistory()
			, new DbTicket()
			, new DbTour()
			, new DbTourHistory()
			, new DbTourAdmission()
			, new DbTourAdmissionHistory()
			, new DbTourRejectReason()
			, new DbTourReservation()
			, new DbTourReservationHistory()
			};
		}

        public void CheckData()
        {
            using (var connection = new SqliteConnection(_databasePath))
            {
                connection.Open();

                var personTypeTable = new DbPersonType();
                var command = connection.CreateCommand();
                command.CommandText = personTypeTable.CreateTable;
                //command.CommandText = @"
                //                select * from user;";

                //command.ExecuteNonQuery();

                //            var nu = DateTime.UtcNow;
                //            command.CommandText = $"insert into personType (name, dateAdded) values (\"Afdelingshoofd\", \"{nu.ToString()}\")";
                //command.ExecuteNonQuery();

                //var nu = DateTime.UtcNow;
                //command.CommandText = $"insert into personType (name, dateAdded) values (\"Gids\", \"{nu}\")";
                //command.ExecuteNonQuery();

                //nu = DateTime.UtcNow;
                //command.CommandText = $"insert into personType (name, dateAdded) values (\"Bezoeker\", \"{nu}\")";
                //command.ExecuteNonQuery();

                command.CommandText = personTypeTable.InsertData;
				command.ExecuteNonQuery();

				command.CommandText = "select id, name, dateAdded from personType";
				using var reader = command.ExecuteReader();
				while (reader.Read())
				{
					Console.WriteLine(reader.GetInt32(0));
					Console.WriteLine(reader.GetString(1));
					Console.WriteLine(reader.GetString(2));
				}


			}

        }

		public bool ExistsObjectPersonnelType(string name)
		{
            using (var connection = new SqliteConnection(_databasePath))
            {
                connection.Open();

				var command = connection.CreateCommand();
				command.CommandText =
				@"
                    select id, name from personType where name = @name;
                ";
                command.Parameters.AddWithValue("@name", name);

				using var reader = command.ExecuteReader();
				if (reader.HasRows)
					return true;
			}

			return false;
		}

		public bool ExistsObjectPersonnelType(int id)
		{
			using (var connection = new SqliteConnection(_databasePath))
			{
				connection.Open();

				var command = connection.CreateCommand();
				command.CommandText =
				@"
                    select id, name from personType where id = @id;
                ";
                command.Parameters.AddWithValue("id", id);

				using var reader = command.ExecuteReader();
				if (reader.HasRows)
					return true;
			}

			return false;
		}

		public void Echt()
        {

            //using (var connection = new SqliteConnection(_databasePath))
            using (var connection = new NpgsqlConnection(_databasePath))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                ";
                command.ExecuteNonQuery();


                command.CommandText =
                @"
                    INSERT INTO systemSettings (name, setting)
                    VALUES ('Tour_MaxNumberOfVisitors', '13');
                    INSERT INTO systemSettings (name, setting)
                    VALUES ('Tour_MaxNumberOfToursPerVisitor', '1');
                    INSERT INTO systemSettings (name)
                    VALUES ('Manager');          
                ";
                command.ExecuteNonQuery();

            }


        }

        public void Test()
        {
            //var kevin = Batteries.Init();

            using (var connection = new SqliteConnection(_databasePath))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    CREATE TABLE user (
                        id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        name TEXT NOT NULL
                    );
                    INSERT INTO user
                    VALUES (1, 'Brice'),
                           (2, 'Alexander'),
                           (3, 'Nate');
                ";
                command.ExecuteNonQuery();

                Console.Write("Name: ");
                var name = Console.ReadLine();

                command.CommandText =
                @"
                    INSERT INTO user (name)
                    VALUES ($name)
                ";
                command.Parameters.AddWithValue("$name", name);
                command.ExecuteNonQuery();

                command.CommandText =
                @"
                    SELECT last_insert_rowid()
                ";
                var newId = (long)command.ExecuteScalar();

                Console.WriteLine($"Your new user ID is {newId}.");
            }

            
        }

        public void KevinInit()
        {
            InitAllTables();
            //CheckTableInit();
        }

        private void CheckTableInit()
        {
			using (var connection = new SqliteConnection(_databasePath))
			{
				connection.Open();

				var command = connection.CreateCommand();

				foreach (var table in _tables)
				{
					command.CommandText = @$"
                        select * from {table.TableName};
                    ";

                    command.CommandText = @"SELECT name FROM sqlite_master WHERE type='table';";

					using var reader = command.ExecuteReader();
                    if (reader.HasRows)
                        Console.WriteLine($"Table {table} bestaat !");
				}
			}
		}

        private void InitAllTables()
        {
			//using (var connection = new SqliteConnection(_databasePath))
			using (var connection = new NpgsqlConnection(_databasePath))
			{
                connection.Open();

                var command = connection.CreateCommand();

                foreach (var table in _tables)
                {
                    command.CommandText = table.CreateTable;
                    command.ExecuteNonQuery();
                }
            }
        }

        private void FillPersonTypeBaseTable()
        {
			using (var connection = new SqliteConnection(_databasePath))
			{
				connection.Open();

				var command = connection.CreateCommand();

				command.CommandText =
				@"
                    insert into personType (name)
                    values ('Bezoeker');
                    insert into personType (name)
                    values ('Gids');
                    insert into personType (name)
                    values ('Afdelingshoofd');
                ";
				command.ExecuteNonQuery();
			}
		}

    }
}