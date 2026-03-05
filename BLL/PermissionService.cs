using System.Collections.Generic;
using System.Linq;
using SharkTank.Core.Models;
using SharkTank.DAL;

namespace SharkTank.BLL
{
    public class PermissionService
    {
        private readonly IPermissionRepository _permissionRepository;

        private readonly HashSet<string> _permissionCodes = new HashSet<string>();

        public PermissionService(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public void LoadPermissionsForRole(int roleId)
        {
            _permissionCodes.Clear();
            var permissions = _permissionRepository.GetByRoleId(roleId) ?? Enumerable.Empty<Permission>();
            foreach (var p in permissions)
            {
                if (!string.IsNullOrWhiteSpace(p.PermissionCode))
                {
                    _permissionCodes.Add(p.PermissionCode);
                }
            }
        }

        public bool Has(string permissionCode)
        {
            if (string.IsNullOrWhiteSpace(permissionCode)) return false;
            return _permissionCodes.Contains(permissionCode);
        }
    }
}

