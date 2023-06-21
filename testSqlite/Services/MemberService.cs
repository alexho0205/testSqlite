using Microsoft.Data.Sqlite;
using testSqlite.DBModels;
using Dapper;

namespace testSqlite.Services
{
    public class MemberService
    {
        private readonly ILogger<MemberService> _logger;
        private readonly IConfiguration _configuration;
        //private string _connectionString = "Data Source=db.db;";

        public MemberService(IConfiguration configuration, ILogger<MemberService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }


        private SqliteConnection getConnection()
        {
            string _connectionString = _configuration.GetConnectionString("DefaultConnection");
            //string _connectionString = "Data Source=db.db;";
            return  new SqliteConnection(_connectionString);
        }

        internal void add(Member member)
        {
            
            using (SqliteConnection conn = getConnection())
            {

                string insertSQL = @"  INSERT INTO users (username, usertel)
                values ($userName , $userTel);";
                var p = new DynamicParameters();
                p.Add("$userName", member.username);
                p.Add("$userTel", member.usertel);
                conn.Execute(insertSQL,p);
            }
        }

        internal List<Member> GetAllMembers()
        {
            List<Member> members = new List<Member>();



            using (SqliteConnection conn = getConnection())
            {

                try
                {
                    members = conn.Query<Member>(" select username , usertel from users").ToList();
                }  catch (Exception e)
                {
                    _logger.LogError(e.ToString());
                }

              

          
            }
            return members;
        }
    }
}
