using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationAuctions.Domain.DomainModels;
using WebApplicationAuctions.Domain.DTO_s.Requests;

namespace WebApplicationAuctions.Core.Interfaces
{
    public interface IUsersService
    {
        public Task<string> Login(Login_Register_DTO users);

        public Task<bool> DeleteUser(int userid);
        public Task<bool> RegisterUser(Login_Register_DTO users);
        public Task<bool> UpdateUsers(UpdateUser_DTO users,int id);
        public Task<List<Users>> GetAllUsers();

        public Task<Users> GetUserWithID(int id);
    }
}
