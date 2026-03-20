using System.Collections.Generic;
using SharkTank.Core.Models;

namespace SharkTank.DAL
{
    public interface IPermissionRepository
    {
        IEnumerable<Permission> GetByRoleId(int roleId);
        IEnumerable<Permission> GetByUserId(int userId);
    }
}
