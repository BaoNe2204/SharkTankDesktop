using System.Collections.Generic;
using SharkTank.Core.Models;

namespace SharkTank.DAL
{
    public interface IRoleRepository
    {
        Role GetById(int roleId);
        IEnumerable<Role> GetAll();
    }
}

