using DatabaseSql.Tables;
using Microsoft.Data.Sqlite;
using SQLitePCL;

namespace DatabaseSql
{
    public class DbController
    {
        public DbController()
        {

        }

        public void CheckData()
        {
            using (var connection = new SqliteConnection("Data Source=poging20230306_001.db"))
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

        public void Echt()
        {

            using (var connection = new SqliteConnection("Data Source=HetDepot.db"))
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

            using (var connection = new SqliteConnection("Data Source=hello.db"))
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
    }
}