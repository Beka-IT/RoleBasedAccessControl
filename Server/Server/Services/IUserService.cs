using Server.Models;
using System.Collections.Generic;

namespace Server.Services
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
        Task<List<User>> GetAllUsers();
    }

}
