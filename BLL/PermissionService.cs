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
            ApplyPermissions(_permissionRepository.GetByRoleId(roleId));
        }

        public void LoadPermissionsForUser(int userId, int roleId)
        {
            // Nếu user có cấu hình quyền chi tiết thì ưu tiên dùng quyền theo user.
            // Nếu chưa có thì fallback về quyền theo role như cũ.
            var userPermissions = _permissionRepository.GetByUserId(userId) ?? Enumerable.Empty<Permission>();
            var hasUserSpecific = userPermissions.Any();

            if (hasUserSpecific)
            {
                ApplyPermissions(userPermissions);
            }
            else
            {
                ApplyPermissions(_permissionRepository.GetByRoleId(roleId));
            }
        }

        public bool Has(string permissionCode)
        {
            if (string.IsNullOrWhiteSpace(permissionCode)) return false;
            return _permissionCodes.Contains(permissionCode);
        }

        private void ApplyPermissions(IEnumerable<Permission> permissions)
        {
            _permissionCodes.Clear();

            foreach (var p in permissions ?? Enumerable.Empty<Permission>())
            {
                if (!string.IsNullOrWhiteSpace(p.PermissionCode))
                {
                    _permissionCodes.Add(p.PermissionCode);
                }
            }
        }
    }
}
