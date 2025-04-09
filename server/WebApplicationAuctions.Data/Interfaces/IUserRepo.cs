using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationAuctions.Domain.DomainModels;
using WebApplicationAuctions.Domain.DTO_s.Requests;


namespace WebApplicationAuctions.Data.Interfaces
{
    public interface IUserRepo
    {
        public Task<bool> RegisterUser(string username, string userpassword);

        public Task<bool> DeleteUser(int userid);

        public Task<bool> UpdateUsers(UpdateUser_DTO users,int id);
        public Task<List<Users>> GetAllUsers();

        public Task<Users> Login(string username);

        public Task<Users> GetUserWithID(int id);
    }
}
