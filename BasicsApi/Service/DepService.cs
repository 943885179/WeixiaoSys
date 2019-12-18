using System.Linq;
using BasicsApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BasicsApi.Service
{
    public class DepService
    {
        private WeixiaoSysContext db;
        public DepService(WeixiaoSysContext context)
        {
            db = context;
        }

        public async Task<List<Department>> Departments()
        {
            return await db.Department.ToListAsync();
        }
        public async Task<int> Add(Department Department)
        {
            await db.Department.AddAsync(Department);
            return await db.SaveChangesAsync();
        }
        public async Task<Department> DepartmentById(int id)
        {
            return await db.Department.FindAsync(id);
        }
    }
}
