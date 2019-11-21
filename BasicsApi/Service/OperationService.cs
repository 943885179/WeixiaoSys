using System.Linq;
using BasicsApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BasicsApi.Service
{
    public class OperationService
    {
        private WeixiaoSysContext db;
        public OperationService(WeixiaoSysContext context)
        {
            db = context;
        }

        public async  Task<List<Operation>> Operations(){
           return await db.Operation.ToListAsync();
        }
        public async  Task<int> Add(Operation Operation){
            await db.Operation.AddAsync(Operation);
            return await db.SaveChangesAsync();
        }
        public async Task<Operation> OperationById(int id){
            return await db.Operation.FindAsync(id);
        }
    }
}
