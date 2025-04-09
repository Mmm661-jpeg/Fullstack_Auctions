using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System.Data;
using WebApplicationAuctions.Data.Interfaces;
using WebApplicationAuctions.Domain.DomainModels;
using WebApplicationAuctions.Domain.DTO_s.Requests;

namespace WebApplicationAuctions.Data.Repository
{
    public class UsersRepo : IUserRepo
    {
        private readonly IAuctionsContext _context;
        private readonly ILogger<UsersRepo> logger;

        public UsersRepo(IAuctionsContext context, ILogger<UsersRepo> logger)
        {
            _context = context;
            this.logger = logger;

        }

        public async Task<bool> RegisterUser(string username, string userpassword)
        {

            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@UserName", username);
            parameters.Add("@UserPassword", userpassword);

            using (var db = _context.GetConnection())
            {
               try
                {
                    await db.OpenAsync();
                    var result = await db.ExecuteScalarAsync<int>("RegisterUsers",parameters,commandType:CommandType.StoredProcedure);     
                    await db.CloseAsync();
                    return result == 1; 
                }
                catch (Exception ex)
                {
                    logger.LogError(ex.Message);
                    return false;
                }
            }
        }

        public async Task<bool> UpdateUsers(UpdateUser_DTO users,int id)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@UserId", id);
            parameters.Add("@UserName", users.Username);
            parameters.Add("@UserPassword", users.Password);


            using (var db = _context.GetConnection())
            {
                await db.OpenAsync();
                int rowsUpdated = await db.ExecuteAsync("UpdateUsers", parameters, commandType: CommandType.StoredProcedure);
                await db.OpenAsync();
                return rowsUpdated == 1;
            }

        }

        public async Task<List<Users>> GetAllUsers()
        {
            using (var db = _context.GetConnection())
            {

                await db.OpenAsync();

                var result = await db.QueryAsync<Users>("GetAllUsers"
                    , commandType: CommandType.StoredProcedure);

                await db.CloseAsync();

                return result.ToList();

            }
        }

        public async Task<Users> Login(string username)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Username", username);

            using (var db = _context.GetConnection())
            {
                await db.OpenAsync();
                var result = await db.QueryFirstOrDefaultAsync<Users>("Login", parameters
                                    , commandType: CommandType.StoredProcedure);
                await db.CloseAsync();
                return result;
            }


        }

        public async Task<bool> DeleteUser(int userid)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserID", userid);

            using(var db = _context.GetConnection())
            {
                await db.OpenAsync();

                int result = await db.ExecuteScalarAsync<int>("DeleteUser", parameters, commandType: CommandType.StoredProcedure);
                await db.CloseAsync();

                return result == 1;
            }
        }

        public async Task<Users> GetUserWithID(int id)
        {
           
            var parameters = new DynamicParameters();
            parameters.Add("@UserID", id);

            using(var db =_context.GetConnection())
            {
                await db.OpenAsync();
                var result = await db.QuerySingleAsync<Users>("GetUserWithID", parameters, commandType: CommandType.StoredProcedure);
                await db.CloseAsync();

                return result;

            }
           
        }
    }
}
