using System.Collections.Generic;
using System.Linq;
using SharkTank.Core.Models;

namespace SharkTank.DAL.InMemory
{
    public class InMemoryPermissionRepository : IPermissionRepository
    {
        private readonly List<Permission> _permissions;
        private readonly Dictionary<int, List<int>> _rolePermissionIds;
        private readonly Dictionary<int, List<int>> _userPermissionIds;

        public InMemoryPermissionRepository()
        {
            // Permission master list (demo) - bạn có thể mở rộng theo chuẩn ERP sau này
            _permissions = new List<Permission>
            {
                new Permission { PermissionId = 1, PermissionCode = "HR.VIEW", PermissionName = "HR - View", Module = "HR" },
                new Permission { PermissionId = 2, PermissionCode = "HR.CREATE", PermissionName = "HR - Create", Module = "HR" },
                new Permission { PermissionId = 3, PermissionCode = "HR.UPDATE", PermissionName = "HR - Update", Module = "HR" },
                new Permission { PermissionId = 4, PermissionCode = "HR.DELETE", PermissionName = "HR - Delete", Module = "HR" },

                new Permission { PermissionId = 5, PermissionCode = "SALES.VIEW", PermissionName = "Sales - View", Module = "Sales" },
                new Permission { PermissionId = 6, PermissionCode = "SALES.CREATE", PermissionName = "Sales - Create", Module = "Sales" },
                new Permission { PermissionId = 7, PermissionCode = "SALES.UPDATE", PermissionName = "Sales - Update", Module = "Sales" },

                new Permission { PermissionId = 8, PermissionCode = "INVENTORY.VIEW", PermissionName = "Inventory - View", Module = "Inventory" },
                new Permission { PermissionId = 9, PermissionCode = "INVENTORY.CREATE", PermissionName = "Inventory - Create", Module = "Inventory" },
                new Permission { PermissionId = 10, PermissionCode = "INVENTORY.UPDATE", PermissionName = "Inventory - Update", Module = "Inventory" },

                new Permission { PermissionId = 11, PermissionCode = "ACCOUNTING.VIEW", PermissionName = "Accounting - View", Module = "Accounting" },
                new Permission { PermissionId = 12, PermissionCode = "ACCOUNTING.CREATE", PermissionName = "Accounting - Create", Module = "Accounting" },
                new Permission { PermissionId = 13, PermissionCode = "ACCOUNTING.UPDATE", PermissionName = "Accounting - Update", Module = "Accounting" },

                new Permission { PermissionId = 14, PermissionCode = "ADMIN.VIEW", PermissionName = "Admin - View", Module = "Admin" },
                new Permission { PermissionId = 15, PermissionCode = "CRM.VIEW", PermissionName = "CRM - View", Module = "CRM" }
            };

            // RoleId -> PermissionIds
            _rolePermissionIds = new Dictionary<int, List<int>>
            {
                // Admin: tất cả quyền
                { 1, _permissions.Select(p => p.PermissionId).ToList() },

                // HR: full CRUD HR
                { 2, new List<int> { 1, 2, 3, 4 } },

                // Sales: view/create/update
                { 3, new List<int> { 5, 6, 7 } },

                // Inventory: view/create/update
                { 4, new List<int> { 8, 9, 10 } },

                // Accounting: view/create/update
                { 5, new List<int> { 11, 12, 13 } }
            };

            // UserId -> PermissionIds (chi tiết theo user, mặc định để trống)
            _userPermissionIds = new Dictionary<int, List<int>>();
        }

        public IEnumerable<Permission> GetByRoleId(int roleId)
        {
            if (_rolePermissionIds.TryGetValue(roleId, out var permissionIds))
            {
                var set = new HashSet<int>(permissionIds);
                return _permissions.Where(p => set.Contains(p.PermissionId)).ToList();
            }

            return new List<Permission>();
        }

        public IEnumerable<Permission> GetByUserId(int userId)
        {
            if (_userPermissionIds.TryGetValue(userId, out var permissionIds))
            {
                var set = new HashSet<int>(permissionIds);
                return _permissions.Where(p => set.Contains(p.PermissionId)).ToList();
            }

            return new List<Permission>();
        }
    }
}
