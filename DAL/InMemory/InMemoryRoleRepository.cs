using System.Collections.Generic;
using SharkTank.Core.Models;

namespace SharkTank.DAL.InMemory
{
    public class InMemoryRoleRepository : IRoleRepository
    {
        private readonly List<Role> _roles = new List<Role>
        {
            new Role { RoleId = 1, RoleName = "Admin", Description = "System Administrator", IsSystemRole = true },
            new Role { RoleId = 2, RoleName = "HR", Description = "Human Resources", IsSystemRole = true },
            new Role { RoleId = 3, RoleName = "Sales", Description = "Sales", IsSystemRole = true },
            new Role { RoleId = 4, RoleName = "Inventory", Description = "Inventory/Warehouse", IsSystemRole = true },
            new Role { RoleId = 5, RoleName = "Accounting", Description = "Accounting", IsSystemRole = true }
        };

        public Role GetById(int roleId)
        {
            return _roles.Find(r => r.RoleId == roleId);
        }

        public IEnumerable<Role> GetAll()
        {
            return _roles;
        }
    }
}

