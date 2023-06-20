using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.Metrics;
using testSqlite.DBModels;

namespace testSqlite.Services
{
    public class MemberService
    {
        private readonly IConfiguration _configuration;
        //private string _connectionString = "Data Source=db.db;";

        public MemberService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        private SqliteConnection getConnection()
        {
            string _connectionString = _configuration.GetConnectionString("DefaultConnection");
            //string _connectionString = "Data Source=db.db;";
            return  new SqliteConnection(_connectionString);
        }

        internal void add(Member member)
        {
            
            using (var connection = getConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                    @"  INSERT INTO users (user_name, user_tel)
                values ($userName , $userTel);
                select last_insert_rowid();";
                command.Parameters.AddWithValue("$userName", member.username);
                command.Parameters.AddWithValue("$userTel", member.usertel);
                int id = Convert.ToInt32((object)command.ExecuteScalar());
            }
        }

        internal List<Member> GetAllMembers()
        {
            List<Member> members = new List<Member>();

            using (var connection = getConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"  select user_name , user_tel from users";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string userName = reader.GetString(0);
                        string userTel = reader.GetString(1);

                       Member memeber = new Member() { 
                           username = userName,
                           usertel = userTel
                       };
                        members.Add(memeber);
                    }
                }
            }
            return members;
        }
    }
}
