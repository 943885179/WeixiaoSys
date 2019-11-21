using System.Linq;
using BasicsApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BasicsApi.Service
{
    public class EmpGroupService
    {
        private WeixiaoSysContext db;
        public EmpGroupService(WeixiaoSysContext context)
        {
            db = context;
        }

        public async  Task<List<EmpGroup>> EmpGroups(){
           return await db.EmpGroup.ToListAsync();
        }
        public async  Task<int> Add(EmpGroup EmpGroup){
            await db.EmpGroup.AddAsync(EmpGroup);
            return await db.SaveChangesAsync();
        }
        public async Task<EmpGroup> EmpGroupById(int id){
            return await db.EmpGroup.FindAsync(id);
        }
    }
}
