using System.Linq;
using BasicsApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BasicsApi.Service
{
    public class PositionService
    {
        private WeixiaoSysContext db;
        public PositionService(WeixiaoSysContext context)
        {
            db = context;
        }
        public async  Task<List<Position>> Positions(){
           return await db.Position.ToListAsync();
        }
        public async  Task<int> Add(Position position){
            await db.Position.AddAsync(position);
            return await db.SaveChangesAsync();
        }
        public async Task<Position> PositionById(int id){
            return await db.Position.FindAsync(id);
        }
    }
}
