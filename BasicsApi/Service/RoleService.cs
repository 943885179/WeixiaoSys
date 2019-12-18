using System.Linq;
using BasicsApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BasicsApi.Service
{
    public class RoleService
    {
        private WeixiaoSysContext db;
        public RoleService(WeixiaoSysContext context)
        {
            db = context;
        }

        public async Task<List<Role>> Roles()
        {
            return await db.Role.ToListAsync();
        }
        public async Task<List<Role>> RolesByUser(int uid)
        {
            return await db.Role.Where(o => o.EmpRole.Any(t => t.EmpId == uid)).ToListAsync();
        }

        public async Task<int> Add(Role Role)
        {
            await db.Role.AddAsync(Role);
            return await db.SaveChangesAsync();
        }
        public async Task<Role> RoleById(int id)
        {
            return await db.Role.FindAsync(id);
        }
    }
}
