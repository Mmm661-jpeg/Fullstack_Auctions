using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApplicationAuctions.Core.Interfaces;
using WebApplicationAuctions.Data.Interfaces;
using WebApplicationAuctions.Domain.DomainModels;
using WebApplicationAuctions.Domain.DTO_s.Requests;

namespace WebApplicationAuctions.Core.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepo _repo;
        private readonly ILogger<UsersService> logger; //Better lgging needed in general.
        private readonly IJwtGenerator _jwtGenerator;
        public UsersService(IUserRepo repo,ILogger<UsersService> logger, IJwtGenerator jwtGenerator)
        {
            _repo = repo;
            this.logger = logger;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<string> Login(Login_Register_DTO users) 
        {
            try
            {
                var loginresult = await _repo.Login(users.Username);
                if (loginresult != null)
                {
                    bool passCheck = await VerifyPass(users.Password, loginresult.UserPassword);
                    if (passCheck)
                    {
                        string token = _jwtGenerator.GenerateToken(loginresult); //async eventually?
                        return token;
                    }
                    else
                    {
                        return null;
                    }
                  
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return null;
            }
        }


        public async Task<bool> RegisterUser(Login_Register_DTO users) 
        {
            try
            {
                users.Password = await MakeHash(users.Password);
                var result = await _repo.RegisterUser(users.Username, users.Password);
                return result;
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateUsers(UpdateUser_DTO users, int id) //dto
        {
            if(string.IsNullOrEmpty(users.Username)) //DB expects pure null
            {
                users.Username = null;
            }
            if(string.IsNullOrEmpty(users.Password))
            {
                users.Password = null;
            }

            var result = await _repo.UpdateUsers(users,id);
            return result;
        }
        public async Task<List<Users>> GetAllUsers()
        { 
            return await _repo.GetAllUsers(); //No need for list in this results are jsut gathered and pushed front. no add remove or indexing [3] might as well be ienumrable
        }

        public async Task<bool> DeleteUser(int userid)
        {
            var result = await _repo.DeleteUser(userid);
            return result;
        }

        public async Task<Users> GetUserWithID(int id)
        {
            var result = await _repo.GetUserWithID(id);
            if (result != null)
            {
                result.UserPassword = null; //should be DTO so password column name is not exposed.
                return result;
            }
            else
            {
                return null;
            }
        }

        private static Task<bool> VerifyPass(string inputPass, string dbPass)
        {
            return Task.Run(()=> BCrypt.Net.BCrypt.Verify(inputPass, dbPass));
          
        }

        private static Task<string> MakeHash(string password)
        {
            return Task.Run(() => BCrypt.Net.BCrypt.HashPassword(password));
        }

       
    }
}
