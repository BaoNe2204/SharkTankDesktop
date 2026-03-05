using System.Collections.Generic;
using SharkTank.Core.Models;

namespace SharkTank.DAL
{
    public interface IUserRepository
    {
        User GetByUsername(string username);
        User GetById(int userId);
        void Update(User user);
        IEnumerable<User> GetAll();
    }
}

