using System.Linq;
using BasicsApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BasicsApi.Service
{
    public class PowerService
    {
        private WeixiaoSysContext db;
        public PowerService(WeixiaoSysContext context)
        {
            db = context;
        }
        public async  Task<List<Power>> Powers(){
           return await db.Power.ToListAsync();
        }
        public async  Task<int> Add(Power Power){
            await db.Power.AddAsync(Power);
            return await db.SaveChangesAsync();
        }
        public async Task<Power> PowerById(int id){
            return await db.Power.FindAsync(id);
        }
    }
}
